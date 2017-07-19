using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
using BigEndianExtension;
using System.IO;

namespace AndroidMemoryAnalyzer
{
    static class HeapDeserializer
    {
        public static HeapHeader Deserialize(this HeapHeader input, BinaryReader br)
        {
            var temp = input;

            var stemp = br.ReadChar();
            temp.Magic = stemp.ToString();
            while (stemp != 0)
            {
                stemp = br.ReadChar();
                temp.Magic += stemp;
            }
            temp.IDSize = br.ReadInt32().FromBigEndian();
            temp.TimeTicks = br.ReadInt64().FromBigEndian();

            return temp;
        }
        public static HeapRawData Deserialize(this HeapRawData input, BinaryReader br)
        {
            input.TimeTicks = br.ReadInt32().FromBigEndian();
            input.Length = br.ReadInt32().FromBigEndian();
            input.RawData = br.ReadBytes(input.Length);

            return input;
        }
       
        //统一不在方法内读TAG，即已经在判断时读过了
        public static HeapString Deserialize(this HeapString input, BinaryReader br)
        {
            (input as HeapRawData).Deserialize(br);
            input.StringID = (int)BitConverter.ToInt32(input.RawData,0).FromBigEndian();
            input.StringData = Encoding.UTF8.GetString(input.RawData, 4, input.RawData.Length - 4);

            return input;
        }
        public static HeapClass Deserialize(this HeapClass input, BinaryReader br)
        {
            (input as HeapRawData).Deserialize(br);
            input.SerialNumber = (int)BitConverter.ToInt32(input.RawData, 0).FromBigEndian();
            input.ObjectID = (int)BitConverter.ToInt32(input.RawData, 4).FromBigEndian();
            input.StackTrace = (int)BitConverter.ToInt32(input.RawData, 8).FromBigEndian();
            input.NameID = (int)BitConverter.ToInt32(input.RawData, 12).FromBigEndian();
            return input;
        }
        public static HeapStack Deserialize(this HeapStack input, BinaryReader br)
        {
            (input as HeapRawData).Deserialize(br);
            input.StactTrace = (int)BitConverter.ToInt32(input.RawData, 0).FromBigEndian();
            input.Thread = (int)BitConverter.ToInt32(input.RawData, 4).FromBigEndian();
            input.NoFrames = (int)BitConverter.ToInt32(input.RawData, 8).FromBigEndian();
            return input;
        }
        public static HeapDumpSegment Deserialize(this HeapDumpSegment input, BinaryReader br)
        {
            (input as HeapRawData).Deserialize(br);
            if (input.RawData[0] == (byte)DumpObjectTag.HEAP_INFO)
            {
                input.SegmentType = DumpSegmentType.ObjectInstance;

                int startIndex = 1; //原本正常的情况
                input.HeapInfo = new HeapInfo().Deserialize(input.RawData, ref startIndex); //原本正常的情况
                input.HeapDumpObjects = new List<HeapDumpObject>();
                //int startIndex = 0; //学长的特殊情况，不导出HeapInfo
                while(startIndex<input.RawData.Length)
                {
                    HeapDumpObject newObject=null;
                    var flag = input.RawData[startIndex];
                    ++startIndex;
                    switch (flag)
                    {
                        case (byte)DumpObjectTag.CLASS_OBJECT:
                            newObject = new DumpClassObject().Deserialize(input.RawData, ref startIndex, input);
                            break;
                        case (byte)DumpObjectTag.OBJECT_ARRAY:
                            newObject=new DumpObjectArray().Deserialize(input.RawData,ref startIndex);
                            break;
                        case (byte)DumpObjectTag.OBJECT_INSTANCE:
                            newObject=new DumpObjectInstance().Deserialize(input.RawData,ref startIndex);
                            break;
                        case (byte)DumpObjectTag.PRIMITIVE_ARRAY_WITH_DATA:
                            newObject=new DumpPrimitiveArray().Deserialize(input.RawData,ref startIndex);
                            break;
                        case (byte)DumpObjectTag.PRIMITIVE_ARRAY_WITHOUT_DATA:
                            newObject=new DumpPrimitiveArrayNoData().Deserialize(input.RawData,ref startIndex);
                            break;
                        default:
                            throw new Exception("尚未处理的Dump类型: " + input.RawData[startIndex]);

                    }
                    input.HeapDumpObjects.Add(newObject); 
                }

            }
            else
            {
                input.SegmentType = DumpSegmentType.RootSet;
            }
            return input;
        }
    }
}
