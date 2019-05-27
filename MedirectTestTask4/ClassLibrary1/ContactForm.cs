using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Selenium
{
    public static class ContactForm 
    {
        public static void FillContactForm(
            Browser browser, string name, string surname, string email, int subjectID, string phoneNumber, string message)
        {
            //accept cookies
            try
            {
                browser.ClickOnXPath("/html/body/div[1]/div[4]/div/div[2]/input");
            }
            catch
            { }

            //fill form
            browser.TypeIntoID("firstName", name);
            browser.TypeIntoID("lastName", surname);
            browser.TypeIntoID("emailtxt", email);
            browser.TypeIntoID("phoneNumber", phoneNumber);
            browser.TypeIntoID("message", message);
            
            browser.ClickOnXPath("/html/body/div[1]/div[3]/div/div[1]/div/form/div[2]/div[4]/div/div/button");
            browser.ClickOnXPath("/html/body/div[1]/div[3]/div/div[1]/div/form/div[2]/div[4]/div/div/div/ul/li["+ ((subjectID % 4) + 2) + "]/a");

            //submit
            browser.ClickOnID("sendbutton");
        }
    }
}
