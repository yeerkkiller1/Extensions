using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Seed
    {
        public static IEnumerable<object> TypeMix = new object[]{
            0, 0.1, ""
        };

        static IEnumerable<int> IntSeedsGeometric(int count)
        {
            Random rand = new Random(5);
            return Enumerable.Range(0, count).Select(x =>
                (int)(Math.Pow(2, rand.NextDouble() * 31) * (2 * rand.NextDouble() - 1))
            );
        }

        static IEnumerable<int> IntSeedEdgeCases()
        {
            yield return 0;
            yield return int.MaxValue;
            yield return int.MinValue;
        }

        public static IEnumerable<int> IntMix =
            Enumerable.Empty<int>()
            .Concat(IntSeedEdgeCases())
            .Concat(IntSeedsGeometric(10))
            .ToList();

        static IEnumerable<double> DoubleSeedsGeometric(int count)
        {
            Random rand = new Random(5);
            return Enumerable.Range(0, count).Select(x =>
                (Math.Pow(2, rand.NextDouble() * 100) * (2 * rand.NextDouble() - 1))
            );
        }

        static IEnumerable<double> DoubleSeedEdgeCases()
        {
            yield return 0;
            yield return double.MinValue;
            yield return double.MaxValue;
            yield return double.Epsilon;
        }

        public static IEnumerable<double> DoubleMix =
            Enumerable.Empty<double>()
            .Concat(DoubleSeedEdgeCases())
            .Concat(DoubleSeedsGeometric(10))
            .ToList();
    }
}
