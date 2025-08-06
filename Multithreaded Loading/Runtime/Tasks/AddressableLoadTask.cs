using MultiThreadedLoading.Runtime.Core;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace MultiThreadedLoading.Runtime.Tasks
{
    /// <summary>
    /// A load task that loads an addressable asset.
    /// </summary>
    /// <typeparam name="T">Type to load.</typeparam>
    public class AddressableLoadTask<T> : ILoadTask<T> where T : UnityEngine.Object
    {
        /// <summary>
        /// The address of the object to load.
        /// </summary>
        private readonly string _address;

        /// <summary>
        /// Constructs a new instance of this object.
        /// </summary>
        /// <param name="address">Address of the object to load.</param>
        public AddressableLoadTask(string address)
        {
            _address = address;
        }

        /// <summary>
        /// Runs a load task, returning the result.
        /// </summary>
        /// <param name="context">Settings for the load task.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The result of the load task.</returns>
        public async Task<T> RunAsync(LoadContext context, CancellationToken token)
        {
            //Get the handle of the addressable and wait for it to load.
            var handle = Addressables.LoadAssetAsync<T>(_address);
            await handle.Task;
            return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
        }
    }
}