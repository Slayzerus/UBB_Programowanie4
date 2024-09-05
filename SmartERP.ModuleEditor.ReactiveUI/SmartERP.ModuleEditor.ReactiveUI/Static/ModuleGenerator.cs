using Mapster;
using Newtonsoft.Json;
using NiceToDev.ProjectGenerator;
using SmartERP.Development.Application.Models;
using SmartERP.ModuleEditor.ReactiveUI.Models;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SmartERP.ModuleEditor.ReactiveUI.Static
{
    public class ModuleGenerator
    {
        private readonly string _moduleRootPath;

        public ModuleGenerator()
        {
            _moduleRootPath = $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\Modules\\";
        }

        public void GenerateModule(CustomModuleModel module)
        {
            CustomModuleConfig config = new()
            {
                Id = module.Id,
                Name = module.Name,
                DisplayName = module.DisplayName,
                Description = module.Description,
                Entities = module.Entities,
                Views = module.Views,
                IsValid = module.IsValid
            };
            config.RootPath = _moduleRootPath;

            SolutionInfo solution = GenerateDotnetSolution(config);

            GeneratorService generator = new();
            string log = generator.Generate(solution);
        }

        private SolutionInfo GenerateDotnetSolution(CustomModuleConfig module)
        {
            var solution = new SolutionInfo(module.ModuleFullName, module.LibraryPath);
            var databaseProject = GenerateLibraryDatabaseProject(module);
            var domainProject = GenerateLibraryDomainProject(module);
            var infrastructureProject = GenerateLibraryInfrastructureProject(module);
            var applicationProject = GenerateLibraryApplicationProject(module);

            databaseProject.References.Add(domainProject);
            infrastructureProject.References.Add(databaseProject);
            infrastructureProject.References.Add(domainProject);
            applicationProject.References.Add(domainProject);
            applicationProject.References.Add(infrastructureProject);

            solution.Projects.Add(domainProject);
            solution.Projects.Add(databaseProject);
            solution.Projects.Add(infrastructureProject);
            solution.Projects.Add(applicationProject);
            return solution;
        }

        private SolutionInfo GenerateAPISolution(CustomModuleModel module, string moduleNameFull)
        {
            var solution = new SolutionInfo($"{module.Name}.API", $"{_moduleRootPath}{moduleNameFull}.API");
            return solution;
        }

        private ProjectInfo GenerateLibraryDatabaseProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Database", $"{module.LibraryPath}\\{module.ModuleFullName}.Database");

            Dictionary<string, string> properties = new();

            foreach (CustomEntityModel entity in module.Entities)
            {
                string dbSetName = entity.Name.Last() == 'y'
                    ? $"{entity.Name.Substring(entity.Name.Length - 1)}ies"
                    : $"{entity.Name}";

                properties.Add($"{dbSetName}", $"DbSet<{entity.Name}>");
                module.DbSetNames.Add(entity, dbSetName);
            }

            ClassInfo contextClass = new()
            {
                Name = module.DatabaseContextName,
                NamespaceWithoutBase = module.DatabaseContextsNamespace.Replace($"{module.DatabaseNamespace}.", ""),
                NamespaceBase = module.DatabaseNamespace,
                Inheritance = { "DbContext" },
                Usings = { module.DomainEntitiesNamespace, "Microsoft.EntityFrameworkCore" },
                Properties = properties
            };

            project.NuGets.Add("Microsoft.EntityFrameworkCore");
            project.Classes.Add(contextClass);
            return project;
        }

        private ProjectInfo GenerateLibraryDomainProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Domain", $"{module.LibraryPath}\\{module.ModuleFullName}.Domain");

            project.NuGets.Add(module.NuGetCommonToolsName);

            //Entities
            foreach (CustomEntityModel entity in module.Entities)
            {
                ClassInfo entityClass = new()
                {
                    Name = entity.Name,
                    NamespaceBase = module.DomainNamespace,
                    NamespaceWithoutBase = module.DomainEntitiesNamespace.Replace($"{module.DomainNamespace}.", "")
                };

                foreach (CustomEntityFieldModel field in entity.Fields)
                {
                    entityClass.Properties.Add(field.Name, field.Type);
                }

                project.Classes.Add(entityClass);
            }

            //IRepository
            ClassInfo irepositoryClass = new()
            {
                Name = module.InfrastructureIRepositoryName,
                NamespaceBase = module.DomainNamespace,
                NamespaceWithoutBase = module.InfrastructureIRepositoryNamespace.Replace($"{module.DomainNamespace}.", ""),
                Usings = { $"{module.NuGetCommonToolsName}.Repositories" },
                Inheritance = { $"I{module.GenericRepositoryName}" },
                IsInterface = true
            };

            project.Classes.Add(irepositoryClass);

            return project;
        }

        private ProjectInfo GenerateLibraryInfrastructureProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Infrastructure", $"{module.LibraryPath}\\{module.ModuleFullName}.Infrastructure");

            ClassInfo repositoryClass = new()
            {
                Name = module.InfrastructureRepositoryName,
                NamespaceBase = module.InfrastructureNamespace,
                NamespaceWithoutBase = module.InfrastructureRepositoriesNamespace.Replace($"{module.InfrastructureNamespace}.", ""),
                Usings = { $"{module.NuGetCommonToolsName}.Repositories", module.InfrastructureIRepositoryNamespace, module.DatabaseContextsNamespace },
                Inheritance = { module.GenericRepositoryName, module.InfrastructureIRepositoryName },
                Constructors = 
                { 
                    new NiceToDev.ProjectGenerator.ConstructorInfo() 
                    {  
                        BaseCalls = new() { FirstCharToLower(module.DatabaseContextName) },
                        Parameters = new() { { FirstCharToLower(module.DatabaseContextName), module.DatabaseContextName } }                        
                    } 
                }

            };

            project.Classes.Add(repositoryClass);

            return project;
        }

        private ProjectInfo GenerateLibraryApplicationProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Application", $"{module.LibraryPath}\\{module.ModuleFullName}.Application");

            ClassInfo iserviceClass = new()
            {
                Name = module.ApplicationIServiceName,
                NamespaceBase = module.ApplicationNamespace,
                NamespaceWithoutBase = module.ApplicationInterfacesNamespace.Replace($"{module.ApplicationNamespace}.", ""),
                Usings = { $"{module.NuGetCommonToolsName}.Services" },
                Inheritance = { $"{module.GenericIServiceName}" },
                IsInterface = true
            };

            ClassInfo serviceClass = new()
            {
                Name = module.ApplicationServiceName,
                NamespaceBase = module.ApplicationNamespace,
                NamespaceWithoutBase = module.ApplicationServicesNamespace.Replace($"{module.ApplicationNamespace}.", ""),
                Usings = {
                    module.InfrastructureIRepositoryNamespace,
                    module.GenericServiceNamespace,
                    module.ApplicationInterfacesNamespace
                },
                Constructors ={
                    new NiceToDev.ProjectGenerator.ConstructorInfo()
                    {
                         Parameters = { { FirstCharToLower(module.InfrastructureRepositoryName), module.InfrastructureIRepositoryName } },
                         BaseCalls = new(){ FirstCharToLower(module.InfrastructureRepositoryName) }
                    }
                },
                Inheritance = { module.GenericServiceName, module.ApplicationIServiceName }

            };

            project.Classes.Add(iserviceClass);
            project.Classes.Add(serviceClass);

            return project;
        }

        public static string FirstCharToLower(string input)
        {
            if (string.IsNullOrEmpty(input) || char.IsLower(input[0]))
            {
                return input;
            }                

            return char.ToLower(input[0]) + input.Substring(1);
        }       
    }
}
