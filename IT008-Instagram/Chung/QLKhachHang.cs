using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_Instagram
{
    public class QLKhachHang
    {
        public static List<KhachHang> getDataKH(string fileKH)
        {
            List<KhachHang> list = new List<KhachHang>();

            using (FileStream fStream = new FileStream(fileKH, FileMode.OpenOrCreate, FileAccess.Read))
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
    }
}
