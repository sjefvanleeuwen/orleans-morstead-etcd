using Orleans.TestingHost;
using System;
using System.Collections.Generic;
using System.Text;

namespace Orleans.Persistence.Morstead.Etcd.Tests
{
    public partial class ClusterFixture : IDisposable
    {
        public ClusterFixture()
        {
            var builder = new TestClusterBuilder();
            builder.AddSiloBuilderConfigurator<TestSiloConfigurator>();

            //this.Cluster.
            Cluster = builder.Build();
            Cluster.Deploy();
        }

        public void Dispose()
        {
            Cluster.StopAllSilos();
        }

        public TestCluster Cluster { get; private set; }
    }
}
