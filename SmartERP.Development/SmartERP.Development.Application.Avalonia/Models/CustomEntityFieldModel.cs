namespace SmartERP.Development.Application.Models
{
    public class CustomEntityFieldModel
    {
        #region Properties

        public long Id { get; set; }

        public long EntityId { get; set; }

        public required CustomEntityModel? Entity { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public bool IsRequired { get; set; }

        #endregion Properties
    }
}
