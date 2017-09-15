using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.Renren
{
    public class RenrenHelper
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

        public static List<RenrenMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<RenrenMessage> result = new List<RenrenMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.renren.xiaonei.android.profile.ProfileModel").ToList();

            foreach (var it in t)
            {
                var msg = new RenrenMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "coc":
                            msg.School = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "user_name":
                            msg.Name = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "gs":
                            msg.Birthday = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "cob":
                            msg.ExtraInfo = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
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
