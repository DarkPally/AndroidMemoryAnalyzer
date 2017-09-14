using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.BrowserSogou
{
    public class BrowserSogouHelper
    {
        public static string GetJavaString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if (tt1.ReferenceTarget == null) return null;
            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
            return str_value;
        }

        public static string GetTextViewString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
            if (info.ClassName != "android.widget.TextView") return null;
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if (tt1.ReferenceTarget == null) return null;
            var str_value = GetJavaString(tt1.ReferenceTarget as ObjectInstanceInfo);
            return str_value;
        }
            
        public static List<BrowserSogouMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<BrowserSogouMessage> result = new List<BrowserSogouMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "sogou.mobile.explorer.cloud.historys.ui.a$a").ToList();

            foreach (var it in t)
            {
                var msg = new BrowserSogouMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "a":
                            var strName= GetTextViewString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            if (strName != null)
                            {
                                msg.Name = strName;
                            }
                            break;
                        case "b":
                            var strUrl = GetTextViewString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            if (strUrl != null)
                            {
                                msg.Url = strUrl;
                            }
                            break;
                        default:
                            break;
                    }
                }
                result.Add(msg);
            }
            return result;
        }

    }
}
