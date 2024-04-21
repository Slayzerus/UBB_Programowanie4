namespace SmartERP.CommonTools.DataModels
{
    public class FieldDataModel
    {
        public string Name { get; set; } = string.Empty;

        public string DisplayName { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public Type Type { get; set; } = typeof(string);

        public string TypeName { get; set; } = string.Empty;

        public string Value { get; set; } = string.Empty;
    }
}
