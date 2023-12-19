using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

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

        // Start crawling
        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Username))
            {
                MessageBox.Show("Username must be filled!");
                return;
            }
            ChromeDriver driver = new ChromeDriver();

            // LogAcc
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    if ((line = sr.ReadLine()) != null)
                    {
                        string[] tkmk = line.Split('|');

                        
                        //vào web instagarm, và đăng nhập theo tài khoản mật khẩu
                        LogAcc.Log(tkmk[0], tkmk[1], driver);

                        Thread.Sleep(8000);

                                
                        
                    }
                }
            }


            driver.Url = "https://instagram.com/" + Username;
            driver.Navigate(); 
            Thread.Sleep(3000);

            

            PTBoTroAutoTim.firstPost(driver, 10);
            Thread.Sleep(2000);
            string urlOld = "";
            string urlNew = "";
            int postCount = 0;

            string userPath = System.IO.Path.Combine(SavedPath, Username);
            if (!Directory.Exists(userPath))
            {
                Directory.CreateDirectory(userPath);
            }

            using (FileStream fStream = new FileStream(System.IO.Path.Combine(userPath, $"{Username}_data.tsv"), FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    sw.WriteLine("Post Number\tNumber of likes\tNumber of comments\tPost time");
                }
            }

            do
            {
                Thread.Sleep(1000);
                
                ++postCount;
                // Get post title
                // _a9zr: bao gom nguyen cai comment
                // _a9zc: username _a9zs: comments
                // h2 _a9zc: chu post username, h3 _a9zc la username cua ng comment

                // tao thu muc luu post hien tai

                string postPath = System.IO.Path.Combine(userPath, $"Post{postCount}");
                if (!Directory.Exists(postPath))
                {
                    Directory.CreateDirectory(postPath);
                }



                // lay likes cua post hien tai
                int numberOfLikes = 0;
                try
                {
                    
                    var likeSection = driver.FindElement(By.ClassName("_ae5m"));
                    var likeSpan = likeSection.FindElement(By.CssSelector("span[class='x1lliihq x1plvlek xryxfnj x1n2onr6 x193iq5w xeuugli x1fj9vlw x13faqbe x1vvkbs x1s928wv xhkezso x1gmr53x x1cpjm7i x1fgarty x1943h6x x1i0vuye xvs91rp xo1l8bm x5n08af x10wh9bi x1wdrske x8viiok x18hxmgj']"));
                    var countLikeSpan = likeSpan.FindElements(By.XPath("*"));
                    // html-span xdj266r x11i5rnm xat24cr x1mh8g0r xexx8yu x4uap5 x18d9i69 xkhd6sd x1hl2dhg x16tdsg8 x1vvkbs :number of likes
                    CultureInfo vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
                    var likes = likeSpan.FindElement(By.CssSelector("span[class='html-span xdj266r x11i5rnm xat24cr x1mh8g0r xexx8yu x4uap5 x18d9i69 xkhd6sd x1hl2dhg x16tdsg8 x1vvkbs']"));
                    numberOfLikes = Convert.ToInt32(likes.Text, vietnameseCulture);
                    if (countLikeSpan.Count > 1)
                    {
                        ++numberOfLikes;
                    }
                }
                catch
                {

                }
                

                IList<IWebElement> elements = driver.FindElements(By.ClassName("_a9zr"));

                //lay comments

                int numberOfComments = elements.Count;
                if (numberOfComments <= 1) numberOfComments = 0;
                else numberOfComments--;

                // lay thoi gian post

                var timeElementDiv = driver.FindElement(By.CssSelector("div[class='_ae5u _ae5v _ae5w']"));
                var timeElementTime = timeElementDiv.FindElement(By.TagName("time"));
                string timeAttribute = timeElementTime.GetAttribute("datetime");

                // ghi vao so luong likes va comments

                using (FileStream fStream = new FileStream(System.IO.Path.Combine(userPath, $"{Username}_data.tsv"), FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine($"{postCount}\t{numberOfLikes}\t{numberOfComments}\t{timeAttribute}");
                    }
                }

                // lay username va comment

                bool isOwner = true;

                using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "comments.tsv"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine("Username\tComments");
                    }
                }

                foreach (IWebElement element in elements)
                {
                    var usernameP = element.FindElement(By.ClassName("_a9zc"));
                    var commentP = element.FindElement(By.ClassName("_a9zs"));
                    var username = usernameP.FindElement(By.TagName("a"));

                    using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "comments.tsv"), FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fStream))
                        {

                            sw.WriteLine($"{username.Text}\t{Regex.Replace(commentP.Text, @"\t|\n|\r", " ")}");
                        }
                    }
                }

                using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "images.tsv"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine("Image Number\tSource\tDescription");
                    }
                }
                int imgCount = 0;
                using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "images.tsv"), FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        try
                        {

                            // _aamj _acvz _acnc _acng
                            int numberOfImage = 1;
                            try
                            {
                                var numberOfImageElement1 = driver.FindElement(By.CssSelector("div[class='_aamj _acvz _acnc _acng']"));
                                var numberOfImageElement2 = numberOfImageElement1.FindElements(By.XPath("*"));
                                numberOfImage = numberOfImageElement2.Count;
                            }
                            catch 
                            { 
                            }
                            
                            if (numberOfImage == 1)
                            {
                                var imageElementD = driver.FindElement(By.CssSelector("div[class='_aagu _aato']"));
                                var imageElement = imageElementD.FindElement(By.TagName("img"));
                                string imgDescription = imageElement.GetAttribute("alt");
                                string imgSource = imageElement.GetAttribute("src");
                                //download and write img
                                using (var client = new HttpClient())
                                {
                                    using (var s = client.GetStreamAsync(imgSource))
                                    {
                                        using (var fs = new FileStream(System.IO.Path.Combine(postPath, $"img{numberOfImage}.jpg"), FileMode.OpenOrCreate))
                                        {
                                            s.Result.CopyTo(fs);
                                        }
                                    }
                                }
                                sw.WriteLine($"{numberOfImage}\t{imgSource}\t{imgDescription}");
                            }
                             
                            if (numberOfImage > 1)
                            {
                                    for (int i = 0; i < numberOfImage; i++)
                                    {
                                        int nchild = 3;
                                        ++imgCount;
                                        if (i == 0) nchild = 2;
                                        var zoned = driver.FindElement(By.ClassName("_aamn"));

                                        IWebElement imageElementli;
                                        if (i == numberOfImage - 1)
                                        {
                                            imageElementli = zoned.FindElement(By.CssSelector($"ul[class='_acay'] > li:last-child"));
                                        }
                                        else imageElementli = zoned.FindElement(By.CssSelector($"ul[class='_acay'] > li:nth-child({nchild})"));
                                        var imageElement = imageElementli.FindElement(By.TagName("img"));
                                        string imgDescription = imageElement.GetAttribute("alt");
                                        string imgSource = imageElement.GetAttribute("src");
                                        //download and write img
                                        using (var client = new HttpClient())
                                        {
                                            using (var s = client.GetStreamAsync(imgSource))
                                            {
                                                using (var fs = new FileStream(System.IO.Path.Combine(postPath, $"img{imgCount}.jpg"), FileMode.OpenOrCreate))
                                                {
                                                    s.Result.CopyTo(fs);
                                                }
                                            }
                                        }
                                        sw.WriteLine($"{imgCount}\t{imgSource}\t{imgDescription}");

                                        //navigate to next img
                                        var btnNext = driver.FindElement(By.CssSelector("button[aria-label='Next']"));
                                        btnNext.Click();
                                        Thread.Sleep(1000);
                                    }
                                }
                            
                           
                            
                        }
                        catch
                        {

                        }
                        // lay image va image description  _aap0 div
                        //string urlImgOld = "";
                        //string urlImgNew = "";


                        //urlImgOld = driver.Url;
                        //do
                        //{
                        //    urlImgOld = driver.Url;
                        //    ++imgCount;
                        //    try
                        //    {
                        //        IList<IWebElement> imgElementDiv = driver.FindElements(By.CssSelector("div[class='_aagu _aato']"));
                        //        IList<IWebElement> imgElementImage = imgElementDiv.FindElements(By.TagName("img"));
                        //        string imgDescription = imgElementImage.GetAttribute("alt");
                        //        string imgSource = imgElementImage.GetAttribute("src");
                        //        using (var client = new HttpClient())
                        //        {
                        //            using (var s = client.GetStreamAsync(imgSource))
                        //            {
                        //                using (var fs = new FileStream(System.IO.Path.Combine(postPath, $"img{imgCount}.jpg"), FileMode.OpenOrCreate))
                        //                {
                        //                    s.Result.CopyTo(fs);
                        //                }
                        //            }
                        //        }
                        //        sw.WriteLine($"{imgCount}\t{imgSource}\t{imgDescription}");
                        //    }
                        //    catch
                        //    {

                        //    }

                        //    try
                        //    {
                        //        var btnElementDivP = driver.FindElement(By.ClassName("_aamm"));
                        //        var btnElementDiv = btnElementDivP.FindElement(By.ClassName("_aao_"));
                        //        var btnNext = btnElementDiv.FindElement(By.CssSelector("button[aria-label='Next']"));
                        //        btnNext.Click();
                        //        Thread.Sleep(800);
                        //        urlImgNew = driver.Url;
                        //    }
                        //    catch
                        //    {
                        //        urlImgNew = driver.Url;
                        //    }

                        //} while (urlImgOld != urlImgNew);
                    }
                }


                while (imgCount > 1)
                {
                    imgCount--;
                    try
                    {
                        var btnElementDivP = driver.FindElement(By.ClassName("_aamm"));
                        var btnElementDiv = btnElementDivP.FindElement(By.ClassName("_aao_"));
                        var btnBack = btnElementDiv.FindElement(By.CssSelector("button[aria-label='Go Back']"));
                        btnBack.Click();
                        Thread.Sleep(300);
                    }
                    catch
                    {

                    }
                }
                urlOld = driver.Url;

                //sang bài post tiếp theo

                try
                {
                    var btnPostKeTiep = driver.FindElement(By.CssSelector("._aaqg > button:nth-child(1)"));
                    btnPostKeTiep.Click();
                    Thread.Sleep(2000);
                    urlNew = driver.Url;
                }
                catch
                {
                    urlNew = driver.Url;
                }
            } while (urlOld != urlNew);

            driver.Quit();
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

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
