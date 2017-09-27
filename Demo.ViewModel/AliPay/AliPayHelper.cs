using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.AliPay
{
    public class AliPayHelper
    {
        public static string GetJavaString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
            if (info.ClassName != "java.lang.String") return null;
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if (tt1.ReferenceTarget == null) return null;
            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
            return str_value;
        }

        public static string GetSpannableString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if (tt1.ReferenceTarget == null) return null;
            var str_value = GetJavaString(tt1.ReferenceTarget as ObjectInstanceInfo);
            return str_value;
        }

        public static List<AliPayMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<AliPayMessage> result = new List<AliPayMessage>();
            var tA = analyser.ObjectArrayInfos.Where(c => c.ClassObject!=null && c.ClassObject.ClassName == "java.util.HashMap$HashMapEntry[]").ToList();

            foreach(var t in tA)
            {
                var flag = false;
                string tState = null;
                string tContent = null;
                string tDate = null;
                string tMoney = null;

                foreach (ObjectInstanceInfo it in t.Elements)
                {
                    if (it == null) continue;
                    string tKey = null;
                    string tValue = null;

                    foreach (var it2 in it.InstanceFields)
                    {
                        switch (it2.Name)
                        {
                            case "key":
                                tKey = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                                break;

                            case "value":
                                tValue = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);

                                break;
                            default:
                                break;
                        }
                    }
                    if(tKey== "MCon")
                    {
                        tContent = tValue;
                        flag = true;
                    }
                    else if(tKey== "HDat")
                    {

                        tDate = tValue;
                    }
                    else if (tKey == "HSta")
                    {

                        tState = tValue;
                    }
                    else if (tKey == "HMon")
                    {
                        flag = true;
                        tMoney = tValue;
                    }

                }
                if(flag)
                {

                    result.Add(new AliPayMessage()
                    {
                        State=tState,
                        Content=tContent,
                        Date=tDate,
                        Money=tMoney
                    });
                }

            }
            return result;
        }

    }
}
