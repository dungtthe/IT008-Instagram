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
    /// Interaction logic for wdThemKhachHang.xaml
    /// </summary>
    public partial class wdThemKhachHang : Window
    {
        string fileKH;
        public wdThemKhachHang(string fileKH)
        {
            InitializeComponent();
            this.fileKH = fileKH;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            using (FileStream fStream = new FileStream(fileKH, FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string link = line;
                        if (link == txtLink.Text)
                        {
                            MessageBox.Show("Thêm không thanh công, do tài khoản đã có trong dữ liệu!");
                            Close();
                            return;
                        }
                    }
                }
            }

            using (FileStream fStream = new FileStream(fileKH, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    string s = txtLink.Text;
                    sw.WriteLine(s);
                }
            }
            MessageBox.Show("Thêm thành công!");
            Close();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
