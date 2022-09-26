using ApiApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiApp.Data
{
    public class DataContext : DbContext
    {
        public DbSet<AppUser> Users { get; set; }

        public DataContext(DbContextOptions options) : base(options)
        {

        }
    }
}
