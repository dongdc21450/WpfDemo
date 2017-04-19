using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Linq;
using WpfDemo.Model;
using WpfDemo.Common;
using System.Windows;
using WpfDemo.View;

namespace WpfDemo.ViewModel
{
    public class UserInfoViewModel : BaseViewModel
    {
        //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"Config\UserInfo.xml");
        public static readonly string XmlPath = @"E:\WpfApplication\WpfDemo\WpfDemo\Config\UserInfo.xml";

        private UserInfoModel userInfo;
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

        private ObservableCollection<UserInfoModel> users;
        public ObservableCollection<UserInfoModel> Users
        {
            get
            {
                return users;
            }
            set
            {
                users = value;
                OnPropertyChanged("Users");
            }
        }

        public UserInfoViewModel()
        {
            UserInfo = new UserInfoModel();
            if (Users != null)
            {
                return;
            }
            LoadUser();
        }

        public void LoadUser()
        {
            Users = new ObservableCollection<UserInfoModel>();
            XElement rootNode = XElement.Load(XmlPath);
            IEnumerable<XElement> userNodes = rootNode.Descendants("user");
            foreach (var userNode in userNodes)
            {
                Users.Add(new UserInfoModel()
                {
                    IsCheck = false,
                    UserId = int.Parse(userNode.Element("userId").Value),
                    UserName = userNode.Element("userName").Value,
                    Age = int.Parse(userNode.Element("age").Value),
                    Height = int.Parse(userNode.Element("height").Value)
                });
            }
        }

        private void AddNewUser(UserInfoModel userInfo)
        {
            //load
            XElement xele = XElement.Load(XmlPath);
            XElement result = xele.Descendants("user").Where(u => int.Parse(u.Element("userId").Value) == userInfo.UserId).FirstOrDefault();
            if (result != null)
            {
                MessageBox.Show("该用户Id已经存在，请重新输入！");
                return;
            }
            //create a new node
            XElement newUser = new XElement(
                "user",
                new XElement("userId", userInfo.UserId),
                new XElement("userName", userInfo.UserName),
                new XElement("age", userInfo.Age),
                new XElement("height", userInfo.Height)
            );
            //add to xele
            xele.Add(newUser);
            //save
            xele.Save(XmlPath);
        }

        private void DeleteUsers()
        {
            //load
            int count = this.Users.Where(u => u.IsCheck == true).Count();
            if (count <= 0)
            {
                MessageBox.Show("请至少勾选一个要删除的用户！");
                return;
            }
            if (!MessageBox.Show("确定要删除吗？", "提示", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
            {
                return;
            }
            foreach (var user in users)
            {
                if (user.IsCheck)
                {
                    DeleteUser(user.UserId);
                }
            }
        }

        private void DeleteUser(int userId)
        {
            XElement xele = XElement.Load(XmlPath);
            XElement result = xele.Descendants("user").Where(u => u.Element("userId").Value == userId.ToString()).FirstOrDefault();
            if (result != null)
            {
                result.Remove();
            }
            xele.Save(XmlPath);
        }

        private void OpenUpdateWnd(UserInfoModel userInfo)
        {
            UserInfoModel user = this.Users.Where(u => u.UserId == userInfo.UserId).FirstOrDefault();
            if (user != null)
            {
                UserEditer userWnd = new UserEditer(user);
                //userWnd.userEdierVM.LoadUser += RecieveMsg;
                userWnd.userEdierVM.DoSomething += (s,e) => {
                    if ((e as DoSomethingArgs).LoadOrClose.Equals("load"))
                    {
                        this.LoadUser();
                    }
                };
                userWnd.ShowDialog();
            }
        }

        public ICommand AddNewCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    AddNewUser(p as UserInfoModel);
                    LoadUser();
                });
            }
        }

        public ICommand BatchDeleteCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    DeleteUsers();
                    LoadUser();
                });
            }
        }

        public ICommand OpenUpdateWndCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    OpenUpdateWnd(p as UserInfoModel);
                });
            }
        }
        public ICommand CheckAllCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    bool result = false;
                    bool.TryParse(p.ToString(), out result);
                    foreach (var user in users)
                    {
                        user.IsCheck = result ? true : false;
                    }
                });
            }
        }

        public ICommand DeleteUserCommand
        {
            get
            {
                return new RelayCommand(p =>
                {
                    int userId = int.Parse(p.ToString());
                    if (!MessageBox.Show("确定要删除吗？", "提示", MessageBoxButton.YesNo).Equals(MessageBoxResult.Yes))
                    {
                        return;
                    }
                    DeleteUser(userId);
                    LoadUser();
                });
            }
        }
    }
}
