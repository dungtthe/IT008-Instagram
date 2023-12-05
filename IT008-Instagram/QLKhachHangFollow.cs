using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_Instagram
{
    public class QLKhachHangFollow
    {
        public static List<KhachHang> getDaTa()
        {
            //new User("TAI KHOAN 2","7891010")
           List<KhachHang> listfollow = new List<KhachHang>();

            using (FileStream fStream = new FileStream("listFollow.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string link1 = line;
                        KhachHang taiKhoanFollow= new KhachHang(link1);
                        listfollow.Add(taiKhoanFollow);
                    }
                }
            }
            return listfollow;
        }
    }
}
