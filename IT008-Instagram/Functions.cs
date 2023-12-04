using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V117.FedCm;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Net.WebRequestMethods;

namespace IT008_Instagram
{
    public class Functions
    {
        private static ChromeDriver driver;
        public static void login(string username, string password)
        {

            driver = new ChromeDriver();
            //vào web instagram
            driver.Url = "https://www.instagram.com/";
            driver.Navigate();

            int count1 = 0;
            int maxTime = 10;
            while (count1 < maxTime)
            {
                try
                {
                    var tentaikhoan = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[1]/div/label/input"));
                    tentaikhoan.SendKeys(username);
                    break;
                }
                catch
                {
                    count1++;
                    Thread.Sleep(1000);
                }

            }
            if (count1 == 10)
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng");
                return;
            }


            int count2 = 0;
            while (count2 < maxTime)
            {
                try
                {

                    var pass = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[2]/div/label/input"));
                    pass.SendKeys(password);
                    break;
                }
                catch
                {
                    count2++;
                    Thread.Sleep(1000);
                }

            }
            if (count2 == 10)
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng");
                return;
            }


            int count3 = 0;
            while (count3 < maxTime)
            {
                try
                {

                    var btnLog = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/section/main/article/div[2]/div[1]/div[2]/form/div/div[3]/button/div"));
                    btnLog.Click();
                    break;
                }
                catch
                {
                    count3++;
                    Thread.Sleep(1000);
                }

            }
            if (count3 == 10)
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng");
                return;
            }





           
        }

        public static void follow(string url)
        {
            //vào messi sau khi login thành công,và nhấn fl
            Thread.Sleep(10000);
            driver.Url = url;
            driver.Navigate();
            Thread.Sleep(10000);

            int count1 = 0;
            int maxTime = 10;
            while (count1 < maxTime)
            {
                try
                {
                    var btnFl = driver.FindElement(By.XPath("/html/body/div[2]/div/div/div[2]/div/div/div[1]/div[1]/div[2]/div[2]/section/main/div/header/section/div[1]/div[2]/div/div[1]/button"));
                    btnFl.Click();
                    break;
                }
                catch
                {
                    count1++;
                    Thread.Sleep(1000);
                }
            }
            if (count1 == 10)
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng");
                return;
            }

        }

        public static void followlist()
        {
            using (FileStream fStream = new FileStream("listUser.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        string[] tkmk = line.Split('|');

                        login(tkmk[0], tkmk[1]);
                        //sau khi đăng nhập thành công
                        string url1 = "https://www.instagram.com/leomessi/";
                        string url2 = "https://www.instagram.com/sontungmtp/";
                        for (int i = 0; i < 2; i++)
                        {
                            if (i == 0)
                            {
                                follow(url1);
                            }
                            else
                            {
                                follow(url2);
                            }
                        }
                        driver.Quit();
                    }
                }
            }
        }
    }
}
