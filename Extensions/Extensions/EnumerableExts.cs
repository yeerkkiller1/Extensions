using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class EnumerableExts
    {
        //Equivalent to Enumerable.Skip(count).Concat(Enumerable.Take(count))
        [Test, TestCase(new int[] { 1, 5, 7, 9, 2 }, 2, new int [] { 7, 9, 2, 1, 5 }, 0)]
        public static void TestRotate<T>(IEnumerable<T> enumerable, int count, IEnumerable<T> result, T type)
        {
            result.Zip(Rotate(enumerable, count), (x, y) =>
            {
                Assert.AreEqual(x, y);
                return 0;
            }).ToList();
        }
        public static IEnumerable<T> Rotate<T>(this IEnumerable<T> enumerable, int count)
        {
            return enumerable.Skip(count).Concat(enumerable.Take(count));
        }
    }
}
