namespace WpfDemo.Model
{
    using System.ComponentModel;
    using ViewModel;

    public class UserInfoModel : BaseViewModel
    {
        public UserInfoModel(UserInfoModel userInfo = null)
        {
            if (userInfo == null)
            {
                return;
            }
            this.UserId = userInfo.UserId;
            this.UserName = userInfo.UserName;
            this.Age = userInfo.Age;
            this.Height = userInfo.Height;
        }

        /// <summary>
        /// flag of is check
        /// </summary>
        public bool IsCheck
        {
            get { return GetValue(() => IsCheck); }
            set { SetValue(() => IsCheck, value); }
        }

        /// <summary>
        /// userId
        /// </summary>
        public int UserId
        {
            get; set;
        }

        /// <summary>
        /// UserName
        /// </summary>
        public string UserName
        {
            get
            {
                return GetValue(() => UserName);
            }
            set
            {
                SetValue(() => UserName, value);
            }
        }

        /// <summary>
        /// The Age of User
        /// </summary>
        public int Age
        {
            get
            {
                return GetValue(() => Age);
            }
            set
            {
                SetValue(() => Age, value);
            }
        }

        /// <summary>
        /// The height of user
        /// </summary>
        public double Height
        {
            get
            {
                return GetValue(() => Height);
            }
            set
            {
                SetValue(() => Height, value);
            }
        }
    }
}
