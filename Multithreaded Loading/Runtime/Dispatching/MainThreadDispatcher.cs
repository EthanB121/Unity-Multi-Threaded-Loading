using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace MultiThreadedLoading.Runtime.Dispatching
{
    /// <summary>
    /// Dispatcher for actions to be executed on the main thread.
    /// </summary>
    public class MainThreadDispatcher : MonoBehaviour
    {
        /// <summary>
        /// Queue of actions to be executed on the main thread.
        /// </summary>
        private static readonly ConcurrentQueue<Action> _mainThreadActions = new();

        /// <summary>
        /// Enqueues an action to be executed on the main thread.
        /// </summary>
        /// <param name="action">Action to be executed on the main thread</param>
        public static void Enqueue(Action action) => _mainThreadActions.Enqueue(action);

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        private void Update()
        {
            //Execute all actions in the queue.
            while (_mainThreadActions.TryDequeue(out var action))
            {
                action?.Invoke();
            }
        }
    }
}