using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class HeapClass : HeapRawData
    {
        override public HeapTag Tag { get { return HeapTag.LOAD_CLASS; } }        
        public int SerialNumber { get; set; }
        public int ObjectID { get; set; }
        public int StackTrace { get; set; }
        public int NameID { get; set; }
        
    }
}
