using System;
using System.IO;
using System.Net;
using System.Text;

namespace Logic
{
    public static class HTTP
    {
        /// <summary>
        /// Get the specified URL
        /// </summary>
        /// <param name="URL">URL</param>
        /// <returns>web response</returns>
        public static WebResponse Get(string URL)
        {
            try
            {
                //create a web request
                WebRequest webRequest = WebRequest.Create(URL);

                //get the response
                WebResponse webResponse = webRequest.GetResponse();
                return webResponse;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Put an object into the specified URL
        /// </summary>
        /// <param name="URL">URL</param>
        /// <param name="content">content</param>
        /// <returns>status</returns>
        public static string Put(string URL, string content)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(URL);
                //set the Method property of the request to PUT
                webRequest.Method = "PUT";

                //convert the parameter into a byte array  
                byte[] byteArray = Encoding.UTF8.GetBytes(content);

                //set the ContentType and ContentLength properties
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = byteArray.Length;

                //get the request stream
                Stream dataStream = webRequest.GetRequestStream();
                //write the data to the request stream
                dataStream.Write(byteArray, 0, byteArray.Length);
                //close the stream
                dataStream.Close();

                //get the response
                WebResponse response = webRequest.GetResponse();

                //close the response
                response.Close();

                //return the status
                return ((HttpWebResponse)response).StatusDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="URL">URL</param>
        /// <returns>status</returns>
        public static string Delete(string URL)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(URL);
                //set the Method property of the request to PUT
                webRequest.Method = "DELETE";
                
                //get the response
                WebResponse response = webRequest.GetResponse();

                //close the response
                response.Close();

                //return the status
                return ((HttpWebResponse)response).StatusDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Post
        /// </summary>
        /// <param name="URL">URL</param>
        /// <param name="content">content</param>
        /// <returns>status</returns>
        public static string Post(string URL, string content)
        {
            try
            {
                WebRequest webRequest = WebRequest.Create(URL);
                //set the Method property of the request to PUT
                webRequest.Method = "POST";

                //convert the parameter into a byte array  
                byte[] byteArray = Encoding.UTF8.GetBytes(content);

                //set the ContentType and ContentLength properties
                webRequest.ContentType = "application/json";
                webRequest.ContentLength = byteArray.Length;

                //get the request stream
                Stream dataStream = webRequest.GetRequestStream();
                //write the data to the request stream
                dataStream.Write(byteArray, 0, byteArray.Length);
                //close the stream
                dataStream.Close();

                //get the response
                WebResponse response = webRequest.GetResponse();

                //close the response
                response.Close();

                //return the status
                return ((HttpWebResponse)response).StatusDescription;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
