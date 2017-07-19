using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public enum DumpSegmentType
    {
        RootSet,
        ObjectInstance,
    }
    public class HeapDumpSegment : HeapRawData
    {
        override public HeapTag Tag { get { return HeapTag.HEAP_DUMP_SEGMENT; } }
        public DumpSegmentType SegmentType { get; set; }
        public HeapInfo HeapInfo { get; set; }
        public List<HeapDumpObject> HeapDumpObjects { get; set; }
    }
}
