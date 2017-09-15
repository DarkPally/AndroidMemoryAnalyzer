using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.BaiduPan
{
    public class BaiduPanHelper
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

        public static List<BaiduPanMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<BaiduPanMessage> result = new List<BaiduPanMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.baidu.netdisk.account.model.CloudUserInfoBean").ToList();

            foreach (var it in t)
            {
                var msg = new BaiduPanMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "mSecureMobil":
                            msg.Phone = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "mNickName":
                            msg.NickName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "mSecureEmail":
                            msg.Email = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "mUName":
                            msg.ID = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "mPriorityName":
                            msg.UserName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
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
