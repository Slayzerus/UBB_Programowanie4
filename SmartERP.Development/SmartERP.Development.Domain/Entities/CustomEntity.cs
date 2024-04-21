namespace SmartERP.Development.Domain.Entities
{
    public class CustomEntity
    {
        public long Id { get; set; }

        public CustomModule Module { get; set; } = new();

        public long ModuleId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<CustomEntityField> Fields { get; set; } = new();
    }
}