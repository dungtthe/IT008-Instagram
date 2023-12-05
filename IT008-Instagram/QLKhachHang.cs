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
        public static List<KhachHang> getData()
        {
            List<KhachHang> list=new List<KhachHang> ();

            using (FileStream fStream = new FileStream("listKHTim.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        list.Add(new KhachHang(line));
                    }
                }
            }
            return list;
        }
    }
}
