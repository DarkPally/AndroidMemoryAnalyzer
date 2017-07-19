using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    public class PrimitiveObjectInfo
    {
        public int NameID { get; set; }
        public string Name { get; set; }
        public PrimitiveType Type { get; set; }
        public object Value { get; set; }
    }




}
