using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using Orleans.Hosting;
using Orleans.Providers;
using Orleans.Runtime;
using Orleans.Storage;

namespace Orleans.Hosting
{
    /// <summary>
    /// Configure silo to use Morstead storage as the default grain storage.
    /// Adapted from Azure blob reference implementation which can be found on: 
    /// https://github.com/sjefvanleeuwen/orleans/blob/master/src/Azure/Orleans.Persistence.AzureStorage/Hosting/AzureBlobSiloBuilderExtensions.cs
    /// </summary>
    public static class MorsteadEtcdSiloBuilderExtensions
    {
        /// <summary>
        /// Configure silo to use Morstead as the default grain storage.
        /// </summary>
        public static ISiloHostBuilder AddMorsteadEtcdGrainStorageAsDefault(this ISiloHostBuilder builder, Action<MorsteadEtcdStorageOptions> configureOptions)
        {
            return builder.AddMorsteadEtcdGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        /// <summary>
        /// Configure silo to use Morstead for grain storage.
        /// </summary>
        public static ISiloHostBuilder AddMorsteadEtcdGrainStorage(this ISiloHostBuilder builder, string name, Action<MorsteadEtcdStorageOptions> configureOptions)
        {
            return builder.ConfigureServices(services => services.AddMorsteadEtcdGrainStorage(name, configureOptions));
        }

        /// <summary>
        /// Configure silo to use Morstead as the default grain storage.
        /// </summary>
        public static ISiloHostBuilder AddMorsteadEtcdGrainStorageAsDefault(this ISiloHostBuilder builder, Action<OptionsBuilder<MorsteadEtcdStorageOptions>> configureOptions = null)
        {
            return builder.AddMorsteadEtcdGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        /// <summary>
        /// Configure silo to use Morstead for grain storage.
        /// </summary>
        public static ISiloHostBuilder AddMorsteadEtcdGrainStorage(this ISiloHostBuilder builder, string name, Action<OptionsBuilder<MorsteadEtcdStorageOptions>> configureOptions = null)
        {
            return builder.ConfigureServices(services => services.AddMorsteadEtcdGrainStorage(name, configureOptions));
        }

        /// <summary>
        /// Configure silo to use Morstead as the default grain storage.
        /// </summary>
        public static ISiloBuilder AddMorsteadEtcdGrainStorageAsDefault(this ISiloBuilder builder, Action<MorsteadEtcdStorageOptions> configureOptions)
        {
            return builder.AddMorsteadEtcdGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        /// <summary>
        /// Configure silo to use Morstead for grain storage.
        /// </summary>
        public static ISiloBuilder AddMorsteadEtcdGrainStorage(this ISiloBuilder builder, string name, Action<MorsteadEtcdStorageOptions> configureOptions)
        {
            return builder.ConfigureServices(services => services.AddMorsteadEtcdGrainStorage(name, configureOptions));
        }

        /// <summary>
        /// Configure silo to use Morstead as the default grain storage.
        /// </summary>
        public static ISiloBuilder AddMorsteadEtcdGrainStorageAsDefault(this ISiloBuilder builder, Action<OptionsBuilder<MorsteadEtcdStorageOptions>> configureOptions = null)
        {
            return builder.AddMorsteadEtcdGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        /// <summary>
        /// Configure silo to use Morstead for grain storage.
        /// </summary>
        public static ISiloBuilder AddMorsteadEtcdGrainStorage(this ISiloBuilder builder, string name, Action<OptionsBuilder<MorsteadEtcdStorageOptions>> configureOptions = null)
        {
            return builder.ConfigureServices(services => services.AddMorsteadEtcdGrainStorage(name, configureOptions));
        }

        /// <summary>
        /// Configure silo to use Morstead as the default grain storage.
        /// </summary>
        public static IServiceCollection AddMorsteadEtcdGrainStorageAsDefault(this IServiceCollection services, Action<MorsteadEtcdStorageOptions> configureOptions)
        {
            return services.AddMorsteadEtcdGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, ob => ob.Configure(configureOptions));
        }

        /// <summary>
        /// Configure silo to use Morstead for grain storage.
        /// </summary>
        public static IServiceCollection AddMorsteadEtcdGrainStorage(this IServiceCollection services, string name, Action<MorsteadEtcdStorageOptions> configureOptions)
        {
            return services.AddMorsteadEtcdGrainStorage(name, ob => ob.Configure(configureOptions));
        }

        /// <summary>
        /// Configure silo to use Morstead as the default grain storage.
        /// </summary>
        public static IServiceCollection AddMorsteadEtcdGrainStorageAsDefault(this IServiceCollection services, Action<OptionsBuilder<MorsteadEtcdStorageOptions>> configureOptions = null)
        {
            return services.AddMorsteadEtcdGrainStorage(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME, configureOptions);
        }

        /// <summary>
        /// Configure silo to use Morstead for grain storage.
        /// </summary>
        public static IServiceCollection AddMorsteadEtcdGrainStorage(this IServiceCollection services, string name,
            Action<OptionsBuilder<MorsteadEtcdStorageOptions>> configureOptions = null)
        {
            configureOptions?.Invoke(services.AddOptions<MorsteadEtcdStorageOptions>(name));
            services.AddTransient<IConfigurationValidator>(sp => new MorsteadStorageOptionsValidator(sp.GetRequiredService<IOptionsMonitor<MorsteadEtcdStorageOptions>>().Get(name), name));
            services.ConfigureNamedOptionForLogging<MorsteadEtcdStorageOptions>(name);
            services.TryAddSingleton<IGrainStorage>(sp => sp.GetServiceByName<IGrainStorage>(ProviderConstants.DEFAULT_STORAGE_PROVIDER_NAME));
            return services.AddSingletonNamedService<IGrainStorage>(name, MorsteadEtcdGrainStorageFactory.Create)
                           .AddSingletonNamedService<ILifecycleParticipant<ISiloLifecycle>>(name, (s, n) => (ILifecycleParticipant<ISiloLifecycle>)s.GetRequiredServiceByName<IGrainStorage>(n));
        }
    }
}
