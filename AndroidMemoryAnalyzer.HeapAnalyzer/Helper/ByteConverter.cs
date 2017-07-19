using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    public static class ByteConverter
    {
        public static T2[] Arr2Arr<T1, T2>(T1[] from)
            where T1 : struct
            where T2 : struct
        {

            int byteNum = from.Length * Marshal.SizeOf(from[0]);
            T2 testByte = new T2();

            dynamic dFrom = from;
            dynamic dTo = new T2[byteNum / Marshal.SizeOf(testByte)];

            IntPtr ptr = Marshal.AllocHGlobal(byteNum);
            Marshal.Copy(dFrom, 0, ptr, from.Length);
            Marshal.Copy(ptr, dTo, 0, dTo.Length);
            return dTo;
        }
    }
}
