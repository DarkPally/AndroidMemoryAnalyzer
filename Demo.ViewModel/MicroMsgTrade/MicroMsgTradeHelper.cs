using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.MicroMsgTrade
{
    public class MicroMsgTradeHelper
    {
        public static string GetJavaString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if (tt1.ReferenceTarget == null) return null;
            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
            return str_value;
        }

        public static List<MicroMsgTradeMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<MicroMsgTradeMessage> result = new List<MicroMsgTradeMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "android.webkit.WebViewCore$WebKitHitTest").ToList();

            foreach (var it in t)
            {
                var msg = new MicroMsgTradeMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "mAnchorText":
                            msg.Message = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        
                        default:
                            break;
                    }
                }
                if(msg.Message!=null)
                {
                    result.Add(msg);
                }
            }
            return result;
        }

    }
}
