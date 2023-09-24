using System;
using System.Threading.Tasks;

namespace UsefulMethods.Methods
{
    /// <summary>
    /// Useful for short-running tasks.
    /// </summary>
    public static class BackgroundQueue
    {
        /// <summary>
        /// ✅ Good approach
        /// </summary>
        /// <param name="action"></param>
        public static void FireAndForget(Func<Task> action)
        {
            action?.Invoke();
        }
    }
}
