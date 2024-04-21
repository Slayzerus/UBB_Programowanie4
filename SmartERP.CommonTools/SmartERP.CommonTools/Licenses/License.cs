using System.ComponentModel.DataAnnotations;

namespace SmartERP.CommonTools.Licenses
{
    public class License
    {
        [Key]
        public Guid Key { get; set; }

        public string CompanyName { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddMonths(1);

        public List<LicenseMachine> Machines { get; set; } = new List<LicenseMachine>();
    }
}
