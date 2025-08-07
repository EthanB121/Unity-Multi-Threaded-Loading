using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadedLoading.Runtime.Core
{
    /// <summary>
    /// A unit of work that loads data.
    /// </summary>
    /// <typeparam name="T">A given data type.</typeparam>
    public interface ILoadTask<T>
    {
        /// <summary>
        /// Runs the load task.
        /// </summary>
        /// <param name="context">Settings for the load task.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns></returns>
        Task<T> RunAsync(LoadContext context, CancellationToken token);
    }
}
