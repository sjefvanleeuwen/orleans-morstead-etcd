using Orleans.TestingHost;
using Morstead.Etcd.Orleans.Test.GrainInterfaces;
using Xunit;

namespace Orleans.Persistence.Morstead.Etcd.Tests
{
    public class ScalarGrainPersistenceTests : IClassFixture<ClusterFixture>
    {
        private readonly TestCluster cluster;

        public ScalarGrainPersistenceTests(ClusterFixture fixture)
        {
            cluster = fixture.Cluster;
        }

        [Fact]
        public async void CanWriteAndReadStringValueType()
        {
            var sut = cluster.GrainFactory.GetGrain<IScalarValuePersistentGrain<string>>(1);
            await sut.SetScalarValue("Hello World!");
            Assert.Equal("Hello World!", await sut.GetScalarValue());

            sut = cluster.GrainFactory.GetGrain<IScalarValuePersistentGrain<string>>(2);
            await sut.SetScalarValue("Hello World! 2");
            Assert.Equal("Hello World! 2", await sut.GetScalarValue());
        }
    }
}
