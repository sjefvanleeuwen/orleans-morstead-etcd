using dotnet_etcd;
using Newtonsoft.Json;
using Orleans.Reminders.Morstead.Etcd.Storage;
using Xunit;

namespace Orleans.Persistence.Morstead.Etcd.Tests
{
    public class EtcdClientTests
    {
        [Fact]
        public async void CanDeserializeKvsFromRange()
        {
            JsonConvert.SerializeObject(new MorsteadEtcdBasedReminderEntry()
            {
                ETag = "ETag"
            });

            var client = new EtcdClient("http://localhost:2379");
            await client.PutAsync("key/1", JsonConvert.SerializeObject(new MorsteadEtcdBasedReminderEntry()
            {
                ETag = "ETag"
            }));
            var s =  await client.GetRangeValAsync("key/*");
            foreach (var i in s)
            {

            }
        }
    }
}
