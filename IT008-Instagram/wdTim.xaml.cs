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
            //loadData();
            radioButton = rdb;
        }

        //void loadData()
        //{
        //    lvListLink.ItemsSource = null;
        //    listLink = QLKhachHang.getData();
        //    lvListLink.ItemsSource = listLink;
        //}

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }


       //// private void btnAdd_Click(object sender, RoutedEventArgs e)
       // {
       //     check = 1;
       //     ShowPopup();
       // }

        private void btnQuaylai_Click(object sender, RoutedEventArgs e)
        {
            Close();
            radioButton.IsChecked=false;
        }

        //private void btnPUDongY_Them_Click(object sender, RoutedEventArgs e)
        //{
        //    using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.OpenOrCreate, FileAccess.Read))
        //    {
        //        using (StreamReader sr = new StreamReader(fStream))
        //        {
        //            string line;
        //            while ((line = sr.ReadLine()) != null)
        //            {
                      
        //                if (line == txtPULink_Them.Text)
        //                {
        //                    puAdd.IsOpen = false;
        //                    MessageBox.Show("Thêm không thanh công, do khách hàng đã có trong dữ liệu!");
        //                    return;
        //                }
        //            }
        //        }
        //    }

        //    using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.Append, FileAccess.Write))
        //    {
        //        using (StreamWriter sw = new StreamWriter(fStream))
        //        {
        //            string s = txtPULink_Them.Text;
        //            sw.WriteLine(s);
        //        }
        //    }

        //    loadData();
        //    puAdd.IsOpen = false;
        //    MessageBox.Show("Thêm thành công!");
        //}

        //private void btnPUThoat_Them_Click(object sender, RoutedEventArgs e)
        //{
        //    puAdd.IsOpen = false;
        //}



        int check;//check=1 là show cho thằng add, check =2 là show cho thằng edit
        //private void ShowPopup()
        //{
        //    //xóa dữ liệu trước đó mà popup dữ
        //    txtPULink_Them.Text = "";

        //    txtPULink_Edit.Text = "";
        //    // Vô hiệu hóa cửa sổ chính
        //    this.IsEnabled = false;

        //    // Tạo lớp phủ
        //    Grid overlay = new Grid() { Background = new SolidColorBrush(Colors.Black), Opacity = 0.5 };
        //    grMain.Children.Add(overlay); // Giả sử MainGrid là grid chính của cửa sổ

        //    if (check == 1)
        //    {
        //        // Hiển thị Popup
        //        puAdd.IsOpen = true;

        //        // Đặt sự kiện khi Popup đóng
        //        puAdd.Closed += (s, e) =>
        //        {
        //            // Kích hoạt lại cửa sổ chính và xóa lớp phủ
        //            this.IsEnabled = true;
        //            grMain.Children.Remove(overlay);
        //        };
        //    }
        //    else
        //    {
        //        puEdit.IsOpen = true;
        //        puEdit.Closed += (s, e) =>
        //        {
        //            this.IsEnabled = true;
        //            grMain.Children.Remove(overlay);
        //        };
        //    }

        //}


        string linkSua = "";
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            check = 2;

            var button = sender as Button;
            if (button != null)
            {
                var item = button.Tag as KhachHang;

                if (item != null)
                {
                    linkSua = item.Link;
                    //ShowPopup();
                }
            }
        }
        //private void btnPUDongY_Edit_Click(object sender, RoutedEventArgs e)
        //{
        //    string linkNew=txtPULink_Edit.Text;

        //    List<string> list = new List<string>();
        //    using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.OpenOrCreate, FileAccess.Read))
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
        //        if (list[i] == linkSua)
        //        {
        //            list[i] = linkNew;
        //            break;
        //        }
        //    }

        //    try
        //    {
        //        File.Delete("listKHTim.txt");
        //    }
        //    catch { }

        //    using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.Append, FileAccess.Write))
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
        //    puEdit.IsOpen = false;
        //    MessageBox.Show("Sửa link khách hàng thành công!");
        //}

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string linkXoa = "";
            if (button != null)
            {
                var item = button.Tag as KhachHang;
                linkXoa = item.Link;
            }

            List<string> list = new List<string>();
            using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(line);
                    }

                }
            }
            list.Remove(linkXoa);

            try
            {
                File.Delete("listKHTim.txt");
            }
            catch { }

            using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    foreach (string s in list)
                    {
                        sw.WriteLine(s);
                    }
                }
            }

            MessageBox.Show("Xóa thành công!");
            //loadData();
        }

        //private void btnPUThoat_Edit_Click(object sender, RoutedEventArgs e)
        //{
        //    puEdit.IsOpen = false;
        //}



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

        }
    }
}
