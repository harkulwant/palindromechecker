using Harkulwant.PalindromeChecker.Model.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Harkulwant.PalindromeChecker.Repository.Context
{
    public class DemoDatabaseContext : DbContext, IDemoDatabaseContext
    {
        private const string configConnectionString = "Data:DemoDatabase:ConnectionString";
        const string defaultSchema = "demo";
        public DbSet<Palindrome> Palindromes { get; set; }
        private static string ConnectionString { get; set; }

        public DemoDatabaseContext(IConfiguration config)
        {
            ConnectionString = config.GetSection(configConnectionString).Value;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(defaultSchema);

            modelBuilder.Entity<Palindrome>().ToTable("Palindromes");
        }

        public async Task<T> SaveAsync<T>(T entity) where T: class
        {
            var response = await AddAsync<T>(entity);
            var id = await SaveChangesAsync();
            return entity;
        }
    }
}
