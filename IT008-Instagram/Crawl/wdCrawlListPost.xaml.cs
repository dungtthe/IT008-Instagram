using Microsoft.WindowsAPICodePack.Dialogs;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Security.Policy;
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
using static System.Net.Mime.MediaTypeNames;

namespace IT008_Instagram
{
    /// <summary>
    /// Interaction logic for wdCrawlListPost.xaml
    /// </summary>
    public partial class wdCrawlListPost : Window, INotifyPropertyChanged
    {
        private ObservableCollection<string> postList = new ObservableCollection<string>();
        public ObservableCollection<string> PostList {  get { return this.postList; } set { postList = value; OnPropertyChanged(nameof(PostList)); } }

        private string link;
        public string Link { get => link; set { link = value; OnPropertyChanged(nameof(Link)); } }

        private string? savedPath;
        public string? SavedPath { get => savedPath; set { savedPath = value; OnPropertyChanged(nameof(SavedPath)); } }

        public wdCrawlListPost()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnX_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            PostList.Clear();
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            PostList.Add(Link);
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
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
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

            int cntLink = 0;

            if (!Directory.Exists(System.IO.Path.Combine(SavedPath, "IGAutomation")))
            {
                Directory.CreateDirectory(System.IO.Path.Combine(SavedPath, "IGAutomation"));
            }

            using (FileStream fStream = new FileStream(System.IO.Path.Combine(SavedPath, "IGAutomation","data.tsv"), FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fStream))
                {
                    sw.WriteLine("Post Number\tAuthor\tContent\tNumber of likes\tNumber of comments\tPost time");
                }
            }

