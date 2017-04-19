namespace WpfDemo
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using System.Windows;
    using View;
    using System.Linq;
    using WpfDemo.Model;
    using System.Threading;

    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();
            //this.dataGrid.ItemsSource = ((UserInfoViewModel)this.DataContext).users;
            isRunning = false;

            //Singleton.GetInstance();

            //IsList<Action> actions = new List<Action>();
            //for (int i = 1; i < 5; i++)
            //{
            //    int v = i;
            //    actions.Add(() => MessageBox.Show(v.ToString()));
            //}

            //foreach (var action in actions)
            //{
            //    action();
            //}

            //GenericClass<int> gci = new GenericClass<int>();
            //gci.PrintType(1);
            //GenericClass<string> gcs = new GenericClass<string>();
            //gcs.PrintType("1");

            //GenericClass<UserInfoModel> gcs = new GenericClass<UserInfoModel>();
            //gcs.PrintType(new UserInfoModel());

            //new Thread(Go).Start();  // .NET 1.0开始就有的
            //Task.Factory.StartNew(Go); // .NET 4.0 引入了 TPL
            //Task.Run(new Action(Go)); // .NET 4.5 新增了一个Run的方法
            //Test();
            //MessageBox.Show(string.Format("Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId));
        }

        //static async Task Test()
        //{
        //    // 方法打上async关键字，就可以用await调用同样打上async的方法
        //    // await 后面的方法将在另外一个线程中执行
        //    await GetName();
        //}

        //static async Task GetName()
        //{
        //    // Delay 方法来自于.net 4.5
        //    await Task.Delay(1000);  // 返回值前面加 async 之后，方法里面就可以用await了
        //    MessageBox.Show(string.Format("Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId));
        //    MessageBox.Show("In antoher thread.....");
        //}

        //private void Go()
        //{
        //    Thread.Sleep(10000);
        //    MessageBox.Show("我是另一个线程");
        //}

        public bool isRunning;

        private void login_Click(object sender, RoutedEventArgs e)
        {
            UserInfoRecordsView usersView = new UserInfoRecordsView();
            usersView.Show();
            this.Close();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            isRunning = !isRunning;
            button.Content = isRunning ? "停止" : "开始";
            if (isRunning)
            {
                //Thread th = new Thread(RunNumbers);
                //th.IsBackground = true;
                //th.Start();
                var task = new Task(()=> {
                    while (isRunning)
                    {
                        Random r = new Random();
                        while (isRunning)
                        {
                            this.Dispatcher.Invoke(() => {
                                num1.Content = r.Next(0, 10).ToString();
                                num2.Content = r.Next(0, 10).ToString();
                                num3.Content = r.Next(0, 10).ToString();
                            });
                        }
                    }
                    MessageBox.Show(string.Format("Current Thread Id :{0}", Thread.CurrentThread.ManagedThreadId));
                });
                task.Start();
                //await
            }
        }

        //private void RunNumbers()
        //{
        //    while (isRunning)
        //    {
        //        Random r = new Random();
        //        while (isRunning)
        //        {
        //            this.Dispatcher.Invoke(()=> {
        //                num1.Content = r.Next(0, 10).ToString();
        //                num2.Content = r.Next(0, 10).ToString();
        //                num3.Content = r.Next(0, 10).ToString();
        //            });
        //        }   
        //    }
        //}
    }
}