using System.ComponentModel.DataAnnotations;

namespace SmartERP.CommonTools.Licenses
{
    public class LicenseMachine
    {
        public Guid LicenseKey { get; set; }

        public License? License { get; set; } = null;

        [Key]
        public Guid MachineKey { get; set; }

        public string MachineId { get; set; } = string.Empty;
    }
}
