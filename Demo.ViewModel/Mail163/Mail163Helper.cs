using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.Mail163
{
    public class Mail163Helper
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

        static void GetMessageSender(ReferenceObjectInfo senderRefer,ref Mail163Message msg)
        {
            if(senderRefer.ReferenceTarget!=null)
            {
                var t = senderRefer.ReferenceTarget as ObjectInstanceInfo;
                foreach (var it2 in t.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "a":
                            msg.SenderName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "b":
                            msg.SenderEmail = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        public static string GetAccount(HeapFileAnalyzer analyser)
        {
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.netease.mobimail.module.az.a.a").ToList();

            foreach (var it in t)
            {
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "a":
                            var tttt = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            if(tttt!=null)
                            {
                                return tttt;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
            return null;
        }
        public static List<Mail163Message> GetMessages(HeapFileAnalyzer analyser)
        {
            List<Mail163Message> result = new List<Mail163Message>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.netease.mobimail.n.c.am").ToList();

            foreach (var it in t)
            {
                var msg = new Mail163Message();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "t":
                            msg.Content = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "s":
                            msg.Theme = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "n":
                            GetMessageSender(it2 as ReferenceObjectInfo, ref msg);
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
