namespace Blackjack
{
    using System;
    using System.Collections.Generic;

    internal static class Extensions
    {
        /// <remarks>ref: https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_.22inside-out.22_algorithm </remarks>
        public static void Shuffle<T>(this IList<T> list, Random rng)
        {
            var idx = list.Count;

            while (idx > 1)
            {
                idx--;
                var k = rng.Next(0, list.Count);

                if (idx == k)
                {
                    continue;
                }

                var value = list[k];
                list[k] = list[idx];
                list[idx] = value;
            }
        }

        public static bool TryDequeue<T>(this Queue<T> queue, out T output)
        {
            if (queue.Count > 0)
            {
                output = queue.Dequeue();
                return true;
            }

            output = default(T);
            return false;
        }
    }
}