using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.YYTalk
{
    public class YYTalkHelper
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

        public static List<YYTalkMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<YYTalkMessage> result = new List<YYTalkMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.yymobile.core.user.UserInfo").ToList();

            foreach (var it in t)
            {
                var msg = new YYTalkMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "userId":
                            msg.UserID = it2.Value.ToString();
                            break;
                        case "birthday":
                            msg.Birthday = it2.Value.ToString();
                            break;
                        case "nickName":
                            msg.Name = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "signature":
                            msg.Signature = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                       case "yyId":
                            msg.YYID = it2.Value.ToString();
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
