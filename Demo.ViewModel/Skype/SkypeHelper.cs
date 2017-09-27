using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.Skype
{
    public class SkypeHelper
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

        public static List<SkypeMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<SkypeMessage> result = new List<SkypeMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "java.util.HashMap$HashMapEntry").ToList();

            var msg = new SkypeMessage();
            foreach (var it in t)
            {
                string tempKey = null;
                string tempValue = null;
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "value":
                            tempValue = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);

                            break;
                        case "key":
                            tempKey= GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);

                            break;
                        default:
                            break;
                    }
                }
                if(tempKey== "existingAccounts")
                {
                    msg.Json = tempValue;
                    break;
                }
            }

            result.Add(msg);
            return result;
        }

    }
}
