using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class HeapInfo:HeapDumpObject
    {
        override public DumpObjectTag Tag { get { return DumpObjectTag.HEAP_INFO; } }
        public int HeapID { get; set; }
        public int HeapNameID { get; set; }
    }
}
