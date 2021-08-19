namespace Sigmade.Domain.Models
{
    public class SearchHistory
    {
        public int Id { get; set; }
        public int UserContragentId { get; set; }
        public UserContragent UserContragent { get; set; }
        public decimal VendorCode { get; set; }
        public string Brand { get; set; }
        public string UserIpAddress { get; set; }
    }
}
