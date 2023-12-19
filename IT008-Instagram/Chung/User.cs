using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace IT008_Instagram
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string taiKhoan;
        private string matKhau;

        public int Id
        {
            get => id;
            set
            {
                if(id != value)
                {
                    id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }
        public string TaiKhoan
        {
            get => taiKhoan;

            set
            {
                if (taiKhoan != value)
                {
                    taiKhoan = value;
                    OnPropertyChanged(nameof(TaiKhoan));
                }
            }
        }

        public string MatKhau
        {
            get => matKhau;
            set
            {
                if (matKhau != value)
                {
                    matKhau = value;
                    OnPropertyChanged(nameof(MatKhau));
                }
            }
        }

        public User(int id,string tk,string mk)
        {
            this.id = id;
            taiKhoan= tk;
            matKhau= mk;
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
