using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Linq;

namespace IT008_Instagram
{
    public class PTBoTroAutoCmt
    {
        public static void autoCmt(ChromeDriver driver, string linkKH, int timeMax)
        {
            //vào trang cá nhân khách hàng
            driver.Url = linkKH;
            driver.Navigate();

            Thread.Sleep(5000);

            //vào bài post đầu tiên của khách hàng
            PTBoTroAutoTim.firstPost(driver, timeMax);


            //bắt đầu cmt toàn bộ list post của khách hàng
            cmtListPost(driver, timeMax);
          
        }



        //phục vụ cho hàm autoCmt
        //cmt bài post hiện hành
        private static void cmtpost(ChromeDriver driver, int timeMax,string noiDungCMT)
        {

            //nhấn button cmt
            int count1 = 0;
            while (count1 < timeMax)
            {
                try
                {
                    var btnCmt = driver.FindElement(By.CssSelector("._aamx > div:nth-child(1) > div:nth-child(1)"));
                    btnCmt.Click();
                    break;
                }
                catch
                {
                    count1++;
                    Thread.Sleep(1000);
                }
            }

            if (count1 == timeMax)
            {
                //nào sửa chỗ này sau
                MessageBox.Show("Thời gian chờ quá lâu.Chương trình sẽ dừng ở đây");
                return;
            }



            //nhập nội dung cmt
            Thread.Sleep(2000);
            int count2 = 0;
            while (count2 < timeMax)
            {
                try
                {
                    var cmt = driver.FindElement(By.CssSelector("textarea.x1i0vuye"));
                    cmt.SendKeys(noiDungCMT);
                    break;
                }
                catch
                {
                    count2++; Thread.Sleep(1000);
                }
            }

            if (count2 == timeMax)
            {
                //nào sửa chỗ này sau
                MessageBox.Show("Thời gian chờ quá lâu.Chương trình sẽ dừng ở đây");
                return;
            }


            //nhấn enter(tương đương với post cmt)
            Actions action = new Actions(driver);
            action.SendKeys(Keys.Enter).Perform();
            //chờ 5s cho nó load cmt lên(mạng lag là toang-nào có cách thì fix)
            Thread.Sleep(5000);

        }


        //cmt toàn bộ list post của khách hàng
        private static void cmtListPost(ChromeDriver driver,int timeMax)
        {
            string noiDungcmt = randomCmt();

            string urlOld = "";
            string urlNew = "";


            do
            {
                //cmt post hiện tại
                cmtpost(driver, timeMax,noiDungcmt);
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
        }


        //
        public static List<string> getDataCmtRandom()
        {
            List<string> listCmtRandom = new List<string>();

            using (FileStream fileStream = new FileStream("listCmtRandom.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        listCmtRandom.Add(line);
                    }

                }
            }
            return listCmtRandom;
        }


        //random cmt
        public static string randomCmt()
        {
            List<string> list = new List<string>();
            int max = 0;
            using (FileStream fStream = new FileStream("listCmtRandom.txt", FileMode.OpenOrCreate, FileAccess.Read))
            {
                using (StreamReader sr = new StreamReader(fStream))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        max++;
                        list.Add(line);
                    }
                }
            }


            var random = new Random();
            int minValue = 0;

            int randomNumber = random.Next(minValue, max);



            return list[randomNumber];
        }
    }
}
