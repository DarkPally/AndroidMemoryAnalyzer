using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
using BigEndianExtension;
using System.IO;

namespace AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs
{
    static class HeapObjectDeserializer
    {
        //同样统一不读取tag        
        public static HeapInfo Deserialize(this HeapInfo input, byte[] data,ref int startIndex)
        {
            input.HeapID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.HeapNameID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            return input;
        }
        public static DumpObjectArray Deserialize(this DumpObjectArray input, byte[] data, ref int startIndex)
        {
            input.ObjectArrayID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.StackTraceSerialNumber = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Length = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.ClassObjectID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.ElementIDs = new List<int>();
            for(int i=0;i<input.Length;++i)
            {
                var eID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
                startIndex += 4;
                input.ElementIDs.Add(eID);
            }

            return input;
        }
        public static DumpObjectInstance Deserialize(this DumpObjectInstance input, byte[] data, ref int startIndex)
        {
            input.ObjectID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.StackTraceSerialNumber = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.ClassObjectID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Length = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.InstanceFieldData = data.Skip(startIndex).Take(input.Length).ToArray();
            startIndex += input.Length;

            return input;
        }       
        public static DumpPrimitiveArray Deserialize(this DumpPrimitiveArray input, byte[] data, ref int startIndex)
        {
            input.PrimitiveArrayID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.StackTraceSerialNumber = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Length = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Type = (PrimitiveType)data[startIndex];
            startIndex += 1;

            var size = PrimitiveTypeHelper.GetLength(input.Type) * input.Length;

            input.ElementDatas = data.Skip(startIndex).Take(size).ToArray();
            startIndex += size;

            return input;
        }
        public static DumpPrimitiveArrayNoData Deserialize(this DumpPrimitiveArrayNoData input, byte[] data, ref int startIndex)
        {
            input.PrimitiveArrayID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.StackTraceSerialNumber = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Length = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            startIndex += 1;
            return input;
        }

        
        public static DumpClassObject Deserialize(this DumpClassObject input, byte[] data, ref int startIndex,HeapDumpSegment heap)
        {
            input.ClassObjectID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.StackTraceSerialNumber = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.SuperClassObjectID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.ClassLoaderID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Signer = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.ProtDomain = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.Reserved = BitConverter.ToInt64(data, startIndex).FromBigEndian();
            startIndex += 8;
            input.InstanceSize = BitConverter.ToInt32(data, startIndex).FromBigEndian();
            startIndex += 4;
            input.EmptyConstPool = BitConverter.ToInt16(data, startIndex).FromBigEndian();
            startIndex += 2;

            #region StaticFieldContent
            input.StaticFieldContent = new StaticFieldContent();
            input.StaticFieldContent.Count_Plused = BitConverter.ToInt16(data, startIndex).FromBigEndian();
            startIndex += 2;
            if (input.StaticFieldContent.Count_Plused!=0)
            {
                var t = heap.HeapDumpObjects.Last();
                if (t.Tag == DumpObjectTag.PRIMITIVE_ARRAY_WITH_DATA)
                {
                    input.StaticFieldStruct = heap.HeapDumpObjects.Last() as DumpPrimitiveArray;          
                } 
                else
                {
                    throw new Exception("StaticFieldStruct加载错误");
                }
                input.StaticFieldContent.StaticStringID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
                startIndex += 4;
                input.StaticFieldContent.ObjectType = (PrimitiveType)data[startIndex];
                startIndex += 1;
                input.StaticFieldContent.ClassObjectID_Plused = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
                startIndex += 4;
                input.StaticFieldContent.StaticFields = new List<StaticFieldContent.StaticField>();
                
                for(short i=0;i<input.StaticFieldContent.Count;++i)
                {
                    var tempStaticField = new StaticFieldContent.StaticField();
                    tempStaticField.NameID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
                    startIndex += 4;
                    tempStaticField.Type = (PrimitiveType)data[startIndex];
                    startIndex += 1;
                    tempStaticField.Value = PrimitiveTypeHelper.GetPrimitiveValue(tempStaticField.Type,data,ref startIndex);
                    input.StaticFieldContent.StaticFields.Add(tempStaticField);
                }
            }
            #endregion

            #region InstanceFieldContent
            input.InstanceFieldContent = new InstanceFieldContent();
            input.InstanceFieldContent.Count = BitConverter.ToInt16(data, startIndex).FromBigEndian();
            startIndex += 2;
            if (input.InstanceFieldContent.Count != 0)
            {
                input.InstanceFieldContent.InstanceFields = new List<InstanceFieldContent.InstanceField>();

                for (short i = 0; i < input.InstanceFieldContent.Count; ++i)
                {
                    var tempInstanceField = new InstanceFieldContent.InstanceField();
                    tempInstanceField.NameID = (int)BitConverter.ToInt32(data, startIndex).FromBigEndian();
                    startIndex += 4;
                    tempInstanceField.Type = (PrimitiveType)data[startIndex];
                    startIndex += 1;
                    input.InstanceFieldContent.InstanceFields.Add(tempInstanceField);
                }
            }
            #endregion

            return input;
        }
  
  
    }
}
