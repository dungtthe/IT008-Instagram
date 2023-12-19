using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace IT008_Instagram
{
    /// <summary>
    /// Interaction logic for FollowWindow.xaml
    /// </summary>
    public partial class FollowWindow : Window
    {
        Window parent;
        List<KhachHang> DStaiKhoanFollows;
        private static ChromeDriver driver;

        public FollowWindow(Window parent)
        {
            InitializeComponent();
            loadData();
            this.parent = parent;
        }

        public void loadData()
        {
            lvDSlinkFollow.ItemsSource = null;
            DStaiKhoanFollows = QLKhachHang.getDataKH("listFollow.txt");
            lvDSlinkFollow.ItemsSource = DStaiKhoanFollows;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            parent.Show();
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            wdThemKhachHang wdThemKhachHang =new wdThemKhachHang("listFollow.txt"); 
            wdThemKhachHang.ShowDialog();
            loadData();
        }
      
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PhuongThucChung.DeleteKH(sender, e, "listFollow.txt");
            loadData();
        }


        //Phần theo dõi
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            // Đọc danh sách tài khoản từ listfollow
            List<string> listFollows = new List<string>();
            using (FileStream fStream = new FileStream("listFollow.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listFollows.Add(line);
                    }
                }
            }
            //Theo dõi
            using (FileStream fStream1 = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr1 = new StreamReader(fStream1)) 
                {
                    string line;
                    while ((line = sr1.ReadLine()) != null)
                    {
                        string[] tk = line.Split('|');
                        driver = new ChromeDriver();
                        LogAcc.Log(tk[0], tk[1],driver);
                        foreach( string link in listFollows )
                        {
                            Thread.Sleep(2000);
                            FollowUser(link);
                        }
                        driver.Quit();
                    }
                }
                MessageBox.Show("Thành công");
            }

        }
        private void FollowUser(string url)
        {
            Thread.Sleep(2000);
            driver.Url = url;
            driver.Navigate();
            int count0 = 0;
            int maxtime = 10;
    
            if (count0 == 10)
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng");
                return;
            }



            int count1 = 0;
            
            while (count1 < maxtime)
            {
                try
                {
                    var btnFl = driver.FindElement(By.CssSelector("._ap3a"));
                    btnFl.Click();
                    break;
                }
                catch 
                {
                    count1++;
                    Thread.Sleep(1000);
                }           
            }
            if (count1 == 10) 
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng");
                return;
            }
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.Tag as KhachHang;

                if (item != null)
                {
                    wdSuaKhachHang wdSuaKhachHang = new wdSuaKhachHang(item.Link, "listFollow.txt");
                    wdSuaKhachHang.ShowDialog();

                    loadData();
                }
            }
        }
    }
}

       