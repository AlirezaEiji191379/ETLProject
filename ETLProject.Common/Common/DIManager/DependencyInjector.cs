using ETLProject.Common.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace ETLProject.Common.Common.DIManager
{
    public static class DependencyInjector
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IRandomStringGenerator,RandomStringGenerator>();

        }

    }
}
