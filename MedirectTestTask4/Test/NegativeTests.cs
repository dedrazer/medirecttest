using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Selenium;

namespace Test
{
    [TestClass]
    public class NegativeTests
    {
        Browser browser = new Browser();

        [TestMethod]
        //Check that the website does not tolerate invalid phone numbers by inserting alphabetical characters there
        public void Test03_FillInvalidContactForm_DataType()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");

                ContactForm.FillContactForm(browser, "John", "Doe", "johndoenut@gmail.com", 1, "test", "this is a test");

                Assert.AreNotEqual(browser.GetElementById("phoneNumber").Text, "test");
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
        //Check that the website does not tolerate empty form submissions
        public void Test05_SubmitEmptyForm_Name()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");

                ContactForm.FillContactForm(browser, "", "Doe", "johndoenut@gmail.com", 1, "test", "this is a test");

                Assert.AreEqual(browser.GetElementById("errorRow").GetCssValue("display"), "block");
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
        //Check that the website hides errors after the user fixes them
        public void Test06_CorrectEmptyForm_Name()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");

                ContactForm.FillContactForm(browser, "", "Doe", "johndoenut@gmail.com", 1, "21234567", "this is a test");

                Assert.AreEqual(browser.GetElementById("errorRow").GetCssValue("display"), "block");

                ContactForm.FillContactForm(browser, "John", "", "", 1, "", "");

                Assert.AreEqual(browser.GetElementById("errorRow").GetCssValue("display"), "none");
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
        //Ensure that numeric fields tolerate mixed input
        public void Test07_FillMixedContactForm_DataType()
        {
            try
            {
                browser.SetUrl("https://www.medirect.com.mt/contact");

                ContactForm.FillContactForm(browser, "John", "Doe", "johndoenut@gmail.com", 1, "32f423v23", "this is a test");

                Assert.AreEqual(browser.GetElementById("phoneNumber").GetAttribute("value"), "3242323");
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
    }
}
