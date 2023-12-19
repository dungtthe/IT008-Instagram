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
    /// Interaction logic for wdThemMotComment.xaml
    /// </summary>
    public partial class wdThemMotComment : Window
    {
        public wdThemMotComment()
        {
            InitializeComponent();
        }

        private void btnDongY_Click(object sender, RoutedEventArgs e)
        {
            if (txtThemCmtRandom.Text == "")
            {
                MessageBox.Show("Cmt không được rỗng!");
            }
            else
            {
                using (FileStream fStream = new FileStream("listCmtRandom.txt", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine(txtThemCmtRandom.Text);
                    }
                }
                MessageBox.Show("Thêm thành công!");
                Close();
            }
        }

        private void btnHuy_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
