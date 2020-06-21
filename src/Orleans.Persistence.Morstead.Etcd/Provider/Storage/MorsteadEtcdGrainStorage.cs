using dotnet_etcd;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Orleans.Configuration;
using Orleans.Persistence.Morstead.Etcd.Provider;
using Orleans.Runtime;
using Orleans.Serialization;
using System;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Orleans.Storage
{
    /// <summary>
    /// Storage provider for writing grain state data to Morstead in JSON format.
    /// Adapted from Azure blob reference implementation which can be found on: 
    /// https://github.com/sjefvanleeuwen/orleans/blob/master/src/Azure/Orleans.Persistence.AzureStorage/Providers/Storage/AzureBlobStorage.cs
    /// </summary>
    public class MorsteadEtcdGrainStorage : IGrainStorage, ILifecycleParticipant<ISiloLifecycle>
    {

        private readonly string name;
        private readonly MorsteadEtcdStorageOptions options;
        private readonly SerializationManager serializationManager;
        private readonly IGrainFactory grainFactory;
        private readonly ITypeResolver typeResolver;
        private readonly ILogger<MorsteadEtcdGrainStorage> logger;
        private JsonSerializerSettings jsonSettings;
        private EtcdClient etcdClient;

        public MorsteadEtcdGrainStorage(
        string name,
        MorsteadEtcdStorageOptions options,
        SerializationManager serializationManager,
        IGrainFactory grainFactory,
        ITypeResolver typeResolver,
        ILogger<MorsteadEtcdGrainStorage> logger)
        {
            this.name = name;
            this.options = options;
            this.serializationManager = serializationManager;
            this.grainFactory = grainFactory;
            this.typeResolver = typeResolver;
            this.logger = logger;
        }

        public Task ClearStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            throw new NotImplementedException();
        }

        public void Participate(ISiloLifecycle lifecycle)
        {
            lifecycle.Subscribe(OptionFormattingUtilities.Name<MorsteadEtcdGrainStorage>(this.name), this.options.InitStage, Init);
        }

        public async Task ReadStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var key = GetKeyName(grainType, grainReference);
            if (this.logger.IsEnabled(LogLevel.Trace)) this.logger.Trace((int)MorsteadEtcdProviderErrorCode.MorsteadEtcdProvider_Storage_Reading, "Reading: GrainType={0} Grainid={1} ETag={2} from BlobName={3} in Container={4}", grainType, grainReference, grainState.ETag, key, name);
            string json = await etcdClient.GetValAsync(key);
            if (string.IsNullOrEmpty(json))
                return;
            grainState.State = this.ConvertFromStorageFormat(json);
            grainState.ETag = key;
            return;
        }

        private object ConvertFromStorageFormat(string contents)
        {
            return JsonConvert.DeserializeObject<object>(contents, this.jsonSettings);
        }
        private string ConvertToStorageFormat(object grainState)
        {
            return JsonConvert.SerializeObject(grainState, this.jsonSettings);
        }

        private string GetKeyName(string grainType, GrainReference grainId)
        {
            return string.Format("{0}-{1}-{2}.json", name,grainType, grainId.ToKeyString());
        }
        public async Task WriteStateAsync(string grainType, GrainReference grainReference, IGrainState grainState)
        {
            var key = GetKeyName(grainType, grainReference);
            await etcdClient.PutAsync(key, ConvertToStorageFormat(grainState.State));
        }

        /// <summary> Initialization function for this storage provider. </summary>
        private async Task Init(CancellationToken ct)
        {
            var stopWatch = Stopwatch.StartNew();
            try
            {
                this.logger.LogInformation((int)MorsteadEtcdProviderErrorCode.MorsteadEtcdProvider_InitProvider, $"MoresteadEtcdGrainStorage initializing: {this.options.ToString()}");
                this.logger.LogInformation((int)MorsteadEtcdProviderErrorCode.MorsteadEtcdProvider_ParamConnectionString, "MoresteadEtcdGrainStorage is using storage key prefix {0}",  name);
                this.jsonSettings = OrleansJsonSerializer.UpdateSerializerSettings(OrleansJsonSerializer.GetDefaultSerializerSettings(this.typeResolver, this.grainFactory), this.options.UseFullAssemblyNames, this.options.IndentJson, this.options.TypeNameHandling);

                this.options.ConfigureJsonSerializerSettings?.Invoke(this.jsonSettings);
                etcdClient = new EtcdClient(this.options.ConnectionString);
                stopWatch.Stop();
                this.logger.LogInformation((int)MorsteadEtcdProviderErrorCode.MorsteadEtcdProvider_InitProvider, $"Initializing provider {this.name} of type {this.GetType().Name} in stage {this.options.InitStage} took {stopWatch.ElapsedMilliseconds} Milliseconds.");
            }
            catch (Exception ex)
            {
                stopWatch.Stop();
                this.logger.LogError((int)ErrorCode.Provider_ErrorFromInit, $"Initialization failed for provider {this.name} of type {this.GetType().Name} in stage {this.options.InitStage} in {stopWatch.ElapsedMilliseconds} Milliseconds.", ex);
                throw;
            }
        }
    }
}
