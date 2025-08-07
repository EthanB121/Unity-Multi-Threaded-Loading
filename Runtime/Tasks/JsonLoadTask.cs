using MultiThreadedLoading.Runtime.Core;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace MultiThreadedLoading.Runtime.Tasks
{
    public class JsonLoadTask<T> : ILoadTask<T>
    {
        /// <summary>
        /// The path of the file.
        /// </summary>
        private readonly string _path;

        /// <summary>
        /// Constructs a new instance of this object.
        /// </summary>
        /// <param name="path">Path of the file to load from.</param>
        public JsonLoadTask(string path)
        {
            _path = path;
        }

        /// <summary>
        /// Runs a load task, returning the result.
        /// </summary>
        /// <param name="context">Settings for the load task.</param>
        /// <param name="token">Cancellation token.</param>
        /// <returns>The result of the load task.</returns>
        public async Task<T> RunAsync(LoadContext context, CancellationToken token)
        {
            return await Task.Run(() =>
            {
                var json = File.ReadAllText(_path);
                return JsonUtility.FromJson<T>(json);
            },token);
        }
    }
}