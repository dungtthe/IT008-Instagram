using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_Instagram
{
    public class QLKhachHang
    {
        public static List<KhachHang> getDataKHTim()
        {
            List<KhachHang> list=new List<KhachHang> ();

            using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                int id = 1;
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(new KhachHang(id, line));
                        id++;
                    }
                }
            }
            return list;
        }

        public static List<KhachHang> getDataCmt()
        {
            List<KhachHang> list = new List<KhachHang>();

            using (FileStream fStream = new FileStream("listKHCmt.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    int id = 1;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(new KhachHang(id, line));
                        id++;
                    }
                }
            }
            return list;
        }


        public static List<KhachHang> getDaTaKHFollow()
        {
            List<KhachHang> listfollow = new List<KhachHang>();

            using (FileStream fStream = new FileStream("listFollow.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                int id = 1;
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string link1 = line;
                        KhachHang taiKhoanFollow = new KhachHang(id, link1);
                        listfollow.Add(taiKhoanFollow);
                        id++;
                    }
                }
            }
            return listfollow;
        }
    }
}
