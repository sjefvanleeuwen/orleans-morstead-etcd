using dotnet_etcd;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Orleans.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Orleans.Runtime.ReminderService
{
    public class MorsteadEtcdBasedReminderTable : IReminderTable
    {
        private EtcdClient etcdClient;

        private readonly IGrainReferenceConverter grainReferenceConverter;
        private readonly ILogger logger;
        private readonly ILoggerFactory loggerFactory;
        private readonly ClusterOptions clusterOptions;
        private readonly MorsteadEtcdReminderStorageOptions storageOptions;

        public MorsteadEtcdBasedReminderTable(
            IGrainReferenceConverter grainReferenceConverter,
            ILoggerFactory loggerFactory,
            IOptions<ClusterOptions> clusterOptions,
            IOptions<MorsteadEtcdReminderStorageOptions> storageOptions)
        {
            this.grainReferenceConverter = grainReferenceConverter;
            this.logger = loggerFactory.CreateLogger<MorsteadEtcdBasedReminderTable>();
            this.loggerFactory = loggerFactory;
            this.clusterOptions = clusterOptions.Value;
            this.storageOptions = storageOptions.Value;
        }

        public async Task Init()
        {
            etcdClient = new EtcdClient(this.storageOptions.ConnectionString);
        }

        public Task<ReminderEntry> ReadRow(GrainReference grainRef, string reminderName)
        {
            throw new NotImplementedException();
        }

        public async Task<ReminderTableData> ReadRows(GrainReference key)
        {
            ReminderTableData data = new ReminderTableData();
            // E.g. Get all keys with pattern "key/*"
            var range = await etcdClient.GetRangeValAsync($"{key}/").ConfigureAwait(false);
            foreach (var item in range)
            {
                //data.Reminders.Add(new ReminderEntry()
               // {
             //       item.Value.ToByteArray()
            //    })
            }

            return null;
        }

        public Task<ReminderTableData> ReadRows(uint begin, uint end)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoveRow(GrainReference grainRef, string reminderName, string eTag)
        {
            throw new NotImplementedException();
        }

        public Task TestOnlyClearTable()
        {
            throw new NotImplementedException();
        }

        public Task<string> UpsertRow(ReminderEntry entry)
        {
            throw new NotImplementedException();
        }
    }
}
