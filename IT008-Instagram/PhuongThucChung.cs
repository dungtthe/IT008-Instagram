using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace IT008_Instagram
{
    public class PhuongThucChung
    {
        public static void DeleteKH(object sender, RoutedEventArgs e,string fileKH)
        {
            var button = sender as Button;
            string tkXoa = "";
            if (button != null)
            {
                var item = button.Tag as KhachHang;
                tkXoa = item.Link;
            }

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
            list.Remove(tkXoa);

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

        }
    }
}
