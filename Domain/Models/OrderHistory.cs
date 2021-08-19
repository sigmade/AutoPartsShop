namespace Sigmade.Domain.Models
{
    public class OrderHistory
    {
        public int Id { get; set; }
        public int UserContragentId { get; set; }
        public UserContragent UserContragent { get; set; }
        public decimal VendorCode { get; set; }
        public string Brand { get; set; }
        public int Count { get; set; }
    }
}
