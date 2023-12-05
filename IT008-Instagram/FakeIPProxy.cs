using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IT008_Instagram
{
    public static class FakeIPProxy
    {
        public static ChromeOptions fake(ChromeDriver driver)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--proxy-server=http://113.161.131.43:80");

           return options;
        }
    }
}
