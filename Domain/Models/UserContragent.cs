using System.Collections.Generic;

namespace Sigmade.Domain.Models
{
    public class UserContragent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string AccountNumber { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

        public ICollection<OrderHistory> Orders { get; set; }
    }
}
