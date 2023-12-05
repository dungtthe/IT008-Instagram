using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml.Linq;

namespace IT008_Instagram
{
    public class PTBoTroAutoTim
    {

        //thả tim 1 khách hàng
        public static void autoTim(ChromeDriver driver, string linkKH,int timeMax)
        {
            //vào trang cá nhân khách hàng
            driver.Url = linkKH;
            driver.Navigate();

            Thread.Sleep(5000);

            //vào bài post đầu tiên của khách hàng
             firstPost(driver, timeMax);

            //bắt đầu thả tim toàn bộ post của khách hàng
             timListPost(driver, timeMax);

        }



        //phục vụ cho hàm autoTim
        //vào bài post đầu tiên
        private static void firstPost(ChromeDriver driver,int timeMax)
        {
            int timePostFirst = 0;
            while (timePostFirst < timeMax)
            {
                try
                {
                    var firstPost = driver.FindElement(By.ClassName("_aagu"));
                    firstPost.Click();
                    break;
                }
                catch
                {
                    Thread.Sleep(1000);
                    timePostFirst++;
                }
            }
            if (timePostFirst == 10)
            {
                MessageBox.Show("Thời gian chờ quá lâu,chương trình tự động dừng!");
                driver.Quit();
                return;
            }
        }


        //phục vụ cho hàm autoTim
        //thả tim toàn bộ ảnh/video có trong 1 bài post
        private static bool timPost(ChromeDriver driver,int timeMax)
        {
            //string urlOld = "";
            //string urlNew = "";
            //do
            //{
                Thread.Sleep(2000);

            //urlOld = driver.Url;

            try//nếu thả tim rồi
            {
                //cái này tìm thấy khi thả tim rồi
                driver.FindElement(By.CssSelector(".xxk16z8 > path:nth-child(2)"));
            }
            catch//nếu chưa thả tim
            {
                try
                {
                    //var btnTim = driver.FindElement(By.XPath("/html/body/div[7]/div[1]/div/div[3]/div/div/div/div/div[2]/div/article/div/div[2]/div/div/div[2]/section[1]/span[1]/div/div"));
                    var btnTim = driver.FindElement(By.CssSelector("._aamw > div:nth-child(1) > div:nth-child(1)"));
                    btnTim.Click();
                }
                catch
                {
                    //kiểu instagram nó thay xpath
                    MessageBox.Show("Có lỗi khi thả tim ,vui lòng liên hệ DUNGTT để sửa");
                    return false;
                }
            }






            //cái này k cần vì nó chức năng tim là tim tổng luôn r
            ////sang ảnh/video kế tiếp của bài post để tim
            //Thread.Sleep(1000);
            //try
            //{
            //    var btnTiepTheo = driver.FindElement(By.CssSelector("._9zm2"));
            //    btnTiepTheo.Click();
            //    Thread.Sleep(1000);
            //    urlNew = driver.Url;
            //}
            //catch
            //{

            //}

            //}while(urlOld!= urlNew);

            return true;
        }


        //thả tim toàn bộ post của 1 khách hàng
        private static bool timListPost(ChromeDriver driver,int timeMax)
        {
            string urlOld = "";
            string urlNew = "";


            do
            {

                //tim post hiện tại
                timPost(driver, timeMax);
                Thread.Sleep(2000);
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
            
            return true;
        }
    }
}
