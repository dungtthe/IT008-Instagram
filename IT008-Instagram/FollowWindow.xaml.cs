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
            DStaiKhoanFollows = QLKhachHang.getDaTaKHFollow();
            lvDSlinkFollow.ItemsSource = DStaiKhoanFollows;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            parent.Show();
        }

        //private void btnAdd_Click(object sender, RoutedEventArgs e)
        //{
        //    check = 1;
        //    ShowPopup();
        //}
        //int check; //check=1 là show cho thằng add, check =2 là show cho thằng edit
        //private void ShowPopup()
        //{
        //    //xóa dữ liệu trước đó mà popup dữ
        //    txtPULink.Text = "";
        //    txtPULink_Edit.Text = " ";

        //    this.IsEnabled = false;
        //    // Tạo lớp phủ
        //    Grid overlay = new Grid() { Background = new SolidColorBrush(Colors.Black), Opacity = 0.5 };
        //    grFollow.Children.Add(overlay); // Giả sử MainGrid là grid chính của cửa sổ

        //    if (check == 1)
        //    {
        //        // Hiển thị Popup
        //        puAddClone.IsOpen = true;

        //        // Đặt sự kiện khi Popup đóng
        //        puAddClone.Closed += (s, e) =>
        //        {
        //            // Kích hoạt lại cửa sổ chính và xóa lớp phủ
        //            this.IsEnabled = true;
        //            grFollow.Children.Remove(overlay);
        //        };
        //    }
        //    else
        //    {
        //        puEditClone.IsOpen = true;
        //        puEditClone.Closed += (s, e) =>
        //        {
        //            this.IsEnabled = true;
        //            grFollow.Children.Remove(overlay);
        //        };
        //    }
        //}

        //private void btnPUThoat_Click(object sender, RoutedEventArgs e)
        //{
        //    puAddClone.IsOpen = false;
        //}
        //private void btnPUDongY_Click(object sender, RoutedEventArgs e)
        //{
        //    using (FileStream fStream = new FileStream("listFollow.txt", FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        using (StreamReader sr = new StreamReader(fStream))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                string link= line;
        //                if (link == txtPULink.Text)
        //                {
        //                    puAddClone.IsOpen = false;
        //                    MessageBox.Show("Thêm không thanh công, do tài khoản đã có trong dữ liệu!");
        //                    return;
        //                }
        //            }
        //        }
        //    }

        //    using (FileStream fStream = new FileStream("listFollow.txt", FileMode.Append, FileAccess.Write))
        //    {
        //        using (StreamWriter sw = new StreamWriter(fStream))
        //        {
        //            string s = txtPULink.Text;
        //            sw.WriteLine(s);
        //        }
        //    }

        //    loadData();
        //    puAddClone.IsOpen = false;
        //    MessageBox.Show("Thêm thành công!");
        //}
        //private void btnDelete_Click(object sender, RoutedEventArgs e)
        //{
        //    var button = sender as Button;
        //    string tkXoa = "";
        //    if (button != null)
        //    {
        //        var item = button.Tag as KhachHang;
        //        tkXoa = item.Link;
        //    }

        //    List<string> list = new List<string>();
        //    using (FileStream fStream = new FileStream("listFollow.txt", FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        using (StreamReader sr = new StreamReader(fStream))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                list.Add(line);
        //            }

        //        }
        //    }
        //    list.Remove(tkXoa);

        //    try
        //    {
        //        File.Delete("listFollow.txt");
        //    }
        //    catch { }

        //    using (FileStream fStream = new FileStream("listFollow.txt", FileMode.Append, FileAccess.Write))
        //    {
        //        using (StreamWriter sw = new StreamWriter(fStream))
        //        {
        //            foreach (string s in list)
        //            {
        //                sw.WriteLine(s);
        //            }
        //        }
        //    }

        //    loadData();

        //}
        //string tkSua = "";
        //private void btnEdit_Click(object sender, RoutedEventArgs e)
        //{
        //    check = 2;

        //    var button = sender as Button;
        //    if (button != null)
        //    {
        //        var item = button.Tag as KhachHang;

        //        if (item != null)
        //        {
        //            tkSua = item.Link;

        //            ShowPopup();
        //        }
        //    }
        //}
        //private void btnPUDongY_Edit_Click(object sender, RoutedEventArgs e)
        //{

        //    string tkNew = "";
        //    tkNew = txtPULink_Edit.Text;

        //    List<string> list = new List<string>();
        //    using (FileStream fStream = new FileStream("listFollow.txt", FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        using (StreamReader sr = new StreamReader(fStream))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
        //                list.Add(line);
        //            }

        //        }
        //    }
        //    for (int i = 0; i < list.Count; i++)
        //    {
        //        if (list[i] == tkSua)
        //        {
        //            list[i] = tkNew;
        //            break;
        //        }
        //    }

        //    try
        //    {
        //        File.Delete("listFollow.txt");
        //    }
        //    catch { }

        //    using (FileStream fStream = new FileStream("listFollow.txt", FileMode.Append, FileAccess.Write))
        //    {
        //        using (StreamWriter sw = new StreamWriter(fStream))
        //        {
        //            foreach (string s in list)
        //            {
        //                sw.WriteLine(s);
        //            }
        //        }
        //    }

        //    loadData();
        //    puEditClone.IsOpen = false;
        //    MessageBox.Show("Sửa thông tin nick clone thành công!");
        //}
        //private void btnPUThoat_Edit_Click(object sender, RoutedEventArgs e)
        //{
        //    puEditClone.IsOpen = false;
        //}

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
    }
}

       