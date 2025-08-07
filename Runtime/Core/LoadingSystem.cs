using MultiThreadedLoading.Runtime.Dispatching;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreadedLoading.Runtime.Core
{
    /// <summary>
    /// Manages and executes loading tasks.
    /// </summary>
    public class LoadingSystem
    {
        /// <summary>
        /// Contains all the loading tasks.
        /// </summary>
        private readonly List<ILoadTask<object>> _tasks = new();
        /// <summary>
        /// The load context.
        /// </summary>
        private readonly LoadContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoadingSystem"/> class.
        /// </summary>
        /// <param name="context"></param>
        public LoadingSystem(LoadContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Adds a task to the loading system.
        /// </summary>
        /// <typeparam name="T">The data type.</typeparam>
        /// <param name="task">Task to load.</param>
        /// <param name="processor">Optional post processing, for example to create a GameObject.</param>
        public void AddTask<T>(ILoadTask<T> task, IDataProcessor<T> processor = null)
        {
            //Add the task and wrap it.
            _tasks.Add(new WrappedLoadTask<T>(task, processor));
        }

        /// <summary>
        /// Runs all the tasks.
        /// </summary>
        /// <param name="token">Cancellation token.</param>
        /// <returns>A task that completes when all tasks have completed.</returns>
        public async Task RunAllAsync(CancellationToken token = default)
        {
            //Run all the tasks.
            foreach (var task in _tasks)
            {
                await task.RunAsync(_context, token);
            }
        }

        /// <summary>
        /// Wraps a load task.
        /// </summary>
        /// <typeparam name="T">Data type.</typeparam>
        private class WrappedLoadTask<T> : ILoadTask<object>
        {
            /// <summary>
            /// The load task.
            /// </summary>
            private readonly ILoadTask<T> _task;
            /// <summary>
            /// The data processor.
            /// </summary>
            private readonly IDataProcessor<T> _processor;

            /// <summary>
            /// Initializes a new instance of the <see cref="WrappedLoadTask{T}"/> class.
            /// </summary>
            /// <param name="task">Task to wrap.</param>
            /// <param name="processor">processor to wrap if any.</param>
            public WrappedLoadTask(ILoadTask<T> task, IDataProcessor<T> processor = null)
            {
                _task = task;
                _processor = processor;
            }

            /// <summary>
            /// Runs the task.
            /// </summary>
            /// <param name="context">Settings for the load task.</param>
            /// <param name="token">Token to cancel the task.</param>
            /// <returns></returns>
            public async Task<object> RunAsync(LoadContext context, CancellationToken token)
            {
                //Run the task.
                var result = await _task.RunAsync(context, token);

                //Process the result on the main thread if processor is available.
                if (_processor != null)
                {
                    MainThreadDispatcher.Enqueue(() => _processor.Process(result, context));
                }

                //Return the result.
                return result;
            }
        }
    }
}