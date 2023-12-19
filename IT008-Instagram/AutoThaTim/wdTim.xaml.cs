using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
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

namespace IT008_Instagram
{
    /// <summary>
    /// Interaction logic for wdTim.xaml
    /// </summary>
    public partial class wdTim : Window
    {
        List<KhachHang> listLink;
        ChromeDriver driver;
      

        RadioButton radioButton;
        public wdTim(RadioButton rdb)
        {
            InitializeComponent();
            loadData();
            radioButton = rdb;
        }

        void loadData()
        {
            lvDSlink.ItemsSource = null;
            listLink = QLKhachHang.getDataKH("listKHTim.txt");
            lvDSlink.ItemsSource = listLink;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        //bắt đầu chạy tool thả tim
        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            int timeMax = 10;//phục vụ cho việc try catch load element
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tkmk = line.Split('|');

                        foreach (var link in listLink)
                        {
                            using (driver = new ChromeDriver())
                            {
                                //vào web instagarm, và đăng nhập theo tài khoản mật khẩu
                                LogAcc.Log(tkmk[0], tkmk[1], driver);


                                //ngủ tầm 5s để load trang chủ của clone
                                Thread.Sleep(5000);

                                //đăng nhập thành công, tiến hành thả tim theo url(khách hàng) truyền vào hàm
                                PTBoTroAutoTim.autoTim(driver, link.Link, timeMax);

                            }
                            Thread.Sleep(1000);
                        }
                    }
                }
            }
            MessageBox.Show("Done!");
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            Close();
            radioButton.IsChecked = false;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            wdThemKhachHang wdThemKhachHang = new wdThemKhachHang("listKHTim.txt");
            wdThemKhachHang.ShowDialog();
            loadData();
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.Tag as KhachHang;

                if (item != null)
                {
                    wdSuaKhachHang wdSuaKhachHang = new wdSuaKhachHang(item.Link, "listKHTim.txt");
                    wdSuaKhachHang.ShowDialog();

                    loadData();
                }
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PhuongThucChung.DeleteKH(sender, e, "listKHTim.txt");
            loadData();
        }
    }
}
