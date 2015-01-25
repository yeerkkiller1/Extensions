using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GoodInterfaces;

namespace Extensions
{
    public class CallInfo : ICallInfo
    {
        public string MemberName { get; set; }
        public string SourceFilePath { get; set; }
        public int LineNumber { get; set; }

        public CallInfo(string memberName, string sourceFilePath, int lineNumber)
        {
            MemberName = memberName;
            SourceFilePath = sourceFilePath;
            LineNumber = lineNumber;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}:{2}", MemberName, SourceFilePath, LineNumber);
        }

        public int CompareTo(ICallInfo other)
        {
            int diff = 0;
            diff = MemberName.CompareTo(other.MemberName);
            if (diff != 0) return diff;

            diff = SourceFilePath.CompareTo(other.SourceFilePath);
            if (diff != 0) return diff;

            diff = LineNumber.CompareTo(other.LineNumber);
            if (diff != 0) return diff;

            return diff;
        }
    }
}
