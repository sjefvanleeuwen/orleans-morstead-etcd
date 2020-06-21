using Orleans;
using Orleans.Runtime;
using Morstead.Etcd.Orleans.Test.GrainInterfaces;
using System.Threading.Tasks;

namespace Morstead.Etcd.Orleans.Test.Grains
{
    public class ScalarValuePersistentGrain<T> : Grain, IScalarValuePersistentGrain<T>
    {
        private IPersistentState<ScalarValueState<T>> Scalar { get; set; }

        public ScalarValuePersistentGrain([PersistentState("scalar-value", "unit-test")] IPersistentState<ScalarValueState<T>> scalar)
        {
            Scalar = scalar;
        }
        public async Task<T> GetScalarValue()
        {
            return await Task.FromResult(Scalar.State.Value);
        }

        public async Task SetScalarValue(T value)
        {
            Scalar.State.Value = value;
            await Scalar.WriteStateAsync();
        }
    }
}
