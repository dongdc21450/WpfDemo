using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WpfDemo.Common;
using WpfDemo.Model;
using WpfDemo.View;

namespace WpfDemo.ViewModel
{
    public class UserEditerViewModel : BaseViewModel
    {
        private UserInfoModel userInfo;
        public Action close;
        public event EventHandler DoSomething;
      
        public UserInfoModel UserInfo
        {
            get
            {
                return userInfo;
            }
            set
            {
                userInfo = value;
                OnPropertyChanged("UserInfo");
            }
        }

        public UserEditerViewModel(UserInfoModel userInfo=null,Action close=null)
        {
            if (userInfo == null)
            {
                return;
            }
            this.UserInfo = userInfo;
            if (close != null)
            {
                this.close = close;
            }
        }

        private void UpateUserInfo(UserInfoModel userInfo)
        {
            //load
            XElement xele = XElement.Load(UserInfoViewModel.XmlPath);

            XElement target = xele.Descendants("user").Where(p => p.Element("userId").Value.Equals(userInfo.UserId.ToString())).FirstOrDefault();
            if (target != null)
            {
                target.SetElementValue("userId", userInfo.UserId);
                target.SetElementValue("userName", userInfo.UserName);
                target.SetElementValue("age", userInfo.Age);
                target.SetElementValue("height", userInfo.Height);
            }
            xele.Save(UserInfoViewModel.XmlPath);
        }


        public ICommand UpdateUserCommand
        {
            get
            {
                return new RelayCommand(p => {
                    UpateUserInfo(p as UserInfoModel);
                    if (DoSomething != null)
                    {
                        DoSomething(this, new DoSomethingArgs("load"));
                        DoSomething(this, new DoSomethingArgs("close"));
                    }
                });
            }
        }
    }
    /// <summary>
    /// 重新加载或者关闭窗口参数
    /// </summary>
    public class DoSomethingArgs : EventArgs
    {
        public DoSomethingArgs(string args=null)
        {
            if (!string.IsNullOrEmpty(args))
            {
                this.LoadOrClose = args;
            }
        }
        /// <summary>
        /// load表示加载，close表示关闭
        /// </summary>
        public string LoadOrClose { get; set; }
    }
}
