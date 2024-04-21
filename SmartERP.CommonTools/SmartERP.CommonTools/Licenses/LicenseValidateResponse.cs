namespace SmartERP.CommonTools.Licenses
{
    public class LicenseValidateResponse
    {
        public bool IsValid { get; set; } = false;

        public DateTime ExpirationDate { get; set; } = DateTime.MinValue;
    }
}
