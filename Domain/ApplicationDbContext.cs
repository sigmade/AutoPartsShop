// This is a personal academic project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
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
        public DbSet<Main> Mains { get; set; }
        public DbSet<Child> Childs { get; set; }

        public DbSet<SubChild> SubChild { get; set; }
    }
}
