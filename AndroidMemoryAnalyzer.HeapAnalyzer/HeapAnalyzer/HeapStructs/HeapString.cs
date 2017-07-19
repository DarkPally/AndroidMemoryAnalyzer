using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class HeapString : HeapRawData
    {
        override public HeapTag Tag { get { return HeapTag.STRING; } }
        public int StringID { get; set; }
        public string StringData { get; set; }
        
    }
}
