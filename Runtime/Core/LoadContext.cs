namespace MultiThreadedLoading.Runtime.Core
{
    /// <summary>
    /// Contains settings for the loading task.
    /// </summary>
    public class LoadContext
    {
        /// <summary>
        /// True to use Addressables.
        /// </summary>
        public bool UseAddressables;
        /// <summary>
        /// True to use Jobs.
        /// </summary>
        public bool UseJobs;
        /// <summary>
        /// True to use Burst.
        /// </summary>
        public bool UseBurst;
    }
}