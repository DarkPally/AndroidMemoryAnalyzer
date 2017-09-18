using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Commands;

using Demo.Library.QQ;
using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.ViewModel
{
    class QQGroupFriendsComparer : IEqualityComparer<QQGroupFriendsBinder>
    {
        public bool Equals(QQGroupFriendsBinder p1, QQGroupFriendsBinder p2)
        {
            if (p1 == null)
                return p2 == null;
            return p1.ID == p2.ID;
        }

        public int GetHashCode(QQGroupFriendsBinder p)
        {
            if (p == null)
                return 0;
            return p.ID.GetHashCode();
        }
    }
    public class QQGroupFriendsBinder
    {
        public string Name { get; set; }
        public string UIN { get; set; }

        public string ID { get; set; }
        public string GroupID { get; set; }
        public string Remark { get; set; }
        public byte Gender { get; set; }
        public int Age { get; set; }
    }
    public class QQViewModel:BindableBase
    {
        List<QQGroupFriendsBinder> groups;
        public List<QQGroupFriendsBinder> Groups
        {
            get { return groups; }
            set
            {
                if (groups != value)
                {
                    groups = value;
                    RaisePropertyChanged("Groups");
                }
            }
        }

        List<QQMessage> msgs;
        public List<QQMessage> Msgs
        {
            get { return msgs; }
            set
            {
                if (msgs != value)
                {
                    msgs = value;
                    RaisePropertyChanged("Msgs");
                }
            }
        }
        string state="准备就绪";
        public string State {
            get{return state;}
            set
            {
                if (state != value)
                {
                    state = value;
                    RaisePropertyChanged("State");
                }
            } 
        }
        public string FilePath { get; set; }
        public DelegateCommand AnalyzeFile
        {
            get
            {
                return analyzeFile ?? (analyzeFile = new DelegateCommand(ExcuteAnalyzeFile));
            }
        }

        DelegateCommand analyzeFile;
        public void ExcuteAnalyzeFile()
        {
            if (FilePath == "")
            {
                State = "请输入文件路径";
                return;
            }

            State = "解析中...";
            Task.Factory.StartNew(() =>
                {
                    try
                    {
                        HeapFileAnalyzer x = new HeapFileAnalyzer(FilePath);

                        x.DoWork();

                        var group = QQHelper.GetGroups(x);
                        var friends = QQHelper.GetFriends(x);
                        int i = 0;
                        List<QQGroupFriendsBinder> temp =friends.Where(c => c.UIN != null).Select(c=> new QQGroupFriendsBinder()
                            {
                                Name=c.Name,
                                GroupID = "g"+c.GroupID,
                                UIN = c.UIN.Replace("\0", ""),
                                ID = "f" + c.UIN.Replace("\0",""),
                                Age = c.Age,
                                Gender = c.Gender,
                                Remark=c.Remark,
                            }).ToList();

                        var t = group.Where(c => c.Name != null).OrderBy(c=>c.GroupID).Select(c => new QQGroupFriendsBinder()
                            {
                                Name = c.Name,
                                ID = "g" + c.GroupID,
                            });
                        temp.AddRange(t);

                        temp = temp.Distinct(new QQGroupFriendsComparer()).ToList();
                        Groups = temp;

                        Msgs = QQHelper.GetMessages(x);
                        State = "解析完成！";
                    }
                    catch
                    {
                        State = "解析出现异常";
                    }
                    

                });

        }
    }


}
