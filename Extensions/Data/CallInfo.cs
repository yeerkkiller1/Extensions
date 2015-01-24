using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Extensions
{
    public class CallInfo : IComparable<CallInfo>
    {
        public string MemberName;
        public string SourceFilePath;
        public int LineNumber;

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

        public int CompareTo(CallInfo other)
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
