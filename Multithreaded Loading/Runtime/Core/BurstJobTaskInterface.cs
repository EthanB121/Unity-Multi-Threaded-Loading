using System.Threading;

namespace MultiThreadedLoading.Runtime.Core
{
    /// <summary>
    /// Abstraction for running a burst-compatible job that returns a result of type T.
    /// </summary>
    /// <typeparam name="T">The result type of the job.</typeparam>
    public interface IBurstJobTask<T>
    {
        /// <summary>
        /// Runs the job, blocking until complete.
        /// </summary>
        /// <param name="token">Optional cancellation token.</param>
        /// <returns>The processed result.</returns>
        T Execute(CancellationToken token);
    }
}