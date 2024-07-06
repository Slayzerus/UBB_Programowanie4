namespace NiceToDev.ProjectGenerator
{
    public class ClassInfo
    {
        public string Name { get; set; } = string.Empty;

        public bool IsInterface { get; set; } = false;

        public List<string> Usings { get; set; } = new();

        public string Namespace => string.IsNullOrEmpty(NamespaceBase)
            ? NamespaceWithoutBase
            : $"{NamespaceBase}.{NamespaceWithoutBase}";

        public string NamespaceBase { get; set; } = string.Empty;

        public string NamespaceWithoutBase { get; set; } = string.Empty;

        public List<string> Inheritance { get; set; } = new();

        public List<ConstructorInfo> Constructors { get; set; } = new();

        public Dictionary<string, string> Properties { get; set; } = new();
    }
}
