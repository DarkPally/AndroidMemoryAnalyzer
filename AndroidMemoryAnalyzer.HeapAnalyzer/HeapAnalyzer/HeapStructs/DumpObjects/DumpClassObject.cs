using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public class StaticFieldContent
    {
        public short Count_Plused { get; set; }
        public int StaticStringID { get; set; }
        public PrimitiveType ObjectType { get; set; }
        public int ClassObjectID_Plused { get; set; }
        public class StaticField
        {
            public int NameID { get; set; }
            public PrimitiveType Type { get; set; }
            public object Value { get; set; }
        }
        public List<StaticField> StaticFields { get; set; }
        public short Count { get { return (short)(Count_Plused - 1); } }
        public int ClassObjectID { get { return ClassObjectID_Plused - 1; } }
    }
    public class InstanceFieldContent
    {
        public short Count { get; set; }
        public class InstanceField
        {
            public int NameID { get; set; }
            public PrimitiveType Type { get; set; }
        }
        public List<InstanceField> InstanceFields { get; set; }
    }
    public class DumpClassObject : HeapDumpObject
    {
        override public DumpObjectTag Tag 
        {
            get { return DumpObjectTag.CLASS_OBJECT; } 
        }
        public DumpPrimitiveArray StaticFieldStruct { get; set; }
        public int ClassObjectID { get; set; }
        public int StackTraceSerialNumber { get; set; }
        public int SuperClassObjectID { get; set; }
        public int ClassLoaderID { get; set; }
        public int Signer { get; set; }
        public int ProtDomain { get; set; }
        public long Reserved { get; set; }
        public int InstanceSize { get; set; }
        public short EmptyConstPool { get; set; }
        public StaticFieldContent StaticFieldContent { get; set; }
        public InstanceFieldContent InstanceFieldContent { get; set; }
    }
}
