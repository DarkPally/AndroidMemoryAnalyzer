using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    public class ReferenceObjectInfo:PrimitiveObjectInfo
    {
        public object ReferenceTarget { get; set; }
    }
    public class ObjectInstanceInfo
    {
        public int ClassObjectID { get; set; }
        public object ClassObject { get; set; }
        public string ClassName 
        {
            get 
            {
                if (ClassObject is ClassObjectInfo) return (ClassObject as ClassObjectInfo).ClassName;
                if (ClassObject is string) return ClassObject as string;
                return null;
            }
        }
        public int ObjectID { get; set; }
        public List<PrimitiveObjectInfo> InstanceFields { get; set; }
        public ObjectInstanceInfo(DumpObjectInstance org, HeapFileAnalyzer analyzer)            
        {
            ClassObjectID = org.ClassObjectID;
            if (analyzer.ClassObjectInfos.ContainsKey(org.ClassObjectID))
            {
                ClassObject = analyzer.ClassObjectInfos[org.ClassObjectID];
            }
            else if (analyzer.ClassNames.ContainsKey(org.ClassObjectID))
            {
                ClassObject = analyzer.ClassNames[org.ClassObjectID];
            }
            ObjectID = org.ObjectID;

            int length = org.Length;

            #region ClassObject方法 -ClassObjectInfo
            if (ClassObject is ClassObjectInfo)
            {
                int templength = 0;
                ClassObjectInfo tempClass = ClassObject as ClassObjectInfo;
                InstanceFields = new List<PrimitiveObjectInfo>();
            
                while(templength<length)
                {
                    if (tempClass.InstanceFields == null) break;
                    foreach (var it in tempClass.InstanceFields)
                    {                    
                       var value=PrimitiveTypeHelper.GetPrimitiveValue(it.Type, org.InstanceFieldData, ref templength);
                       if (it.Type != PrimitiveType.HPROF_BASIC_OBJECT)
                       {
                           InstanceFields.Add(new PrimitiveObjectInfo()
                               {
                                   Name = it.Name,
                                   NameID = it.NameID,
                                   Type = it.Type,
                                   Value = value,
                               });
                       }
                       else
                       {
                           InstanceFields.Add(new ReferenceObjectInfo()
                           {
                               Name = it.Name,
                               NameID = it.NameID,
                               Type = it.Type,
                               Value = value,
                               ReferenceTarget = null
                           });
                       }
                    }

                    if (!analyzer.ClassObjectInfos.ContainsKey(tempClass.SuperClassObjectID)) break;
                    tempClass = analyzer.ClassObjectInfos[tempClass.SuperClassObjectID];
                }  
            };
            #endregion

            #region ClassObject方法 -string
            if (ClassObject is string)
            {
                InstanceFields = new List<PrimitiveObjectInfo>();
                if(ClassName=="java.lang.String")
                {
                    int templength = 0;

                    var value = PrimitiveTypeHelper.GetPrimitiveValue(PrimitiveType.HPROF_BASIC_OBJECT, org.InstanceFieldData, ref templength);
                    InstanceFields.Add(new ReferenceObjectInfo()
                    {
                        Name = "data",
                        NameID = 0,
                        Type = PrimitiveType.HPROF_BASIC_OBJECT,
                        Value = value,
                    });
                }

            };
            #endregion
        }
    }
}
