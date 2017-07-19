using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Library.ViewModel
{
    public class MainViewModel
    {
        public QQViewModel QQViewModel { get; set; }
        public MicroMsgViewModel MicroMsgViewModel { get; set; }
        

        private static readonly MainViewModel instance = new MainViewModel();
        public static MainViewModel GetInstance() { return instance; }
        private MainViewModel()
        {
            MicroMsgViewModel = new MicroMsgViewModel();
            QQViewModel = new QQViewModel();
        }
    }
}
