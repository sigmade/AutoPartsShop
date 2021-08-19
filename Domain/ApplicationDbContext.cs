using Microsoft.EntityFrameworkCore;
using Sigmade.Domain.Models;

namespace Sigmade.Domain
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<OrderHistory> OrderHistories { get; set; }
        public DbSet<SearchHistory> SearchHistories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserContragent> UserContragents { get; set; }
    }
}
