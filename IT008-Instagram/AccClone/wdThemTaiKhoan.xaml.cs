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
    /// Interaction logic for wdThemTaiKhoan.xaml
    /// </summary>
    public partial class wdThemTaiKhoan : Window
    {
        public wdThemTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tkmk = line.Split('|');
                        if (tkmk[0] == txtUsername.Text)
                        {
                            MessageBox.Show("Thêm không thanh công, do tài khoản đã có trong dữ liệu!");
                            Close();
                            return;
                        }
                    }
                }
            }

            using (FileStream fStream = new FileStream("listUser.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    string s = txtUsername.Text + "|" + txtPassword.Text;
                    sw.WriteLine(s);
                }
            }
            MessageBox.Show("Thêm thành công!");
            Close();
        }
    }
}
