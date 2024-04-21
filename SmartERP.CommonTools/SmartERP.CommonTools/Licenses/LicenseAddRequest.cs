namespace SmartERP.CommonTools.Licenses
{
    public class LicenseAddRequest
    {
        public string CompanyName { get; set; } = string.Empty;

        public DateTime ExpirationDate { get; set; } = DateTime.Now.AddMonths(1);
    }
}
