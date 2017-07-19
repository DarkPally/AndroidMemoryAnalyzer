using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    
    public class ObjectArrayInfo
    {
        public int ObjectArrayID { get; set; }
        public int ClassObjectID { get; set; }
        public ClassObjectInfo ClassObject { get; set; }
        public List<object> Elements { get; set; }
        public List<int> ElementIDs { get; set; }
        public ObjectArrayInfo(DumpObjectArray org, HeapFileAnalyzer analyzer)            
        {
            ClassObjectID = org.ClassObjectID;
            if (analyzer.ClassObjectInfos.ContainsKey(org.ClassObjectID))
            {
                ClassObject = analyzer.ClassObjectInfos[org.ClassObjectID];
            }
            ObjectArrayID = org.ObjectArrayID;
            ElementIDs = org.ElementIDs;          
        }
    }
}
