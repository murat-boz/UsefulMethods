using System;
using System.Collections.Generic;
using System.Linq;

namespace UsefulMethods.EnumerableExtensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> items, Func<T, TKey> keySelector)
        {
            var seenKeys = new HashSet<TKey>();

            foreach (var item in items)
            {
                if (seenKeys.Add(keySelector(item)))
                {
                    yield return item;
                }
            }
        }

        /// <summary>
        /// This extension method is that it allows for efficient processing and handling of large collections of data by breaking them into smaller, manageable chunks.
        /// This method doesn’t change the original collection, it only returns a new collection with the batches
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<T>> Batch<T>(
            this IEnumerable<T> source, int size)
        {
            T[] bucket = null;
            var count = 0;

            foreach (var item in source)
            {
                if (bucket == null)
                    bucket = new T[size];

                bucket[count++] = item;

                if (count != size)
                    continue;

                yield return bucket.Select(x => x);

                bucket = null;
                count = 0;
            }

            // Return the last bucket with all remaining elements
            if (bucket != null && count > 0)
            {
                Array.Resize(ref bucket, count);
                yield return bucket.Select(x => x);
            }
        }

        public static Dictionary<TKey, int> CountBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            Dictionary<TKey, int> counts = new Dictionary<TKey, int>();

            foreach (TSource element in source)
            {
                TKey key = keySelector(element);

                if (counts.ContainsKey(key))
                {
                    counts[key]++;
                }
                else
                {
                    counts[key] = 1;
                }
            }

            return counts;
        }

        public static IEnumerable<TResult> FullJoin<TOuter, TInner, TKey, TResult>(
            this IEnumerable<TOuter> outer,
            IEnumerable<TInner> inner,
            Func<TOuter, TKey> outerKeySelector,
            Func<TInner, TKey> innerKeySelector,
            Func<TOuter, TInner, TResult> resultSelector)
        {
            var outerLookup = outer.ToLookup(outerKeySelector);
            var innerLookup = inner.ToLookup(innerKeySelector);

            var keys = new HashSet<TKey>(outerLookup.Select(g => g.Key)
                                                    .Concat(innerLookup.Select(g => g.Key)));

            foreach (var key in keys)
            {
                foreach (var outerItem in outerLookup[key].DefaultIfEmpty())
                {
                    foreach (var innerItem in innerLookup[key].DefaultIfEmpty())
                    {
                        yield return resultSelector(outerItem, innerItem);
                    }
                }
            }
        }
    }
}
