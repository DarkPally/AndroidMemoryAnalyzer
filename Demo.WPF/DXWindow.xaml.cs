using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

using Demo.Library.ViewModel;
namespace Demo.WPF
{
    /// <summary>
    /// Interaction logic for DXWindow1.xaml
    /// </summary>
    public partial class DXWindow1 : DXWindow
    {
        public DXWindow1()
        {
            InitializeComponent();

            this.DataContext = MainViewModel.GetInstance();
            var type=MainViewModel.GetInstance().startType;
            switch(type)
            {
                case "com.tencent.mobileqq":  
                    tabControl.SelectTabItem(0);
                    break;
                case "com.tencent.mm":
                    tabControl.SelectTabItem(2);
                    break;
                case "com.tencent.mmtools":
                    tabControl.SelectTabItem(4);
                    break; 
                case "com.netease.mobimail":
                    tabControl.SelectTabItem(5);
                    break;
                case "com.corp21cn.mail189":
                    tabControl.SelectTabItem(6);
                    break;
                case "com.cn21.ecloud":
                    tabControl.SelectTabItem(7);
                    break;
                case "com.qihoo.browser":
                    tabControl.SelectTabItem(8);
                    break;

                case "sogou.mobile.explorer":
                    tabControl.SelectTabItem(9);
                    break;

                case "com.sdu.didi.psnger":

                    tabControl.SelectTabItem(10);
                    break;
                case "com.dolphin.browser.xf":

                    tabControl.SelectTabItem(11);
                    break;
                case "cn.com.fetion":

                    tabControl.SelectTabItem(12);
                    break;
                case "com.alibaba.android.babylon":
                    tabControl.SelectTabItem(13);
                    break;

                case "jp.naver.line.android":
                    tabControl.SelectTabItem(14);
                    break;
                case "com.immomo.momo":
                    tabControl.SelectTabItem(15);
                    break;
                case "com.oupeng.mini.android":
                    tabControl.SelectTabItem(16);
                    break;
                case "com.microsoft.office.outlook":
                    tabControl.SelectTabItem(17);

                    break;

                case "com.tencent.androidqqmail":
                    tabControl.SelectTabItem(18);
                    break;
                case "com.alibaba.mobileim":

                    tabControl.SelectTabItem(19);
                    break; 
                case "com.whatsapp":
                    tabControl.SelectTabItem(20);
                    break;
                case "im.yixin":
                    tabControl.SelectTabItem(21);
                    break;
                case "com.duowan.mobile":
                    tabControl.SelectTabItem(22);
                    break;
                case "com.baidu.hi":
                    tabControl.SelectTabItem(23);
                    break;
                case "com.baidu.netdisk":
                    tabControl.SelectTabItem(24);
                    break;

                case "com.tencent.mtt":
                    tabControl.SelectTabItem(25);
                    break;

                case "com.xiaomi.channel":
                    tabControl.SelectTabItem(26);
                    break;

                case "com.renren.xiaonei.android":
                    tabControl.SelectTabItem(27);
                    break; 
                default:
                    break;
            }
        }
    }
}
