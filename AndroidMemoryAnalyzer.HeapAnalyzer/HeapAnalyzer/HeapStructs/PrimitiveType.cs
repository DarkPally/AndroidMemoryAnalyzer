using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigEndianExtension;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    public enum PrimitiveType
    {
        HPROF_BASIC_OBJECT = 2,
        HPROF_BASIC_BOOLEAN = 4,
        HPROF_BASIC_CHAR = 5,
        HPROF_BASIC_FLOAT = 6,
        HPROF_BASIC_DOUBLE = 7,
        HPROF_BASIC_BYTE = 8,
        HPROF_BASIC_SHORT = 9,
        HPROF_BASIC_INT = 10,
        HPROF_BASIC_LONG = 11,
    }

    public static class PrimitiveTypeHelper
    {
        
       static int[] sizes = { -1, -1, 4, -1, 1, 2, 4, 8, 1, 2, 4, 8  };
       public static int GetLength(PrimitiveType basicType)
       {
           if ((int)basicType >= sizes.Count())
                return -1;
           return sizes[(int)basicType];
       }

       public static object GetPrimitiveValue(PrimitiveType basicType,byte[] data,ref int startIndex)
       {
           var length = PrimitiveTypeHelper.GetLength(basicType);
           object value=null;
           switch (length)
           {
               case 1:
                   value = data[startIndex];
                   break;
               case 2:
                   value = BitConverter.ToInt16(data, startIndex).FromBigEndian();
                   break;
               case 4:
                   value = BitConverter.ToInt32(data, startIndex).FromBigEndian();
                   break;
               case 8:
                   value = BitConverter.ToInt64(data, startIndex).FromBigEndian();
                   break;
               default:
                   throw new Exception("StaticField类型错误");
           }
           startIndex += length;
           return value;
       }
    }
}
