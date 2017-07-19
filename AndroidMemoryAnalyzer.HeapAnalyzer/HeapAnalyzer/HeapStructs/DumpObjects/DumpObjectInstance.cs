using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class DumpObjectInstance : HeapDumpObject
    {
        override public DumpObjectTag Tag { get { return DumpObjectTag.OBJECT_INSTANCE; } }
        public int ObjectID { get; set; }
        public int StackTraceSerialNumber { get; set; }
        public int ClassObjectID { get; set; }
        public int Length { get; set; }
        public byte[] InstanceFieldData { get; set; }
    }
}
