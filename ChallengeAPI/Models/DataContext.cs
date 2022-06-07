using Microsoft.EntityFrameworkCore;

namespace ChallengeAPI.Models
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _config;

        public DataContext(IConfiguration config)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder opt)
        {
            var connectionString = _config.GetConnectionString("MySqlDatabase");
            opt.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().Property(b => b.Id)
                .ValueGeneratedOnAdd();
        }

        public DbSet<Cliente> Clientes { get; set; } = null!;
    }
}
