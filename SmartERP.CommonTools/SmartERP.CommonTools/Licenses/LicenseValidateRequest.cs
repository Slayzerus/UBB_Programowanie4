using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartERP.CommonTools.Licenses
{
    public class LicenseValidateRequest
    {
        public Guid LicenseKey { get; set; }

        public Guid MachineKey { get; set; }
    }
}
