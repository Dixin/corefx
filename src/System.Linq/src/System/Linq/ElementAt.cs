// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;

namespace System.Linq
{
    public static partial class Enumerable
    {
        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (source is IPartition<TSource> partition)
            {
                TSource element = partition.TryGetElementAt(index, out bool found);
                if (found)
                {
                    return element;
                }
            }
            else
            {
                if (source is IList<TSource> list)
                {
                    return list[index];
                }

                if (index >= 0)
                {
                    using (IEnumerator<TSource> e = source.GetEnumerator())
                    {
                        while (e.MoveNext())
                        {
                            if (index == 0)
                            {
                                return e.Current;
                            }

                            index--;
                        }
                    }
                }
            }

            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
            return default;
        }

        public static TSource ElementAt<TSource>(this IEnumerable<TSource> source, Index index)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (!index.IsFromEnd)
            {
                return source.ElementAt(index.Value);
            }

            int indexFromEnd = index.Value;
            if (indexFromEnd > 0)
            {
                if (source is IList<TSource> list)
                {
                    return list[list.Count - indexFromEnd];
                }

                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        Queue<TSource> queue = new Queue<TSource>();
                        queue.Enqueue(e.Current);
                        while (e.MoveNext())
                        {
                            if (queue.Count == indexFromEnd)
                            {
                                queue.Dequeue();
                            }

                            queue.Enqueue(e.Current);
                        }

                        if (queue.Count == indexFromEnd)
                        {
                            return queue.Dequeue();
                        }
                    }
                }
            }

            ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
            return default;
        }

        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, int index)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (source is IPartition<TSource> partition)
            {
                return partition.TryGetElementAt(index, out bool _);
            }

            if (index >= 0)
            {
                if (source is IList<TSource> list)
                {
                    if (index < list.Count)
                    {
                        return list[index];
                    }
                }
                else
                {
                    using (IEnumerator<TSource> e = source.GetEnumerator())
                    {
                        while (e.MoveNext())
                        {
                            if (index == 0)
                            {
                                return e.Current;
                            }

                            index--;
                        }
                    }
                }
            }

            return default(TSource);
        }

        public static TSource ElementAtOrDefault<TSource>(this IEnumerable<TSource> source, Index index)
        {
            if (source == null)
            {
                ThrowHelper.ThrowArgumentNullException(ExceptionArgument.source);
            }

            if (!index.IsFromEnd)
            {
                return source.ElementAtOrDefault(index.Value);
            }

            int indexFromEnd = index.Value;
            if (indexFromEnd > 0)
            {
                if (source is IList<TSource> list)
                {
                    int count = list.Count;
                    if (count >= indexFromEnd)
                    {
                        return list[count - indexFromEnd];
                    }
                }

                using (IEnumerator<TSource> e = source.GetEnumerator())
                {
                    if (e.MoveNext())
                    {
                        Queue<TSource> queue = new Queue<TSource>();
                        queue.Enqueue(e.Current);
                        while (e.MoveNext())
                        {
                            if (queue.Count == indexFromEnd)
                            {
                                queue.Dequeue();
                            }

                            queue.Enqueue(e.Current);
                        }

                        if (queue.Count == indexFromEnd)
                        {
                            return queue.Dequeue();
                        }
                    }
                }
            }

            return default!;
        }
    }
}
