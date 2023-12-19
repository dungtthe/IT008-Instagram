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
    /// Interaction logic for wdCmt.xaml
    /// </summary>
    public partial class wdCmt : Window
    {
        List<KhachHang> listLink;
        ChromeDriver driver;
        List<string> listCmtRandom;

        RadioButton radioButton;
        public wdCmt( RadioButton radioButton)
        {
            InitializeComponent();
            loadData();
            loadCmtRandom();
            this.radioButton = radioButton;
        }
        void loadData()
        {
            lvListLink.ItemsSource = null;
            listLink = QLKhachHang.getDataKH("listKHCmt.txt");
            lvListLink.ItemsSource = listLink;
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

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

                                //đăng nhập thành công, tiến hành cmt theo url(khách hàng) truyền vào hàm
                                PTBoTroAutoCmt.autoCmt(driver, link.Link, timeMax);
                            }
                            Thread.Sleep(1000);
                        }
                    }

                }
            }

            MessageBox.Show("Done!");

        }

        private void btnQuaylai_Click(object sender, RoutedEventArgs e)
        {
            Close();
            radioButton.IsChecked = false;
        }


        //xóa 1 kh
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PhuongThucChung.DeleteKH(sender, e, "listKHCmt.txt");
            loadData();
        }

  

        //load lại danh sách cmt để random
        private void loadCmtRandom()
        {
            cboListCmt.ItemsSource = null;
            listCmtRandom = PTBoTroAutoCmt.getDataCmtRandom();
            cboListCmt.ItemsSource = listCmtRandom;
        }



        //xóa cmt random đang chọn
        private void btnXoaCmtRandom_Click(object sender, RoutedEventArgs e)
        {

            if(cboListCmt.SelectedItem != null)
            {
                string s=cboListCmt.SelectedItem.ToString();
               


                List<string> list = new List<string>();
                using (FileStream fStream = new FileStream("listCmtRandom.txt", FileMode.OpenOrCreate, FileAccess.Read))
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

                try
                {
                    File.Delete("listCmtRandom.txt");
                }
                catch { }

                list.Remove(s);


                using (FileStream fStream = new FileStream("listCmtRandom.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        foreach (string item in list)
                        {
                            sw.WriteLine(item);
                        }
                    }
                }
                loadCmtRandom();
                MessageBox.Show("Xóa thành công");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn 1 cmt!");
            }
        }


        //thêm 1 cmt để random
        private void btnThemCmtRandom_Click(object sender, RoutedEventArgs e)
        {
            wdThemMotComment wdThemMotComment = new wdThemMotComment();
            wdThemMotComment.ShowDialog();
            loadCmtRandom();
        }



        //edit link của kh
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button != null)
            {
                var item = button.Tag as KhachHang;

                if (item != null)
                {
                    wdSuaKhachHang wdSuaKhachHang = new wdSuaKhachHang(item.Link, "listKHCmt.txt");
                    wdSuaKhachHang.ShowDialog();

                    loadData();
                }
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            wdThemKhachHang wdThemKhachHang = new wdThemKhachHang("listKHCmt.txt");
            wdThemKhachHang.ShowDialog();
            loadData();
        }
    }
}
