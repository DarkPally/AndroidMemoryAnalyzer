using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace AndroidMemoryAnalyzer
{
    class Program
    {
        static void Main(string[] args)
        {
            //string path = @"F:\工作项目\内存提取\test\com.tencent.mobileqq.hprof";
            //string path = @"F:\工作项目\内存提取\test\qq进程内存测试分析\联系人界面\com_tencent_mobileqq.hprof";
            string path = @"F:\工作项目\内存提取\test\qq进程内存测试分析\消息界面\com.tencent.mobileqq.hprof";
            //string path = @" F:\工作项目\内存提取\test\微信进程内存测试分析\com.tencent.mm.hprof";
            //AnalysisManager HeapAnalyzer = new AnalysisManager(path,null);
            //HeapAnalyzer.DoWork();
           
            HeapFileAnalyzer x = new HeapFileAnalyzer(path);

            x.DoWork();
            lookForQQText(x);
            //lookForMessageHost(x);
            //lookForMessage(x);
           // PrintGroupFriend(x);
            Console.ReadLine();
        }
        static void PrintMicroMsgFriends(HeapFileAnalyzer analyser)
        {
            //ClassName = "com.tencent.mobileqq.text.QQText"

            //ClassName = "com.tencent.mm.storage.f"
            //ClassName = "com.tencent.mm.storage.w"
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mm.storage.w").ToList();
            var t2 = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mm.storage.f").ToList();
            using (FileStream fsWrite = new FileStream(@"result_mm_friends_w.txt", FileMode.OpenOrCreate))
            {
                foreach (var it in t)
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);

                    byte[] myByte_ClassName = System.Text.Encoding.UTF8.GetBytes(it.ClassName + "\r\n");
                    fsWrite.Write(myByte_ClassName, 0, myByte_ClassName.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item = null;
                        if (it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if (temp != null)
                            {
                                if (temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                        if (tt1.ReferenceTarget == null)
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                        }
                                        else
                                        {
                                            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                                else
                                {
                                    item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(others)");
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }

                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            };

        }
        static void lookForMicroMsgFriends(HeapFileAnalyzer analyser)
        {
            //ClassName = "com.tencent.mobileqq.text.QQText"
            var t = analyser.PrimitiveArrayInfos.Where(c => c.StringData != null && c.StringData.Contains("把酒祝东风")).ToList();
            List<ObjectInstanceInfo> tstring = new List<ObjectInstanceInfo>();
            foreach (var it in t)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.PrimitiveArrayID)
                        )
                ).FirstOrDefault();
                tstring.Add(temp);
            }
            //ClassName = "com.tencent.mm.storage.f"
            //ClassName = "com.tencent.mm.storage.w"
            var tObjectInfo = new List<List<ObjectInstanceInfo>>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectInfo.Add(temp);
            }

            using (FileStream fsWrite = new FileStream(@"result_mm_friends2.txt", FileMode.OpenOrCreate))
            {
                foreach (var it in tObjectInfo[1])
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);

                    byte[] myByte_ClassName = System.Text.Encoding.UTF8.GetBytes(it.ClassName + "\r\n");
                    fsWrite.Write(myByte_ClassName, 0, myByte_ClassName.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item = null;
                        if (it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if (temp != null)
                            {
                                if (temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                        if (tt1.ReferenceTarget == null)
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                        }
                                        else
                                        {
                                            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                                else
                                {
                                    item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(others)");
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }

                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            };

        }
        static void lookForQQText(HeapFileAnalyzer analyser)
        {
            var t = analyser.ObjectInstanceInfos
                .Where(c => c.ClassName != null
                    && c.ClassName.Equals("com.tencent.mobileqq.app.message.QQMessageFacade$Message"))
                    .ToList();
            using (FileStream fsWrite = new FileStream(@"result_msg.txt", FileMode.OpenOrCreate))
            {
                foreach (var it in t)
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);

                    //byte[] myByte_ClassName = System.Text.Encoding.UTF8.GetBytes(it.ClassName + "\r\n");
                    //fsWrite.Write(myByte_ClassName, 0, myByte_ClassName.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item = null;
                        if (it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if (temp != null)
                            {
                                if (temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                        if (tt1.ReferenceTarget == null)
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                        }
                                        else
                                        {
                                            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                                else
                                {
                                    item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(others)");
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }

                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            };
        }
        static void lookForMessage2(HeapFileAnalyzer analyser)
        {
            //com.etrump.mixlayout.ETFragment
            //com.etrump.mixlayout.ETParagraph
            //com.tencent.mobileqq.app.message.QQMessageFacade$Message
            //ClassName = "com.tencent.mobileqq.text.QQText"
            var t = analyser.PrimitiveArrayInfos.Where(c => c.StringData != null
                && c.StringData.Contains("[图片]")).ToList();
            List<ObjectInstanceInfo> tstring = new List<ObjectInstanceInfo>();
            foreach (var it in t)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.PrimitiveArrayID)
                        )
                ).FirstOrDefault();
                if (temp!=null)
                tstring.Add(temp);
            }

            var tObjectInfo = new List<List<ObjectInstanceInfo>>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectInfo.Add(temp);
            }

            using (FileStream fsWrite = new FileStream(@"result_msg.txt", FileMode.OpenOrCreate))
            {
                foreach (var it_Main in tObjectInfo)
                {
                    foreach (var it in it_Main)
                    {
                        string line = "———————————————— \r\n";
                        byte[] myByte_line = System.Text.Encoding.Default.GetBytes(line);
                        fsWrite.Write(myByte_line, 0, myByte_line.Length);

                        byte[] myByte_ClassName = System.Text.Encoding.Default.GetBytes(it.ClassName + "\r\n");
                        fsWrite.Write(myByte_ClassName, 0, myByte_ClassName.Length);
                        foreach (var it2 in it.InstanceFields)
                        {
                            string item = null;
                            if (it2 is ReferenceObjectInfo)
                            {
                                var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                                if (temp != null)
                                {
                                    if (temp is ObjectInstanceInfo)
                                    {
                                        var cn = (temp as ObjectInstanceInfo).ClassName;
                                        if (cn == "java.lang.String")
                                        {
                                            var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                            if (tt1.ReferenceTarget == null)
                                            {
                                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                            }
                                            else
                                            {
                                                var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                            }
                                        }
                                        else
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(others)");
                                    }
                                }
                                else
                                {
                                    item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                            }

                            byte[] myByte = System.Text.Encoding.Default.GetBytes(item);

                            fsWrite.Write(myByte, 0, myByte.Length);
                        }
                    }
                };

            };

        }
        static void lookForMessageHost(HeapFileAnalyzer analyser)
        {
            
            

            /*
            int id=-1495901704;
            var t = analyser.ObjectArrayInfos.
                Where(c => c.ElementIDs != null
                        && c.ElementIDs.Exists(i => i.Equals(id))
                        ).ToList();
            
            //ObjectArrayID = -1489440640
            //ObjectID = -1489442520
            //ObjectID = -1498294296
            //ObjectID = -1486030896
            var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(-1486030896)
                        )
                ).ToList();
            

            
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassObject != null &&
                //c.ClassName != "com.tencent.mobileqq.data.MessageForText" &&
                (c.ClassObject as ClassObjectInfo).SuperClassName!=null &&
                (c.ClassObject as ClassObjectInfo).SuperClassName.Equals("com.tencent.mobileqq.data.ChatMessage")).ToList();
            */

            /*
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName.Contains("Friends")).ToList();
            using (FileStream fsWrite = new FileStream(@"result_friends_test.txt", FileMode.OpenOrCreate))
            {
                foreach (var it in t)
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item = null;
                        if (it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if (temp != null)
                            {
                                if (temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                        if (tt1.ReferenceTarget == null)
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                        }
                                        else
                                        {
                                            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                                else
                                {
                                    item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(others)");
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }

                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            };
            */
            //只有Friends[]
            var t22 = analyser.ObjectInstanceInfos.Where(c=>c.ClassName!=null &&
                c.ClassName.Contains(".Friends")
                ).ToList();

            t22 = analyser.ObjectInstanceInfos.Where(c => c.ClassName != null &&
                c.ClassName.Contains(".Message")
                ).ToList();

            var t23 = analyser.ClassObjectInfos.Where(c => c.Value.ClassName != null &&
                c.Value.ClassName.Contains(".Message")
                ).ToList();


            var t = analyser.PrimitiveArrayInfos.
                    Where(c => c.StringData != null && c.StringData.Contains("笑笑巫")


                ).ToList();

            List<ObjectInstanceInfo> tstring = new List<ObjectInstanceInfo>();
            foreach (var it in t)
            {
                var temp_x = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.PrimitiveArrayID)
                        )
                ).ToList();
                var temp = temp_x.FirstOrDefault();
                tstring.Add(temp);
            }

            var tObjectInfo = new List<List<ObjectInstanceInfo>>();
            // List<string> tObjectInfoPropertyName = new List<string>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectInfo.Add(temp);
            }

            var tObjectArray = new List<List<ObjectArrayInfo>>();
            // List<string> tObjectInfoPropertyName = new List<string>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectArrayInfos.
                    Where(c => c.ElementIDs != null
                        && c.ElementIDs.Exists(i => i.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectArray.Add(temp);
            }
        }
        static void lookForMessage(HeapFileAnalyzer analyser)
        {            
            //ClassName = "com.tencent.mobileqq.data.MessageForText"
            //Super:com.tencent.mobileqq.data.ChatMessage
            //ObjectID = -1495901704
            var t = analyser.PrimitiveArrayInfos.Where(c => c.StringData != null && c.StringData.Contains("有人昨天晚上在接驳车上捡到一卡通吗")).ToList();
            List<ObjectInstanceInfo> tstring = new List<ObjectInstanceInfo>();
            foreach (var it in t)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.PrimitiveArrayID)
                        )
                ).FirstOrDefault();
                tstring.Add(temp);
            }

            var tObjectInfo = new List<List<ObjectInstanceInfo>>();
            // List<string> tObjectInfoPropertyName = new List<string>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectInfo.Add(temp);
            }

            using (FileStream fsWrite = new FileStream(@"result_msg.txt", FileMode.OpenOrCreate))
            {
                foreach (var it in tObjectInfo[0])
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item = null;
                        if (it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if (temp != null)
                            {
                                if (temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                        if (tt1.ReferenceTarget == null)
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                        }
                                        else
                                        {
                                            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                                else
                                {
                                    item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(others)");
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }

                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            };

        }
        static string GetJavaString(ObjectInstanceInfo info)
        {
            if (info == null) return "(null)";                    
            var tt1 = ((info as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
            if(tt1.ReferenceTarget==null) return "(null)";
            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
            return str_value;                   
        }
        static void PrintGroupFriend(HeapFileAnalyzer analyser)
        {
            var gr = GetGroupWithFriends(analyser);
            using (FileStream fsWrite = new FileStream(@"result.txt", FileMode.OpenOrCreate))
            {
                XmlSerializer xml = new XmlSerializer(gr.GetType());
                xml.Serialize(fsWrite, gr);
            }
        }
        static List<QQGroup> GetGroupWithFriends(HeapFileAnalyzer analyser)
        {
            var fr = GetFriends(analyser);
            var gr = GetGroups(analyser);
            foreach(var it in gr)
            {
                it.GroupFriends = fr.Where(c => c.GroupID == it.GroupID).ToList();
            }
            return gr;
        }
        static List<QQFriend> GetFriends(HeapFileAnalyzer analyser)
        {
            List<QQFriend> result=new List<QQFriend>();
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mobileqq.data.Friends").ToList();
            //var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName.Contains("Friends")).ToList();
            //var t2 = analyser.ClassObjectInfos.Where(c => c.Value.ClassName != null && c.Value.ClassName.Contains("Black")).ToList();
            
            foreach (var it in t)
            {
                var friend = new QQFriend();
                foreach (var it2 in it.InstanceFields)
                {
                    switch (it2.Name)
                    {
                        case "uin":
                            friend.UIN=GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "name":
                            friend.Name=GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
                            break;
                        case "remark":
                            friend.Remark=GetJavaString((it2 as ReferenceObjectInfo).ReferenceTarget as ObjectInstanceInfo);
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
        static List<QQGroup> GetGroups(HeapFileAnalyzer analyser)
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
        static void lookForFriends(HeapFileAnalyzer analyser)
        {
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mobileqq.data.Friends").ToList();


            using (FileStream fsWrite = new FileStream(@"result_friends.txt", FileMode.OpenOrCreate))
            {
                foreach (var it in t)
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item = null;
                        if (it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if (temp != null)
                            {
                                if (temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1 = ((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);

                                        if(tt1.ReferenceTarget==null)
                                        {
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "(null)");
                                        }
                                        else
                                        {
                                            var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                            item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                        }
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, "null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }

                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            };

        }
        static void lookForGroups(HeapFileAnalyzer analyser)
        {
            var t = analyser.ObjectInstanceInfos.Where(c => c.ClassName == "com.tencent.mobileqq.data.Groups").ToList();
            
            
            using (FileStream fsWrite = new FileStream(@"result_groups.txt", FileMode.OpenOrCreate))
            {
                foreach(var it in t)
                {
                    string line = "———————————————— \r\n";
                    byte[] myByte_line = System.Text.Encoding.UTF8.GetBytes(line);
                    fsWrite.Write(myByte_line, 0, myByte_line.Length);
                    foreach (var it2 in it.InstanceFields)
                    {
                        string item=null;
                        if(it2 is ReferenceObjectInfo)
                        {
                            var temp = (it2 as ReferenceObjectInfo).ReferenceTarget;
                            if(temp!=null)
                            {
                                if(temp is ObjectInstanceInfo)
                                {
                                    var cn = (temp as ObjectInstanceInfo).ClassName;
                                    if (cn == "java.lang.String")
                                    {
                                        var tt1=((temp as ObjectInstanceInfo).InstanceFields[0] as ReferenceObjectInfo);
                                        
                                        var str_value = (tt1.ReferenceTarget as PrimitiveArrayInfo).StringData;
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, str_value);
                                    }
                                    else
                                    {
                                        item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value, cn);
                                    }
                                }
                            }
                            else
                            {
                                item = String.Format("{0}:{1} ({2}) \r\n", it2.Name, it2.Value,"null");
                            }
                        }
                        else
                        {
                            item = String.Format("{0}:{1} \r\n", it2.Name, it2.Value);
                        }
                        
                        byte[] myByte = System.Text.Encoding.UTF8.GetBytes(item);

                        fsWrite.Write(myByte, 0, myByte.Length);
                    }
                };

            }; 
         
        }

        static void lookForContactInfo_Groups(HeapFileAnalyzer analyser)
        {
            var t = analyser.PrimitiveArrayInfos.Where(c => c.StringData != null && c.StringData.Contains("初中同学")).ToList();

            List<ObjectInstanceInfo> tstring = new List<ObjectInstanceInfo>();
            foreach (var it in t)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.PrimitiveArrayID)
                        )
                ).FirstOrDefault();
                tstring.Add(temp);
            }

            var tObjectInfo = new List<List<ObjectInstanceInfo>>();
            // List<string> tObjectInfoPropertyName = new List<string>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectInfo.Add(temp);
            }
        }
        static void lookForContactInfo_Friends(HeapFileAnalyzer analyser)
        {
            var t = analyser.PrimitiveArrayInfos.Where(c => c.StringData != null && c.StringData.Contains("417065131")).ToList();
            
            List<ObjectInstanceInfo> tstring = new List<ObjectInstanceInfo>();
            foreach(var it in t)
            {
                var temp=analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null 
                        && c.InstanceFields.Exists(i =>i.Value.Equals(it.PrimitiveArrayID)
                        )
                ).FirstOrDefault();
                tstring.Add(temp);
            }

            var tObjectInfo = new List<List<ObjectInstanceInfo>>();
           // List<string> tObjectInfoPropertyName = new List<string>();
            foreach (var it in tstring)
            {
                var temp = analyser.ObjectInstanceInfos.
                    Where(c => c.InstanceFields != null
                        && c.InstanceFields.Exists(i => i.Value.Equals(it.ObjectID)
                        )
                ).ToList();
                tObjectInfo.Add(temp);
            }
        }
    }
}
