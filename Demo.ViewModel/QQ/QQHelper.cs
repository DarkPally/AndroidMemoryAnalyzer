using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.QQ
{
    public class QQHelper
    {
        public static string GetJavaString(ObjectInstanceInfo info)
        {
            if (info == null) return null;
            if (info.ClassName != "java.lang.String") return null;
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if (tt1.ReferenceTarget == null) return null;
            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
            return str_value;
        }
        public static List<QQGroup> GetGroupWithFriends(HeapFileAnalyzer analyser)
        {
            var fr = GetFriends(analyser);
            var gr = GetGroups(analyser);
            foreach (var it in gr)
            {
                it.GroupFriends = fr.Where(c => c.GroupID == it.GroupID).ToList();
            }
            return gr;
        }
        public static List<QQFriend> GetFriends(HeapFileAnalyzer analyser)
        {
            List<QQFriend> result = new List<QQFriend>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mobileqq.data.Friends").ToList();
            
            foreach (var it in t)
            {
                var friend = new QQFriend();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "uin":
                            friend.UIN = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "name":
                            friend.Name = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "remark":
                            friend.Remark = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "gender":
                            friend.Gender = (byte)it2.Value;
                            break;
                        case "age":
                            friend.Age = (int)it2.Value;
                            break;
                        case "groupid":
                            friend.GroupID = (int)it2.Value;
                            break;
                        default:
                            break;
                    }
                }
                result.Add(friend);
            }
            return result;
        }
        public static List<QQGroup> GetGroups(HeapFileAnalyzer analyser)
        {
            List<QQGroup> result = new List<QQGroup>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mobileqq.data.Groups").ToList();
            foreach (var it in t)
            {
                var group = new QQGroup();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "group_id":
                            group.GroupID = (int)it2.Value;
                            break;
                        case "group_name":
                            group.Name = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "group_friend_count":
                            group.GroupFriendCount = (int)it2.Value;
                            break;
                        case "group_online_friend_count":
                            group.GroupOnlineFriendCount = (int)it2.Value;
                            break;
                        default:
                            break;
                    }
                }
                result.Add(group);
            }
            return result;
        }
        public static List<QQMessage> GetMessages(HeapFileAnalyzer analyser)
        {
            List<QQMessage> result = new List<QQMessage>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mobileqq.app.message.QQMessageFacade$Message").ToList();

            foreach (var it in t)
            {
                var msg = new QQMessage();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "nickName":
                            msg.NickName = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "senderuin":
                            msg.SenderUIN = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "selfuin":
                            msg.SelfUIN = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "frienduin":
                            msg.FriendUIN = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "msg":
                            msg.Msg = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "time":
                            msg.Time = (long)it2.Value;
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
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "java.util.HashMap$HashMapEntry" ).ToList();

            foreach (var it in t)
            {

                string tempKey = "";
                string tempValue = "";
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "key":
                            tempKey = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);

                            break;
                        case "value":
                            tempValue = GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        
                        default:
                            break;
                    }
                }
                if(tempKey== "QQUni")
                {
                    return tempValue;
                }
            }
            return null;
        }
    }
}
