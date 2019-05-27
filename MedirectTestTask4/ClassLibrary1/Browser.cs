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

        /// <summary>
        /// direct browser to a URL
        /// </summary>
        /// <param name="url">URL</param>
        public void SetUrl(string url)
        {
            if (!driver.Url.Equals(url))
                driver.Url = url;
        }

        /// <summary>
        /// return URL
        /// </summary>
        /// <returns>URL</returns>
        public string GetUrl()
        {
            return driver.Url;
        }

        /// <summary>
        /// close the browser
        /// </summary>
        public void Close()
        {
            driver.Close();
        }

        /// <summary>
        /// get an element using its ID
        /// </summary>
        /// <param name="id">ID</param>
        /// <returns>element</returns>
        public IWebElement GetElementById(string id)
        {
            return driver.FindElement(By.Id(id));
        }

        /// <summary>
        /// get an element using its XPath
        /// </summary>
        /// <param name="XPath">XPath</param>
        /// <returns>element</returns>
        public IWebElement GetElementByXPath(string XPath)
        {
            return driver.FindElement(By.XPath(XPath));
        }

        /// <summary>
        /// identify an element by its ID and type text into it
        /// </summary>
        /// <param name="id">element ID</param>
        /// <param name="text">text</param>
        public void TypeIntoID(string id, string text)
        {
            GetElementById(id).SendKeys(text);
        }

        /// <summary>
        /// identify an element by its XPath and click on it
        /// </summary>
        /// <param name="XPath">XPath</param>
        public void ClickOnXPath(string XPath)
        {
            IWebElement element = GetElementByXPath(XPath);
            ScrollIntoView(element);
            element.Click();
        }

        /// <summary>
        /// identify an element by its ID and click on it
        /// </summary>
        /// <param name="ID">ID</param>
        public void ClickOnID(string ID)
        {
            IWebElement element = GetElementById(ID);
            ScrollIntoView(element);
            element.Click();
        }

        /// <summary>
        /// scroll an element into view
        /// </summary>
        /// <param name="element">element</param>
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

        /// <summary>
        /// click on a hyperlink and return the result URL
        /// </summary>
        /// <param name="XPath">hyperlink XPath</param>
        /// <param name="destinationURL">desired destination</param>
        /// <returns>success state</returns>
        public bool TestHyperlink(string XPath, string destinationURL)
        {
            ClickOnXPath(XPath);

            string url = GetUrl();
            return url.Equals(destinationURL);
        }
    }
}
