using NiceToDev.ProjectGenerator;
using SmartERP.Development.Application.Models;
using SmartERP.ModuleEditor.ReactiveUI.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
            CustomModuleConfig config = new();
            config.RootPath = _moduleRootPath;

           /* string moduleName = $"SmartERP.Module.{module.Name.Replace(" ", "").Replace(".", "")}";
            string modulePath = $"{_moduleRootPath}{moduleName}";
            string libProjectPath = $"{modulePath}\\{moduleName}";


            GenerateProject(moduleName, libProjectPath, "classlib");
            GenerateDbContext(module, moduleName, libProjectPath);
            foreach (CustomEntityModel entity in module.Entities)
            {
                GenerateEntity(module, entity, moduleName, libProjectPath);
            }
            GenerateProject($"{moduleName}.API", $"{libProjectPath}.API", "webapi");
            Directory.CreateDirectory($"{modulePath}\\{moduleName}.VueJS");
            CreateSolution(moduleName, modulePath);*/

            // Generate module
        }

        private SolutionInfo GenerateLibrarySolution(CustomModuleConfig module)
        {
            var solution = new SolutionInfo(module.Name, module.LibraryPath);
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

            return project;
        }
        
        private ProjectInfo GenerateLibraryDomainProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Domain", $"{module.LibraryPath}\\{module.ModuleFullName}.Domain");

            return project;
        }
        
        private ProjectInfo GenerateLibraryInfrastructureProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Infrastructure", $"{module.LibraryPath}\\{module.ModuleFullName}.Infrastructure");

            return project;
        }

        private ProjectInfo GenerateLibraryApplicationProject(CustomModuleConfig module)
        {
            var project = new ProjectInfo($"{module.ModuleFullName}.Application", $"{module.LibraryPath}\\{module.ModuleFullName}.Application");

            return project;
        }

        private void CreateSolution(string moduleName, string modulePath)
        {
            string cdCommand = $"cd {_moduleRootPath}";
            string cdAgainCommand = $"cd {modulePath}";
            string command = $"dotnet new sln -n {moduleName} -o {moduleName}";
            string addLibProjectCommand = $"dotnet sln add {modulePath}\\{moduleName}\\{moduleName}.csproj";
            string addApiProjectCommand = $"dotnet sln add {modulePath}\\{moduleName}.API\\{moduleName}.API.csproj";
            Process.Start("cmd.exe", $"/c {cdCommand}&{command}&{cdAgainCommand}&{addLibProjectCommand}&{addApiProjectCommand}");
        }

        private void GenerateProject(string moduleName, string modulePath, string template)
        {                        
            if (Directory.Exists(modulePath))
            {
                Directory.Delete(modulePath, true);
            }
            string command = $"dotnet new {template} -lang \"C#\" -n {moduleName} -f \"net8.0\" -o \"{modulePath}\"";
            Process.Start("cmd.exe", $"/c {command}");

            Directory.CreateDirectory($"{modulePath}\\Database");
            Directory.CreateDirectory($"{modulePath}\\Domain");
            Directory.CreateDirectory($"{modulePath}\\Domain\\Entities");
            
        }

        private void GenerateDbContext(CustomModuleModel module, string moduleName, string modulePath)
        {
            StringBuilder contextContent = new StringBuilder();
            contextContent.AppendLine("using System;");
            contextContent.AppendLine("using System.Collections.Generic;");
            contextContent.AppendLine("using Microsoft.EntityFrameworkCore;");
            contextContent.AppendLine("");
            contextContent.AppendLine($"namespace {moduleName}.Domain.Entities");
            contextContent.AppendLine("{");
            contextContent.AppendLine($"\tpublic class {moduleName}Context : DbContext");
            contextContent.AppendLine("\t{");
            foreach(CustomEntityModel entity in module.Entities)
            {
                contextContent.AppendLine($"\t\tpublic DbSet<{entity.Name}> {entity.Name}s {{ get; set; }}");                       
            }
            contextContent.AppendLine("\t}");
            contextContent.AppendLine("}");

            string contextPath = $"{modulePath}\\Database\\{moduleName}Context.cs";
            File.WriteAllText(contextPath, contextContent.ToString());
        }

        private void GenerateEntity(
            CustomModuleModel module, 
            CustomEntityModel entity, 
            string moduleName,
            string modulPath)
        {
            string entityPath = $"{modulPath}\\Domain\\Entities\\{entity.Name}.cs";
            string entityContent = GenerateEntityContent(module, entity, moduleName);
            File.WriteAllText(entityPath, entityContent);
        }

        private string GenerateEntityContent(CustomModuleModel module, CustomEntityModel entity, string moduleName)
        {
            StringBuilder entityContent = new StringBuilder();
            entityContent.AppendLine("using System;");
            entityContent.AppendLine("using System.Collections.Generic;");
            entityContent.AppendLine("");
            /*entityContent.AppendLine("using System.Linq;");
            entityContent.AppendLine("using System.Text;");
            entityContent.AppendLine("using System.Threading.Tasks;");*/
            entityContent.AppendLine();
            entityContent.AppendLine($"namespace {moduleName}.Domain.Entities");
            entityContent.AppendLine("{");
            entityContent.AppendLine($"\tpublic class {entity.Name}");
            entityContent.AppendLine("\t{");
            foreach (CustomEntityFieldModel field in entity.Fields)
            {
                entityContent.AppendLine($"\t\tpublic {field.Type} {field.Name} {{ get; set; }}");
            }
            entityContent.AppendLine("\t}");
            entityContent.AppendLine("}");

            return entityContent.ToString();
        }
    }
}
