using Data.DBContext;
using Data.DBContext;
using Microsoft.Extensions.DependencyInjection;

namespace DataAccess.ServiceExtensions
{
    /// <summary>
    /// This class responsible to DBContext injection, creating db(if not exist), apply pending migrations 
    /// </summary>
    public static class DBInitializerService
    {
        /// <summary>
        ///This method responsible to DBContext injection, creating db(if not exist), apply pending migrations 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection InitializeDatabase(this IServiceCollection services)
        {
            services.AddDbContext<SampleContext>();

            //ServiceProvider provider = services.BuildServiceProvider();

            //BogforingContext _context = provider.GetService<BogforingContext>();


            //IEnumerable<string> pendingMigrations = _context.Database.GetPendingMigrations();

            //if (pendingMigrations.Count() > 0)
            //    _context.Database.Migrate();

            return services;
        }
    }
}
