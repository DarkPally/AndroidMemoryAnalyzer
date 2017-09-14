using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Demo.WPF.Pages.LaiWang
{
    /// <summary>
    /// GroupsAndFriends.xaml 的交互逻辑
    /// </summary>
    public partial class Messages : UserControl
    {
        public Messages()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new System.Windows.Forms.OpenFileDialog();
            openFileDialog.Title = "选择安卓heap文件";
            openFileDialog.Filter = "heap文件|*.hprof";
            openFileDialog.FileName = tb_fileAddress.Text;
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = true;

            System.Windows.Forms.DialogResult result = openFileDialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.Cancel)
            {
                return;
            }
            tb_fileAddress.Text = openFileDialog.FileName;
        }
    }
}
