using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.LaiWang
{
    public class LaiWangHelper
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

        public static List<LaiWangMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<LaiWangMessage> result = new List<LaiWangMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "java.util.HashMap$HashMapEntry").ToList();

            var msg = new LaiWangMessage();
            foreach (var it in t)
            {
                string tempValue = null;
                string tempKey = null;
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "value":
                            tempValue = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "key":
                            tempKey = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        default:
                            break;
                    }
                }
                if(tempKey=="name")
                {
                    msg.Name = tempValue;
                }
                else if (tempKey == "mobile")
                {
                    msg.Phone = tempValue;
                }
                else if(tempKey == "loginId")
                {
                    msg.LoginID = tempValue;
                }
            }

            result.Add(msg);
            return result;
        }

    }
}
