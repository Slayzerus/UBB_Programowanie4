namespace NiceToDev.ProjectGenerator
{
    public class ConstructorInfo
    {
        public Dictionary<string, string> Parameters { get; set; } = new();

        public List<string> BaseCalls { get; set; } = new();
    }
}
