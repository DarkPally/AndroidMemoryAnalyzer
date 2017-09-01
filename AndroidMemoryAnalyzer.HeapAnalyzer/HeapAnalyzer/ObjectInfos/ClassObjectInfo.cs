using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    
    public class ClassObjectInfo
    {
        public int ClassObjectID { get; set; }
        public string ClassName { get; set; }
        public int SuperClassObjectID { get; set; }
        public string SuperClassName { get; set; }
        public List<PrimitiveObjectInfo> StaticFields { get; set; }
        public List<PrimitiveObjectInfo> InstanceFields { get; set; }
        public ClassObjectInfo(DumpClassObject org,HeapFileAnalyzer analyzer)            
        {
            ClassObjectID = org.ClassObjectID;
            ClassName = analyzer.ClassNames[org.ClassObjectID];
            SuperClassObjectID = org.SuperClassObjectID;
            SuperClassName = analyzer.ClassNames.ContainsKey(SuperClassObjectID) ?
                analyzer.ClassNames[org.SuperClassObjectID] :
                "未找到基类名称";
            if (org.StaticFieldContent.StaticFields != null)
            {
                StaticFields = org.StaticFieldContent.StaticFields.Select(c =>
                    new PrimitiveObjectInfo()
                    {
                        NameID = c.NameID,
                        Name = analyzer.StringDatas[c.NameID],
                        Type = c.Type,
                        Value = c.Value,
                    }).ToList();
            }
            if (org.InstanceFieldContent.InstanceFields != null)
            {
                InstanceFields = org.InstanceFieldContent.InstanceFields.Select(c =>
                 new PrimitiveObjectInfo()
                 {
                     NameID = c.NameID,
                     Name = analyzer.StringDatas[c.NameID],
                     Type = c.Type,
                 }).ToList();
            }
            
        }
    }
}
