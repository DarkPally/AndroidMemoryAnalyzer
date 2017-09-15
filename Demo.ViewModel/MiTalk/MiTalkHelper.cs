using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.MiTalk
{
    public class MiTalkHelper
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

        public static List<MiTalkMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<MiTalkMessage> result = new List<MiTalkMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.xiaomi.channel.account.MLAccount").ToList();

            foreach (var it in t)
            {
                var msg = new MiTalkMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "fullName":
                            msg.FullName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "nickname":
                            msg.NickName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "username":
                            msg.UserName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "password":
                            msg.Password = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
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
