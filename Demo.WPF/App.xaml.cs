using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using Demo.Library.ViewModel;

namespace Demo.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            string s = string.Empty;

            if (e.Args.Length==2)
            {
                MainViewModel.InitializeWithParams(e.Args[0], e.Args[1]);
            }
            //MainViewModel.InitializeWithParams("MicroMsg", @"F:\工作项目\内存提取\test\qq进程内存测试分析\联系人界面\com_tencent_mobileqq.hprof");
            
        }   
    }
}
