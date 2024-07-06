namespace NiceToDev.ProjectGenerator
{
    public class ProjectInfo
    {
        public ProjectInfo(string name, string path, ProjectTemplate template = ProjectTemplate.ClassLib)
        {
            Name = name;
            Path = path;
            Template = template;
            CsprojPath = $"{path}\\{name}.csproj";
        }

        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        public string CsprojPath { get; set; } = string.Empty;

        public ProjectTemplate Template { get; set; } = ProjectTemplate.ClassLib;

        public List<ClassInfo> Classes { get; set; } = new();

        public List<string> NuGets { get; set; } = new();

        public List<ProjectInfo> References { get; set; } = new();

        public List<string> Directories { get; set; } = new();
    }
}
