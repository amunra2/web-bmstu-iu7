using Microsoft.Extensions.DependencyInjection;
using ServerING.ModelConverters;

namespace ServerING.Utils {
    public static class ProvideExtension {
        public static IServiceCollection AddDtoConverters(this IServiceCollection services) {
            services.AddTransient<PlatformConverters>();
            // другие конвертеры

            return services;
        }
    }
}