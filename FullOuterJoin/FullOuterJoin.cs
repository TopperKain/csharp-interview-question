using System;
using System.Collections.Generic;
using System.Linq;

namespace FullOuterJoin
{
    public static class FullOuterJoin
    {
        public static IEnumerable<TResult> FullOuterJoin<TLeft, TRight, TKey, TResult>(
            this IEnumerable<TLeft> left,
            IEnumerable<TRight> right,
            Func<TLeft, TKey> leftKeySelector,
            Func<TRight, TKey> rightKeySelector,
            Func<TLeft, TRight, TKey, TResult> resultSelector)
        {
            var leftLookup = left.ToLookup(leftKeySelector);
            var rightLookup = right.ToLookup(rightKeySelector);

            var leftKeys = new HashSet<TKey>(leftLookup.Select(g => g.Key));
            var rightKeys = new HashSet<TKey>(rightLookup.Select(g => g.Key));

            var allKeys = new HashSet<TKey>(leftKeys);
            allKeys.UnionWith(rightKeys);

            foreach (var key in allKeys)
            {
                var leftElements = leftLookup[key];
                var rightElements = rightLookup[key];

                if (leftElements.Any() && rightElements.Any())
                {
                    foreach (var leftElement in leftElements)
                    {
                        foreach (var rightElement in rightElements)
                        {
                            yield return resultSelector(leftElement, rightElement, key);
                        }
                    }
                }
                else if (leftElements.Any())
                {
                    foreach (var leftElement in leftElements)
                    {
                        yield return resultSelector(leftElement, default, key);
                    }
                }
                else
                {
                    foreach (var rightElement in rightElements)
                    {
                        yield return resultSelector(default, rightElement, key);
                    }
                }
            }
        }
    }
}
