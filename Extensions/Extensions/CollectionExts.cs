using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class DictionaryExtensions
    {
        public static V GetCreate<K, V>(
            this Dictionary<K, V> dict,
            K key
        )
            where V : new()
        {
            if (dict.ContainsKey(key))
            {
                return dict[key];
            }

            var value = new V();
            dict.Add(key, value);
            return value;
        }
    }

    public static class ListExtensions
    {
        public static List<T> InsertSorted<T>(this List<T> list, T value) where T : IComparable<T>
        {
            var pos = list.BinarySearch(value);
            if (pos < 0) pos = ~pos;
            list.Insert(pos, value);
            return list;
        }

        public static List<T> Sort<T, K>(this List<T> list, Func<T, K> map) where K : IComparable<K>
        {
            //OR, we could make a list of tuples with T, K and sort that,
            //OR, we could make a dictionary to map it, to reduce map calls

            list.Sort((x, y) => map(x).CompareTo(map(y)));
            return list;
        }

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T val in enumerable)
            {
                action(val);
            }
        }

        public static void Add<K, V>(this ConcurrentDictionary<K, V> dict, K key, V value)
        {
            if (!dict.TryAdd(key, value)) throw new Exception("Trying to add duplicate key in ConcurrentDictionary: " + key);
        }

        public static V Remove<K, V>(this ConcurrentDictionary<K, V> dict, K key)
        {
            V val;
            if (!dict.TryRemove(key, out val)) throw new KeyNotFoundException();
            return val;
        }

        public static void Add<T>(this ConcurrentQueue<T> queue, T val)
        {
            queue.Enqueue(val);
        }
    }
}
