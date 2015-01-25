using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public static class NCrunch
    {
        public static bool IsInTest()
        {
            return Environment.GetEnvironmentVariable("NCrunch") == "1";
        }

        public static void SimTest(Action action)
        {
            //Assert.IsFalse(IsInTest());
            try
            {
                Environment.SetEnvironmentVariable("NCrunch", "1");
                action();
            }
            finally
            {
                Environment.SetEnvironmentVariable("NCrunch", "0");
            }
        }

        private static int rand_internal(int min, int max)
        {
            long seed = DateTime.Now.Ticks;
            int rand = (int)(seed % max + min);
            return rand;
        }
        public static int Rand()
        {
            return Rand(int.MinValue, int.MaxValue);
        }
        public static int Rand(int max)
        {
            return Rand(0, max);
        }
        public static int Rand(int min, int max)
        {
            return rand_internal(min, max);
        }

        public static int RandPort()
        {
            return Rand(10000, 20000);
        }
    }
}
