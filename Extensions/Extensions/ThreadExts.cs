using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extensions
{
    public static class ThreadExts
    {
        public static void WaitThrow(this ManualResetEvent resetEvent, TimeSpan timespan,
            string error = "Timed out when waiting for ManualResetEvent")
        {
            if(!resetEvent.WaitOne(timespan))
            {
                throw new Exception(error);
            }
        }
    }
}
