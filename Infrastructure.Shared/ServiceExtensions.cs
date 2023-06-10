using Core.Domain.Shared.Contacts;
using Infrastructure.Shared.Helper;
using Infrastructure.Shared.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Shared
{
    public static class ServiceExtensions
    {
        public static void AddSharedInfrastructure(this IServiceCollection services)
        {
            services.AddTransient(typeof(IFileManagementRepository), typeof(FileManagementRepository));
            services.AddTransient(typeof(RefitHelper), typeof(RefitHelper));
        }

    }
}
