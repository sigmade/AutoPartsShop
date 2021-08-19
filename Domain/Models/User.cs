using Sigmade.Domain.Models.Enums;

namespace Sigmade.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }
    }
}
