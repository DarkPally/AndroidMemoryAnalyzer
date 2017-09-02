using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Prism.Mvvm;
using Prism.Commands;

using Demo.Library.Mail189;
using AndroidMemoryAnalyzer.HeapAnalyzer;

namespace Demo.Library.ViewModel
{

    public class Mail189ViewModel : BindableBase
    {

        List<Mail189Message> msgs;
        public List<Mail189Message> Msgs
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
                        Msgs = Mail189Helper.GetMessages(x);
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
