using Microsoft.Xaml.Behaviors.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_Instagram
{
    public class QLUser
    {
        public static ObservableCollection<User> getDaTa()
        {
            ObservableCollection < User > listUser= new ObservableCollection<User>();

            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using(StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    int id = 1;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tkmk = line.Split('|');
                        User user =new User(id,tkmk[0], tkmk[1]);
                        listUser.Add(user);
                        id++;
                    }
                }
            }
            return listUser;
        }
    }
}
