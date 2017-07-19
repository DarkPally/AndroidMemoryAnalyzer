using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public enum DumpObjectTag
    {
        CLASS_OBJECT = 0x20,//可能也是0x23
        OBJECT_INSTANCE = 0x21,
        OBJECT_ARRAY = 0x22,
        PRIMITIVE_ARRAY_WITH_DATA  = 0x23,
        PRIMITIVE_ARRAY_WITHOUT_DATA = 0xc3,

        HEAP_INFO = 0xfe
    }
    public class HeapDumpObject
    {
        virtual public DumpObjectTag Tag { get { return 0; } }
    }
}
