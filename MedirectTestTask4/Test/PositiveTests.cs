using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium;

namespace Test
{
    [TestClass]
    public class PositiveTests
    {
        Browser browser = new Browser();

        [TestMethod]
        //Check that the contact page is accessible
        public void Test01_LoadContactPage()
        {
            try
            {
                string url = "https://www.medirect.com.mt/contact";
                browser.SetUrl(url);
                Assert.AreEqual(url, browser.GetUrl());

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Fill out and submit a valid contact form
        public void Test02_SubmitContactForm()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");

                ContactForm.FillContactForm(browser, "John", "Doe", "johndoenut@gmail.com", 1, "21234567", "this is a test");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                browser.Close();
            }
        }

        [TestMethod]
        //Check that the mail link is correct
        public void Test08_CheckMailLink()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");

                Assert.AreEqual(browser.GetElementByXPath("/html/body/div[1]/div[3]/div/div[2]/div[2]/div/div/div/div/p/a").GetAttribute("href"), "mailto:info@medirect.com.mt");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
            finally
            {
                browser.Close();
            }
        }

        [TestMethod]
        //Check that the save page is accessible
        public void Test09_LoadSavePage()
        {
            try
            {
                string url = "https://www.medirect.com.mt/save";
                browser.SetUrl(url);
                Assert.AreEqual(url, browser.GetUrl());

            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the mixed funds page is accessible
        public void Test10_LoadMixedFundsPage()
        {
            try
            {
                string url = "https://www.medirect.be/mutual-funds/selection-of-top-funds/mixed-funds";
                browser.SetUrl(url);
                Assert.AreEqual(url, browser.GetUrl());
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the home nav button works
        //this is sometimes broken by ReCAPTCHA
        //if so, rerun failed tests
        public void Test11_NavigateHomePage()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");
                Assert.AreEqual(browser.TestHyperlink("/html/body/div[1]/div[1]/nav/div/div[2]/div/div/ul/li[2]", "https://www.medirect.com.mt/"), true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the save nav button works
        //this is sometimes broken by ReCAPTCHA
        //if so, rerun failed tests
        public void Test12_NavigateSavePage()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");
                Assert.AreEqual(browser.TestHyperlink("/html/body/div[1]/div[1]/nav/div/div[2]/div/div/ul/li[3]", "https://www.medirect.com.mt/save"), true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the invest nav button works
        //this is sometimes broken by ReCAPTCHA
        //if so, rerun failed tests
        public void Test13_NavigateInvestPage()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");
                Assert.AreEqual(browser.TestHyperlink("/html/body/div[1]/div[1]/nav/div/div[2]/div/div/ul/li[4]", "https://www.medirect.com.mt/invest"), true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the wealth management nav button works
        //this is sometimes broken by ReCAPTCHA
        //if so, rerun failed tests
        public void Test14_NavigateWealthManagementPage()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");
                Assert.AreEqual(browser.TestHyperlink("/html/body/div[1]/div[1]/nav/div/div[2]/div/div/ul/li[5]", "https://www.medirect.com.mt/wealth-management"), true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the updates nav button works
        //this is sometimes broken by ReCAPTCHA
        //if so, rerun failed tests
        public void Test15_NavigateUpdatesPage()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");
                Assert.AreEqual(browser.TestHyperlink("/html/body/div[1]/div[1]/nav/div/div[2]/div/div/ul/li[6]", "https://www.medirect.com.mt/updates"), true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }

        [TestMethod]
        //Check that the about us nav button works
        //this is sometimes broken by ReCAPTCHA
        //if so, rerun failed tests
        public void Test16_NavigateAboutUsPage()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");
                Assert.AreEqual(browser.TestHyperlink("/html/body/div[1]/div[1]/nav/div/div[2]/div/div/ul/li[7]", "https://www.medirect.com.mt/about-us"), true);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }

            browser.Close();
        }
    }
}
