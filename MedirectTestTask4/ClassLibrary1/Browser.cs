using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Selenium
{
    public class Browser
    {
        IWebDriver driver = new FirefoxDriver();

        public void SetUrl(string url)
        {
            if (!driver.Url.Equals(url))
                driver.Url = url;
        }

        public string GetUrl()
        {
            return driver.Url;
        }

        public void Close()
        {
            driver.Close();
        }

        public IWebElement GetElementById(string id)
        {
            return driver.FindElement(By.Id(id));
        }

        public IWebElement GetElementByXPath(string XPath)
        {
            return driver.FindElement(By.XPath(XPath));
        }

        public void TypeIntoID(string id, string text)
        {
            GetElementById(id).SendKeys(text);
        }

        public void ClickOnXPath(string XPath)
        {
            IWebElement element = GetElementByXPath(XPath);
            ScrollIntoView(element);
            element.Click();
        }

        public void ClickOnID(string ID)
        {
            IWebElement element = GetElementById(ID);
            ScrollIntoView(element);
            element.Click();
        }

        public void ScrollIntoView(IWebElement element)
        {
            try
            {
                ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);
            }
            catch (Exception ex)
            {

            }
        }

        public bool TestHyperlink(string XPath, string destinationURL)
        {
            ClickOnXPath(XPath);

            string url = GetUrl();
            return url.Equals(destinationURL);
        }
    }
}
