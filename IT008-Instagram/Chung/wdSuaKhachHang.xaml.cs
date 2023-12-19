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
    /// Interaction logic for wdSuaKhachHang.xaml
    /// </summary>
    public partial class wdSuaKhachHang : Window
    {
        string linkSua;
        string fileKH;
        public wdSuaKhachHang(string link, string fileKH)
        {
            InitializeComponent();
            linkSua = link;
            this.fileKH = fileKH;
            txtLink.Text = linkSua;
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            string linkNew = "";
            linkNew = txtLink.Text;

            List<string> list = new List<string>();
            using (FileStream fStream = new FileStream(fileKH, FileMode.OpenOrCreate, FileAccess.Read))
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
                if (list[i] == linkSua)
                {
                    list[i] = linkNew;
                    break;
                }
            }

            try
            {
                File.Delete(fileKH);
            }
            catch { }

            using (FileStream fStream = new FileStream(fileKH, FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    foreach (string s in list)
                    {
                        sw.WriteLine(s);
                    }
                }
            }

            MessageBox.Show("Sửa thông tin khách hàng thành công!");
            Close();
        }
    }
}
