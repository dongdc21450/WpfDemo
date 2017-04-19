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
using System.Windows.Shapes;
using WpfDemo.Model;
using WpfDemo.ViewModel;
using WpfDemo.Common;
using System.Xml.Linq;

namespace WpfDemo.View
{
    /// <summary>
    /// UserEditer.xaml 的交互逻辑
    /// </summary>
    public partial class UserEditer : Window
    {
        public UserEditerViewModel userEdierVM;
        public UserEditer(UserInfoModel userInfo)
        {
            InitializeComponent();
            userEdierVM = new UserEditerViewModel(userInfo,this.Close);
            userEdierVM.DoSomething += (s,e)=> {
                if ((e as DoSomethingArgs).LoadOrClose.Equals("close"))
                {
                    Close();
                }
            };
            DataContext = userEdierVM;
        }
    }
}
