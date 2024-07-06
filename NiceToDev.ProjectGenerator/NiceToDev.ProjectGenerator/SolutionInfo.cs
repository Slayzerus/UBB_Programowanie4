namespace NiceToDev.ProjectGenerator
{
    public class SolutionInfo
    {
        public SolutionInfo(string name, string path)
        {
            Name = name;
            Path = path;
            SlnPath = $"{path}\\{name}.sln";
        }

        public string Name { get; set; } = string.Empty;

        public string Path { get; set; } = string.Empty;

        public string SlnPath { get; set; } = string.Empty;

        public List<ProjectInfo> Projects { get; set; } = new();
    }
}
