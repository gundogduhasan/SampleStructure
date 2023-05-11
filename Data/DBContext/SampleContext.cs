using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Data.DBContext
{
    public class SampleContext : DbContext
    {
        #region DBSets
       
        public DbSet<Company> Companies { get; set; }
       
        #endregion DBSets

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                ConfigurationBuilder configurationBuilder = new ConfigurationBuilder();
                string path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                configurationBuilder.AddJsonFile(path, false);

                IConfigurationRoot root = configurationBuilder.Build();
                string connectionString = root.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
