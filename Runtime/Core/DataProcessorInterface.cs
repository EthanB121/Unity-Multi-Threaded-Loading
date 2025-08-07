namespace MultiThreadedLoading.Runtime.Core
{
    /// <summary>
    /// Processes loaded data to create a <see cref="UnityEngine.GameObject"/>,
    /// <br>which would need to be returned to the main thread.</br>
    /// <br>Useful for instantiation, setup, logging, etc.</br>
    /// </summary>
    /// <typeparam name="T">Type to process</typeparam>
    public interface IDataProcessor<T>
    {
        /// <summary>
        /// Processes loaded data on the main thread.
        /// </summary>
        /// <param name="data">Loaded data to process.</param>
        /// <param name="context">Context it was loaded with.</param>
        void Process(T data, LoadContext context);
    }
}