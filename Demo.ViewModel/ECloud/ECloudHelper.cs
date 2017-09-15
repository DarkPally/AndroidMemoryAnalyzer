using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.ECloud
{
    public class ECloudHelper
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

        public static List<ECloudMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<ECloudMessage> result = new List<ECloudMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.cn21.ecloud.analysis.bean.File").ToList();

            foreach (var it in t)
            {
                var msg = new ECloudMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "_createDate":
                            msg.CreateTime = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "_name":
                            msg.FileName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "_lastOpTime":
                            msg.LastOperateTime = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        default:
                            break;
                    }
                }
                result.Add(msg);
            }
            return result;
        }

        public static string GetAccount(HeapFileAnalyzer analyser)
        {
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.cn21.ecloud.analysis.bean.UserInfo").ToList();

            foreach (var it in t)
            {
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "_loginName":
                            var str= GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            if (str!=null)
                            {
                                return str;
                            }
                            break;                        
                        default:
                            break;
                    }
                }
            }
            return null;
        }

    }
}
