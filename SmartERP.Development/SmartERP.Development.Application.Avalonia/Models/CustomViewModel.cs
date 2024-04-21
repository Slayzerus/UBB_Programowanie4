namespace SmartERP.Development.Application.Models
{
    public class CustomViewModel
    {

        #region Properties
        public long ModuleId { get; set; }

        public CustomModuleModel Module { get; set; } = new();


        #endregion Properties
    }
}
