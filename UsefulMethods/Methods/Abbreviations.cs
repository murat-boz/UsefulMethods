using System;
using System.Collections.Generic;

namespace UsefulMethods.Methods
{
    public static class Abbreviations
    {
        public static IEnumerable<T> Arr<T>(params T[] elements) => elements;

        public static void Try(
            Action action,
            Action<Exception>? errorHandler = null
        )
        {
            try
            {
                action();
            }
            catch (Exception ex)
            {
                if (errorHandler != null)
                {
                    errorHandler(ex);
                }
            }
        }

        public static T Try<T>(
            Func<T> func,
            Action<Exception>? errorHandler = null)
        {
            try
            {
                return func();
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
    }
}
