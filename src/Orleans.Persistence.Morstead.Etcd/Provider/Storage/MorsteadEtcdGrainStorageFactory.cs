using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using System;

namespace Orleans.Storage
{
    /// <summary>
    /// Storage provider for writing grain state data to Morstead in JSON format.
    /// Adapted from Azure blob reference implementation which can be found on: 
    /// https://github.com/sjefvanleeuwen/orleans/blob/master/src/Azure/Orleans.Persistence.AzureStorage/Providers/Storage/AzureBlobStorage.cs
    /// </summary>
    public static class MorsteadEtcdGrainStorageFactory
    {
        public static IGrainStorage Create(IServiceProvider services, string name)
        {
            var optionsMonitor = services.GetRequiredService<IOptionsMonitor<MorsteadEtcdStorageOptions>>();
            return ActivatorUtilities.CreateInstance<MorsteadEtcdGrainStorage>(services, name, optionsMonitor.Get(name));
        }
    }
}
