using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Commands;

using Demo.Library.Mail163;
using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.ViewModel
{

    public class Mail163ViewModel : BindableBase
    {

        List<Mail163Message> msgs;
        public List<Mail163Message> Msgs
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
        string account = "无";
        public string Account
        {
            get { return account; }
            set
            {
                if (account != value)
                {
                    account = value;
                    RaisePropertyChanged("Account");
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
                        Msgs = Mail163Helper.GetMessages(x);
                        Account = Mail163Helper.GetAccount(x);
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
