namespace SmartERP.Development.Domain.Entities
{
    public class CustomModule
    {
        public long Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<CustomEntity> Entities { get; set; } = new();

        public List<CustomView> Views { get; set; } = new();
    }
}
