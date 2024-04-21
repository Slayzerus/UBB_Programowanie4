namespace SmartERP.Development.Application.Models
{
    public class CustomEntityModel
    {
        #region Property

        public long Id { get; set; }

        public CustomModuleModel? Module { get; set; }

        public long ModuleId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public List<CustomEntityFieldModel> Fields { get; set; } = new();

        #endregion Property
    }
}
