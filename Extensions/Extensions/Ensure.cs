using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Extensions
{
    public static class Ensure
    {
        public static void IsOnThread(Thread thread, string error="")
        {
            if(thread != Thread.CurrentThread)
            {
                throw new Exception(error 
                    + "\n" + "Got a call from the thread "
                    + Thread.CurrentThread
                    + " when it should have been from " 
                    + thread.ManagedThreadId);
            }
        }

        static ConcurrentDictionary<CallInfo, Thread> threads = new ConcurrentDictionary<CallInfo, Thread>();
        public static void IsSingleThreaded (
              [CallerMemberName] string memberName = ""
            , [CallerFilePath] string sourceFilePath = ""
            , [CallerLineNumber] int sourceLineNumber = 0
        ) {
            CallInfo curInfo = new CallInfo(memberName, sourceFilePath, sourceLineNumber);

            if(!threads.TryAdd(curInfo, Thread.CurrentThread))
            {
                Thread otherThread = threads[curInfo];
                //Well then, there is a conflict, let's give a nice exception
                throw new Exception(
                    string.Format("Multiple threads (first {0}, then {1}) have called {0}, which is supposed to be single threaded.",
                        Thread.CurrentThread.ManagedThreadId, otherThread.ManagedThreadId, curInfo
                    )
                );
            }
        }

        public static void IsInTest()
        {
            if(!NCrunch.IsInTest())
            {
                throw new Exception("This should only be run in tests, don't call it normally!");
            }
        }
        public static void IsNotInTest()
        {
            if (NCrunch.IsInTest())
            {
                throw new Exception("This should NOT be run in tests, because our code sucks and is not test friendly!");
            }
        }
    }
}
