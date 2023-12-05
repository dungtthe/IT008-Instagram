using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.DevTools.V117.Debugger;

namespace IT008_Instagram
{
    public class LogAcc
    {
        
        public static void Log(string user, string pass, ChromeDriver driver)
        {

            //driver = new ChromeDriver();
            //Vào instagram
            driver.Url = "https://www.instagram.com/";
            driver.Navigate();

            //Đăng nhập
            
            int count1 = 0;
            int maxTime = 10;
            while (count1 < maxTime)
            {
                try
                {
                    var username = driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[1]/div/label/input"));
                    username.SendKeys(user);
                    break;
                }
                catch
                {
                    count1++;
                    Thread.Sleep(1000);
                }
                if (count1 == maxTime)
                {
                    MessageBox.Show("Thời gian chờ quá lâu, thoát khỏi chương trình");
                    return;
                }
            }
            
            int count2 = 0;
            while (count2 < maxTime)
            {
                try
                {
                    var password = driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[2]/div/label/input"));
                    password.SendKeys(pass);
                    break;
                }
                catch
                {
                    count2++;
                    Thread.Sleep(1000);
                }
                if (count2 == maxTime)
                {
                    MessageBox.Show("Thời gian chờ quá lâu, thoát khỏi chương trình");
                    return;
                }
            }

            int count3 = 0;
            while (count3 < maxTime)
            {
                try
                {
                    var btnlog = driver.FindElement(By.XPath("//*[@id=\"loginForm\"]/div/div[3]/button/div"));
                    btnlog.Click();
                    break;
                }
                catch
                {
                    count3++;
                    Thread.Sleep(1000);
                }
                if (count3 == maxTime)
                {
                    MessageBox.Show("Thời gian chờ quá lâu, thoát khỏi chương trình");
                    return;
                }
            }
        }
    }
}