            foreach (string postlink in PostList)
            {
                cntLink++;
                driver.Url = postlink;
                driver.Navigate();
                Thread.Sleep(3000);

                string postPath = System.IO.Path.Combine(SavedPath, "IGAutomation", $"Post{cntLink}");
                if (!Directory.Exists(postPath))
                {
                    Directory.CreateDirectory(postPath);
                }
                int numberOfLikes = 0;
                try
                {

                    //var likeSection = driver.FindElement(By.ClassName("_ae5m"));
                    //var likeSpan = likeSection.FindElement(By.CssSelector("span[class='x1lliihq x1plvlek xryxfnj x1n2onr6 x193iq5w xeuugli x1fj9vlw x13faqbe x1vvkbs x1s928wv xhkezso x1gmr53x x1cpjm7i x1fgarty x1943h6x x1i0vuye xvs91rp xo1l8bm x5n08af x10wh9bi x1wdrske x8viiok x18hxmgj']"));
                    //var countLikeSpan = likeSpan.FindElements(By.XPath("*"));
                    // html-span xdj266r x11i5rnm xat24cr x1mh8g0r xexx8yu x4uap5 x18d9i69 xkhd6sd x1hl2dhg x16tdsg8 x1vvkbs :number of likes
                    CultureInfo vietnameseCulture = CultureInfo.GetCultureInfo("vi-VN");
                    var likes = driver.FindElement(By.CssSelector("span[class='html-span xdj266r x11i5rnm xat24cr x1mh8g0r xexx8yu x4uap5 x18d9i69 xkhd6sd x1hl2dhg x16tdsg8 x1vvkbs']"));
                    numberOfLikes = Convert.ToInt32(likes.Text, vietnameseCulture);
                    //if (countLikeSpan.Count > 1)
                    //{
                    //    ++numberOfLikes;
                    //}
                }
                catch
                {

                }

                IList<IWebElement> elements = driver.FindElements(By.ClassName("_a9zr"));
                // body > div.x1n2onr6.xzkaem6 > div.x9f619.x1n2onr6.x1ja2u2z > div > div.x1uvtmcs.x4k7w5x.x1h91t0o.x1beo9mf.xaigb6o.x12ejxvf.x3igimt.xarpa2k.xedcshv.x1lytzrv.x1t2pt76.x7ja8zs.x1n2onr6.x1qrby5j.x1jfb8zj > div > div > div > div > div.xb88tzc.xw2csxc.x1odjw0f.x5fp0pe.x1qjc9v5.xjbqb8w.x1lcm9me.x1yr5g0i.xrt01vj.x10y3i5r.xr1yuqi.xkrivgy.x4ii5y1.x1gryazu.x15h9jz8.x47corl.xh8yej3.xir0mxb.x1juhsu6 > div > article > div > div._ae65 > div > div > div._ae2s._ae3v._ae3w > div._ae5q._akdn._ae5r._ae5s > ul > div:nth-child(3) > div > div

                //var elementParent = driver.FindElement(By.CssSelector("div > div > div.x9f619.x1n2onr6.x1ja2u2z > div > div > div.x78zum5.xdt5ytf.x1t2pt76.x1n2onr6.x1ja2u2z.x10cihs4 > div.x9f619.xvbhtw8.x78zum5.x168nmei.x13lgxp2.x5pf9jr.xo71vjh.x1uhb9sk.x1plvlek.xryxfnj.x1c4vz4f.x2lah0s.x1q0g3np.xqjyukv.x1qjc9v5.x1oa3qoh.x1qughib > div.x1gryazu.xh8yej3.x10o80wk.x14k21rp.x17snn68.x6osk4m.x1porb0y > section > main > div > div.x6s0dn4.x78zum5.xdt5ytf.xdj266r.xkrivgy.xat24cr.x1gryazu.x1n2onr6.xh8yej3 > div > div.x4h1yfo > div > div.x5yr21d.xw2csxc.x1odjw0f.x1n2onr6 > div > div.x78zum5.xdt5ytf.x1iyjqo2"));
                //var elementCount = elementParent.FindElements(By.XPath("*"));
                //lay comments

                int numberOfComments = elements.Count;
                if (numberOfComments <= 1) numberOfComments = 0;
                else numberOfComments--;
                // lay thoi gian post

                var timeElementTime = driver.FindElement(By.ClassName("_aaqe"));
                string timeAttribute = timeElementTime.GetAttribute("datetime");

                // lay content
                // content
                // x193iq5w xeuugli x1fj9vlw x13faqbe x1vvkbs xt0psk2 x1i0vuye xvs91rp xo1l8bm x5n08af x10wh9bi x1wdrske x8viiok x18hxmgj                
                
                // get author
                var authorElement = driver.FindElement(By.CssSelector("span[class='_ap3a _aaco _aacw _aacx _aad7 _aade']"));
                string author = authorElement.Text;
                bool hasContent = true;
                string content = "";
                try
                {
                    var contentElement = driver.FindElement(By.CssSelector("div > div._a9zr > div._a9zs > h1"));
                    content = contentElement.Text;
                }
                catch
                {
                    hasContent = false;
                }
                

                // ghi vao so luong likes va comments

                using (FileStream fStream = new FileStream(System.IO.Path.Combine(SavedPath, "IGAutomation", $"data.tsv"), FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine($"{cntLink}\t{author}\t{Regex.Replace(content, @"\t|\n|\r", "")}\t{numberOfLikes}\t{numberOfComments}\t{timeAttribute}");
                    }
                }

                
                using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "comments.tsv"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        sw.WriteLine("Username\tComments");
                    }
                }
                bool firstElement = true;
                foreach (IWebElement element in elements)
                {
                    if (hasContent && firstElement)
                    {
                        firstElement = false;
                        continue;
                    }
                    // username
                    //_ap3a _aaco _aacw _aacx _aad7 _aade

                    //comment: (bo cai dau tien) -> span
                    //x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1uhb9sk x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1cy8zhl x1oa3qoh x1nhvcw1
                    //x9f619 xjbqb8w x78zum5 x168nmei x13lgxp2 x5pf9jr xo71vjh x1uhb9sk x1plvlek xryxfnj x1c4vz4f x2lah0s xdt5ytf xqjyukv x1cy8zhl x1oa3qoh x1nhvcw1

                    var usernameElement = element.FindElement(By.CssSelector("div._a9zr > h3 > div > div > div > a"));
                    string username = usernameElement.Text;
                    var commentElementD = element.FindElement(By.CssSelector("div._a9zr > div._a9zs > span"));
                    //var commentElementS = commentElementD.FindElement(By.TagName("span"));
                    string comment = commentElementD.Text;
                    using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "comments.tsv"), FileMode.Append, FileAccess.Write))
                    {
                        using (StreamWriter sw = new StreamWriter(fStream))
                        {

                            sw.WriteLine($"{username}\t{Regex.Replace(comment, @"\t|\n|\r", "")}");
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
                //_aagu
                using (FileStream fStream = new FileStream(System.IO.Path.Combine(postPath, "images.tsv"), FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fStream))
                    {
                        try
                        {
                            // ul[class='_acay'] > li:nth-child(2) first
                            // ul[class='_acay'] > li:nth-child(3) 



                            // get number of images
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
                                var imageElementD = driver.FindElement(By.CssSelector("div[class='_aagu']"));
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
                                    IWebElement imageElementli;
                                    if (i == numberOfImage - 1)
                                    {
                                        imageElementli = driver.FindElement(By.CssSelector($"ul[class='_acay'] > li:last-child"));
                                    }
                                    else imageElementli = driver.FindElement(By.CssSelector($"ul[class='_acay'] > li:nth-child({nchild})"));
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
                        
                    }
                }


                

            }
            driver.Quit();

        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
