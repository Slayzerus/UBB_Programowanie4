namespace SmartERP.Development.Domain.Entities
{
    public class CustomView
    {
        public long Id { get; set; }

        public required CustomModule Module { get; set; }

        public long ModuleId { get; set; }
    }
}
