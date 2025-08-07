using MultiThreadedLoading.Runtime.Core;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadedLoading.Runtime.Tasks
{
    /// <summary>
    /// A load task that executes a generic Burst-compiled job and returns its result.
    /// </summary>
    /// <typeparam name="T">The result type produced by the job.</typeparam>
    public class BurstLoadTask<T> : ILoadTask<T>
    {
        /// <summary>
        /// The job task to run.
        /// </summary>
        private readonly IBurstJobTask<T> _jobTask;

        /// <summary>
        /// Constructs a new instance of this object.
        /// </summary>
        /// <param name="jobTask"></param>
        public BurstLoadTask(IBurstJobTask<T> jobTask)
        {
            _jobTask = jobTask;
        }

        /// <summary>
        /// Runs a load job, returning the result.
        /// </summary>
        /// <param name="context">Settings for the load task.</param>
        /// <param name="token">Cancellation task.</param>
        /// <returns>The result of the load task.</returns>
        public async Task<T> RunAsync(LoadContext context, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                return _jobTask.Execute(token);
            });
        } 
    }
}