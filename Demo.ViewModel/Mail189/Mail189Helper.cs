using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.Mail189
{
    public class Mail189Helper
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
        public static string GetAccount(HeapFileAnalyzer analyser)
        {
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.corp21cn.mailapp.MailAccount").ToList();

            foreach (var it in t)
            {
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "mDescription":
                            var tttt = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            if (tttt != null)
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
        public static List<Mail189Message> GetMessages(HeapFileAnalyzer analyser)
        {
            List<Mail189Message> result = new List<Mail189Message>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.corp21cn.mailapp.activity.nr").ToList();

            foreach (var it in t)
            {
                var msg = new Mail189Message();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "aeQ":
                            msg.Content = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "aeP":
                            msg.Theme = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "aeS":
                            msg.Sender = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "aeT":
                            msg.Receiver = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        default:
                            break;
                    }
                }
                result.Add(msg);
            }

            var t2 = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "android.text.Layout$Ellipsizer").ToList();

            foreach (var it in t2)
            {
                var msg = new Mail189Message();
                int temp = 0;
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "mText":
                            msg.Content = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "mWidth":
                            temp = (int)it2.Value;
                            break;
                      
                    }
                }
                if(temp==548)
                {
                    result.Add(msg);
                }
            }

            return result;
        }

    }
}
