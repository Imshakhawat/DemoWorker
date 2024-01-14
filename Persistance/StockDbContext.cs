using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Persistance
{
    public class StockDbContext : DbContext, IStockDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssembly;

        public StockDbContext(string connectionString, string migrationAssembly)
        {
            _connectionString = connectionString;
            _migrationAssembly = migrationAssembly;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString,
                    x => x.MigrationsAssembly(_migrationAssembly));
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Student>().HasData(StudentSeed.Students);

            base.OnModelCreating(modelBuilder);
        }

        public int SaveChanges()
        {
            return base.SaveChanges();
        }
        public DbSet<StockPrice> StockPrice { get; set; }
        public DbSet<Company> Company { get; set; }

    }
}
