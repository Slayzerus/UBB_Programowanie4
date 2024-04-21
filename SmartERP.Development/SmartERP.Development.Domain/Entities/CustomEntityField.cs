namespace SmartERP.Development.Domain.Entities
{
    public class CustomEntityField
    {
        public long Id { get; set; }

        public long SmartEntityId { get; set; }

        public required CustomEntity Entity { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool IsRequired { get; set; }
    }
}
