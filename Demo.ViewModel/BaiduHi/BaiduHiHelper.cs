using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.BaiduHi
{
    public class BaiduHiHelper
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

        public static List<BaiduHiMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<BaiduHiMessage> result = new List<BaiduHiMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "java.util.Formatter").ToList();

            foreach (var it in t)
            {
                var msg = new BaiduHiMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "arg":
                            var str = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            if (str != null && str.Contains("baiduid") && str.Contains("nickname"))
                            {
                                msg.Json = str;
                            }
                            break;
                        default:
                            break;
                    }
                }
                if (msg.Json!=null)
                {
                    result.Add(msg);
                }
            }
            return result;
        }

    }
}
