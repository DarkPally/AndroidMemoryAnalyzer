using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class DumpPrimitiveArray : HeapDumpObject
    {
        override public DumpObjectTag Tag 
        {
            get { return  DumpObjectTag.PRIMITIVE_ARRAY_WITH_DATA ; } 
        }
        public int PrimitiveArrayID { get; set; }
        public int StackTraceSerialNumber { get; set; }
        public int Length { get; set; }
        public PrimitiveType Type { get; set; }
        public byte[] ElementDatas { get; set; }
    }
}
