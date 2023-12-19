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
    /// Interaction logic for wdSuaTaiKhoanClone.xaml
    /// </summary>
    public partial class wdSuaTaiKhoanClone : Window
    {
        string tkSua;
        public wdSuaTaiKhoanClone(string tk,string mk)
        {
            InitializeComponent();
            txtUsername.Text = tk;
            txtPassword.Text = mk;
            tkSua = tk + "|" + mk;
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            string tkNew = "";
            tkNew = txtUsername.Text + "|" + txtPassword.Text;

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
            MessageBox.Show("Sửa thông tin nick clone thành công");
            Close();
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
