using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public enum HeapTag
    {
        STRING = 0x01,
        LOAD_CLASS = 0x02,
        STACK_TRACE = 0x05,
        HEAP_DUMP_SEGMENT = 0x1c,//包括RootSet和ObjectInstance
    }
    public class HeapRawData
    {
        virtual public HeapTag Tag { get { return 0; } }
        public int TimeTicks { get; set; }
        public int Length { get; set; }
        public byte[] RawData { get; set; }
        
    }
}
