using Business.EntityServices;
using Microsoft.Extensions.DependencyInjection;

namespace Business.ServiceExtensions
{
    public static class BusinessService
    {
        public static IServiceCollection AddBusinessService(this IServiceCollection services)
        {
            services.AddScoped<ICompanyService, CompanyService>();
            

            return services;
        }
    }
}
