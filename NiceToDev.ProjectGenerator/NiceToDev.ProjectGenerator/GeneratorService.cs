using NiceToDev.CommandLine;
using System.Text;

namespace NiceToDev.ProjectGenerator
{
    public class GeneratorService
    {
        private readonly CmdClient _cmdClient = new(true, false);

        public string Generate(SolutionInfo solution)
        {
            ClearPath(solution.Path);
            StringBuilder stringBuilder = new();
            CreateDirectoryIfNotExists(solution.Path);
            foreach (ProjectInfo project in solution.Projects)
            {
                stringBuilder.AppendLine(Generate(project));
            }
            stringBuilder.AppendLine(GenerateSolution(solution));
            return stringBuilder.ToString();
        }

        public string Generate(ProjectInfo project)
        {
            StringBuilder result = new();
            _cmdClient.AddCommand($"cd {GetParentPath(project.Path)}");
            _cmdClient.AddCommand($"dotnet new {project.Template.ToString().ToLower()} -n {project.Name}");
            result.AppendLine(_cmdClient.Execute());

            foreach (ClassInfo classInfo in project.Classes)
            {
                Generate(classInfo, project.Path);
            }

            if (project.NuGets.Count > 0)
            {
                foreach (string nuget in project.NuGets)
                {
                    _cmdClient.AddCommand($"dotnet add {project.CsprojPath} package {nuget}");
                }
                result.AppendLine(_cmdClient.Execute());
            }

            if (project.References.Count > 0)
            {
                foreach (ProjectInfo reference in project.References)
                {
                    _cmdClient.AddCommand($"dotnet add {project.CsprojPath} reference {reference.CsprojPath}");
                }
                result.AppendLine(_cmdClient.Execute());
            }

            return result.ToString();
        }

        public void Generate(ClassInfo classInfo, string rootPath)
        {
            StringBuilder content = new();
            if (classInfo.Usings.Count > 0)
            {
                foreach (string usingLine in classInfo.Usings)
                {
                    content.AppendLine($"using {usingLine};");
                }
                content.AppendLine();
            }

            content.AppendLine($"namespace {classInfo.Namespace}");
            content.AppendLine("{");

            string classWithInheritance = classInfo.Inheritance.Count > 0
                ? $"{classInfo.Name} : {string.Join(", ", classInfo.Inheritance)}"
                : $"{classInfo.Name}";

            string classOrOther = classInfo.IsInterface ? "interface" : "class";

            content.AppendLine($"\tpublic {classOrOther} {classWithInheritance}");
            content.AppendLine("\t{");

            if (classInfo.Constructors.Count > 0)
            {
                foreach (ConstructorInfo constructor in classInfo.Constructors)
                {
                    string parameters = string.Join(", ", constructor.Parameters.Select(p => $"{p.Value} {p.Key}"));
                    string baseCalls = constructor.BaseCalls.Count > 0
                        ? $": base({string.Join(", ", constructor.BaseCalls)})"
                        : string.Empty;
                    content.AppendLine($"\t\tpublic {classInfo.Name}({parameters}) {baseCalls}");
                    content.AppendLine("\t\t{");
                    content.AppendLine("\t\t}");
                }
            }

            foreach (KeyValuePair<string, string> property in classInfo.Properties)
            {
                content.AppendLine($"\t\tpublic {property.Value} {property.Key} {{ get; set; }}");
            }

            content.AppendLine("\t}");
            content.AppendLine("}");

            string directoryPath = $"{rootPath}\\{classInfo.NamespaceWithoutBase.Replace(".", "\\")}";
            CreateDirectoryIfNotExists(classInfo.NamespaceWithoutBase.Replace(".", "\\"), rootPath);
            string filePath = $"{directoryPath}\\{classInfo.Name}.cs";
            File.WriteAllText(filePath, content.ToString());
        }

        private void ClearPath(string path)
        {
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
            }
        }

        private void CreateDirectoryIfNotExists(string path, string rootPath = "")
        {
            string[] parts = path.Split("\\");
            string newPath = string.Empty;

            for (int i = 0; i < parts.Length; i++)
            {
                newPath += parts[i];
                string fullPath = string.IsNullOrEmpty(rootPath)
                        ? newPath
                        : $"{rootPath}\\{newPath}";
                if (!Directory.Exists(fullPath))
                {
                    Directory.CreateDirectory(fullPath);
                }
                newPath += "\\";
            }
        }

        private string GenerateSolution(SolutionInfo solution)
        {
            _cmdClient.AddCommand($"cd {solution.Path}");
            _cmdClient.AddCommand($"dotnet new sln -n {solution.Name}");
            foreach (ProjectInfo project in solution.Projects)
            {
                _cmdClient.AddCommand($"dotnet sln {solution.SlnPath} add {project.CsprojPath}");
            }

            return _cmdClient.Execute();
        }

        private string GetParentPath(string path)
        {            
            return Path.GetFullPath(Path.Combine(path, @".."));
        }
    }
}
