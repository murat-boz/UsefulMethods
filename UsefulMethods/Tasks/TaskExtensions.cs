using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UsefulMethods.Tasks
{
    public static class TaskExtensions
    {
        public static async Task TryAsync(
            this Task task,
            Action<Exception> errorHandler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler(ex);
                }
            }
        }

        public static async Task<T> TryAsync<T>(
            this Task<T> task,
            Action<Exception> errorHandler = null)
        {
            try
            {
                return await task;
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler(ex);

                    
                }

                return default(T);
            }
        }

        public static async Task<IEnumerable<T>> WhenAllAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return await Task
                .WhenAll(tasks)
                .ConfigureAwait(false);
        }

        public static Task WhenAllAsync(this IEnumerable<Task> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            return Task
                .WhenAll(tasks);
        }

        public static async Task<IEnumerable<T>> WhenAllSequentialAsync<T>(this IEnumerable<Task<T>> tasks)
        {
            if (tasks is null)
            {
                throw new ArgumentNullException(nameof(tasks));
            }

            var results = new List<T>();

            foreach (var task in tasks)
            {
                results.Add(await task.ConfigureAwait(false));
            }

            return results;
        }

        public static async Task<TOut> MapAsync<TIn, TOut>(
            this Task<TIn> task,
            Func<TIn, Task<TOut>> mapAsync)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (mapAsync is null)
            {
                throw new ArgumentNullException(nameof(mapAsync));
            }

            return await mapAsync(await task);
        }

        public static async Task<TOut> MapAsync<TIn, TOut>(
            this Task<TIn> task,
            Func<TIn, TOut> map)
        {
            if (task is null)
            {
                throw new ArgumentNullException(nameof(task));
            }

            if (map is null)
            {
                throw new ArgumentNullException(nameof(map));
            }

            return map(await task);
        }
    }
}
