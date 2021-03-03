using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class DataValue
    {
        private long val;
        private Callback backVal;
        public DataValue(long num, Callback callback)
        {
            val = num;
            backVal = callback;
        }
        public void ThreadFactorial()
        {
            long ans = Program.Factorial(val);
            if (backVal != null) backVal(ans);
        }
        public void ThreadSummaNum()
        {
            long ans = Program.SummaNum(val);
            if (backVal != null) backVal(ans);
        }
    }
}
