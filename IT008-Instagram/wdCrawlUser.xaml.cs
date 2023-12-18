using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for wdCrawlUser.xaml
    /// </summary>
    public partial class wdCrawlUser : Window, INotifyPropertyChanged
    {
        private string? username;
        public string? Username { get => username; set { username = value; OnPropertyChanged(nameof(Username)); } }
        private string? savedPath;
        public string? SavedPath { get => savedPath; set { savedPath = value; OnPropertyChanged(nameof(SavedPath)); } }

        

        public wdCrawlUser()
        {
            InitializeComponent();
            DataContext = this;
            SavedPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnChoosePath_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyComputer);
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                SavedPath = dialog.FileName;
            }
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
