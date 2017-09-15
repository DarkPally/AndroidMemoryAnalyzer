using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.BrowserQQ
{
    public class BrowserQQHelper
    {
        public static string GetJavaString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
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

        public static List<BrowserQQMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<BrowserQQMessage> result = new List<BrowserQQMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mtt.browser.history.History").ToList();

            foreach (var it in t)
            {
                var msg = new BrowserQQMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "name":
                            msg.Name = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "url":
                            msg.Url = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
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
