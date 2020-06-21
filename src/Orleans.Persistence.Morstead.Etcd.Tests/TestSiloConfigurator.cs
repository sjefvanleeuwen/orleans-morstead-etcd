using Orleans.Hosting;
using Orleans.TestingHost;

namespace Orleans.Persistence.Morstead.Etcd.Tests
{
    public class TestSiloConfigurator : ISiloConfigurator
    {
        public void Configure(ISiloBuilder siloBuilder)
        {
            siloBuilder
                .AddMorsteadEtcdGrainStorage(name: "unit-test",options=> {
                    // etcd nodes
                    options.ConnectionString = "http://localhost:2379";
                });
        }
    }
}
