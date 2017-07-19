using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class DumpObjectArray : HeapDumpObject
    {
        override public DumpObjectTag Tag { get { return DumpObjectTag.OBJECT_ARRAY; } }
        public int ObjectArrayID { get; set; }
        public int StackTraceSerialNumber { get; set; }
        public int Length { get; set; }
        public int ClassObjectID { get; set; }
        public List<int> ElementIDs { get; set; }
    }
}
