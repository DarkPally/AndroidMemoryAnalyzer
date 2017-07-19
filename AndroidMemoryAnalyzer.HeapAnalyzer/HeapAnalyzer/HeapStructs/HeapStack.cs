using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class HeapStack : HeapRawData
    {
        override public HeapTag Tag { get { return HeapTag.STACK_TRACE; } }
        public int StactTrace { get; set; }
        public int Thread { get; set; }
        public int NoFrames { get; set; }
        
    }
}
