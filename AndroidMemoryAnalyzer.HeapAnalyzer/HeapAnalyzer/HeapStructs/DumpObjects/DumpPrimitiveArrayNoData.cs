using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class DumpPrimitiveArrayNoData : HeapDumpObject
    {
        override public DumpObjectTag Tag 
        {
            get { return DumpObjectTag.PRIMITIVE_ARRAY_WITHOUT_DATA; } 
        }
        public int PrimitiveArrayID { get; set; }
        public int StackTraceSerialNumber { get; set; }
        public int Length { get; set; }
    }
}
