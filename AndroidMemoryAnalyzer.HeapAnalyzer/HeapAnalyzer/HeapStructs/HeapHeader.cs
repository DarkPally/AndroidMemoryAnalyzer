using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class HeapHeader
    {
        public string Magic { get; set; }
        public int IDSize { get; set; }
        public long TimeTicks { get; set; }
        public DateTime Time { get{return new DateTime(1970,1,1).AddTicks(TimeTicks);} }

        
    }
}
