using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AndroidMemoryAnalyzer.HeapAnalyzer.HeapStructs;
namespace AndroidMemoryAnalyzer.HeapAnalyzer
{
    
    public class PrimitiveArrayInfo
    {
        public int PrimitiveArrayID { get; set; }
        public PrimitiveType Type { get; set; }
        public List<object> ElementDatas { get; set; }
        public string StringData { get; set; }
        public PrimitiveArrayInfo(DumpPrimitiveArray org)            
        {
            PrimitiveArrayID = org.PrimitiveArrayID;

            Type = org.Type;

            ElementDatas = new List<object>();
            for (var start = 0; start < org.ElementDatas.Length;)
            {
                var t = PrimitiveTypeHelper.GetPrimitiveValue(org.Type, org.ElementDatas, ref start);
                ElementDatas.Add(t);
            }
            if(Type==PrimitiveType.HPROF_BASIC_CHAR)
            {
                if(ElementDatas.Count>0)
                {
                    //if (ElementDatas.Exists(c=>(short)c>255 || (short)c<0))//此处为Unicode编码
                    {
                        var temp = ElementDatas.Select(c => (short)c).ToArray();
                        var t2 = ByteConverter.Arr2Arr<short, byte>(temp);
                        StringData = Encoding.Unicode.GetString(t2);
                    }
                   // else//此处为ASCII编码
                   // {
                   //     var temp = ElementDatas.Select(c => (byte)(short)c).ToArray();
                   //     StringData = Encoding.Default.GetString(temp);
                   // }
                }
                else
                {
                    StringData = "";
                }
            }
        }
    }
}
