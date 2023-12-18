using MaterialDesignThemes.Wpf;
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

namespace IT008_Instagram
{
    /// <summary>
    /// Interaction logic for MainPage.xaml
    /// </summary>
    public partial class MainPage : Window
    {
        ObservableCollection<User> DSUser;
        public MainPage()
        {
            InitializeComponent();
            loadData();
        }

        void loadData()
        {
            lvDSUser.ItemsSource = null;
            DSUser = QLUser.getDaTa();
            lvDSUser.ItemsSource = DSUser;
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }



        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            check = 1;
            ShowPopup();
        }

        int check;//check=1 là show cho thằng add, check =2 là show cho thằng edit
        private void ShowPopup()
        {
            //xóa dữ liệu trước đó mà popup dữ
            txtPUTenTK.Text = "";
            txtPUMK.Text = "";
            txtPUTenTK_Edit.Text = "";
            txtPUMK_Edit.Text = "";

            // Vô hiệu hóa cửa sổ chính
            this.IsEnabled = false;

            // Tạo lớp phủ
            Grid overlay = new Grid() { Background = new SolidColorBrush(Colors.Black), Opacity = 0.5 };
            grMain.Children.Add(overlay); // Giả sử MainGrid là grid chính của cửa sổ

            if (check == 1)
            {
                // Hiển thị Popup
                puAddClone.IsOpen = true;

                // Đặt sự kiện khi Popup đóng
                puAddClone.Closed += (s, e) =>
                {
                    // Kích hoạt lại cửa sổ chính và xóa lớp phủ
                    this.IsEnabled = true;
                    grMain.Children.Remove(overlay);
                };
            }
            else
            {
                puEditClone.IsOpen = true;
                puEditClone.Closed += (s, e) =>
                {
                    this.IsEnabled = true;
                    grMain.Children.Remove(overlay);
                };
            }

        }



        private void btnPUThoat_Click(object sender, RoutedEventArgs e)
        {
            puAddClone.IsOpen = false;
        }

        private void btnPUDongY_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tkmk = line.Split('|');
                        if (tkmk[0] == txtPUTenTK.Text)
                        {
                            puAddClone.IsOpen = false;
                            MessageBox.Show("Thêm không thanh công, do tài khoản đã có trong dữ liệu!");
                            return;
                        }
                    }
                }
            }

            using (FileStream fStream = new FileStream("listUser.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    string s = txtPUTenTK.Text + "|" + txtPUMK.Text;
                    sw.WriteLine(s);
                }
            }

            loadData();
            puAddClone.IsOpen = false;
            MessageBox.Show("Thêm thành công!");
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            string tkXoa = "";
            if (button != null)
            {
                var item = button.Tag as User;
                tkXoa = item.TaiKhoan + "|" + item.MatKhau;
            }

            List<string> list = new List<string>();
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
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
            list.Remove(tkXoa);

            try
            {
                File.Delete("listUser.txt");
            }
            catch { }

            using (FileStream fStream = new FileStream("listUser.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    foreach (string s in list)
                    {
                        sw.WriteLine(s);
                    }
                }
            }

            loadData();

        }


        string tkSua = "";
        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            check = 2;

            var button = sender as Button;
            if (button != null)
            {
                var item = button.Tag as User;

                if (item != null)
                {
                    tkSua = item.TaiKhoan + "|" + item.MatKhau;

                    ShowPopup();
                }
            }
        }

        private void btnPUDongY_Edit_Click(object sender, RoutedEventArgs e)
        {

            string tkNew = "";
            tkNew = txtPUTenTK_Edit.Text + "|" + txtPUMK_Edit.Text;

            List<string> list = new List<string>();
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
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

            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == tkSua)
                {
                    list[i] = tkNew;
                    break;
                }
            }

            try
            {
                File.Delete("listUser.txt");
            }
            catch { }

            using (FileStream fStream = new FileStream("listUser.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    foreach (string s in list)
                    {
                        sw.WriteLine(s);
                    }
                }
            }

            loadData();
            puEditClone.IsOpen = false;
            MessageBox.Show("Sửa thông tin nick clone thành công!");
        }
        private void btnPUThoat_Edit_Click(object sender, RoutedEventArgs e)
        {
            puEditClone.IsOpen = false;
        }

        private void rdbCrawlListPost_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Show window CrawlListPost");
        }

        private void rdbFollow_Checked(object sender, RoutedEventArgs e)
        {
            if (lvDSUser.Items.Count > 0) 
            {
                MessageBox.Show("Show window follow");
                FollowWindow followWindow = new FollowWindow(this);
                followWindow.Show();
                this.Hide();
                rdbFollow.IsChecked = false;
            }
            else
            {
                MessageBox.Show("Hãy thêm acc clone");
            }
            
        }

        private void rdbCMT_Checked(object sender, RoutedEventArgs e)
        {
            wdCmt wdCmt=new wdCmt(rdbCMT);
            wdCmt.ShowDialog();
        }

        private void rdbTym_Checked(object sender, RoutedEventArgs e)
        {
            wdTim wdTim = new wdTim(rdbTym);
            wdTim.ShowDialog();
        }

        private void rdbCrawlUser_Checked(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Show window crawl");
            wdCrawlUser crawlUserWindow = new wdCrawlUser();
            crawlUserWindow.ShowDialog();
        }
    }

   
}
