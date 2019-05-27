using Data;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Logic
{
    public static class InvalidMovieAPI
    {
        /// <summary>
        /// Get a movie using a string ID
        /// </summary>
        /// <param name="id">movie ID</param>
        /// <returns>movie</returns>
        public static Movie GetMovie(string id)
        {
            WebResponse webResponse = HTTP.Get("http://medirect-dev.azurewebsites.net/api/Movies/" + id);
            Stream responseStream = webResponse.GetResponseStream();

            Movie result = new Movie();
            using (var reader = new StreamReader(responseStream))
            {
                //get textual response
                string movieJSON = reader.ReadToEnd();

                //convert from JSON
                result = JsonConvert.DeserializeObject<Movie>(movieJSON);
            }

            webResponse.Close();

            return result;
        }

        /// <summary>
        /// Add movies
        /// </summary>
        /// <param name="newMovies">new movies</param>
        /// <returns>status</returns>
        public static string AddMovies(List<InvalidMovie2> newMovies)
        {
            return
                HTTP.Post("http://medirect-dev.azurewebsites.net/api/Movies/"
                , JsonConvert.SerializeObject(newMovies));
        }

        /// <summary>
        /// Add movies
        /// </summary>
        /// <param name="newMovies">new movies</param>
        /// <returns>status</returns>
        public static string AddMovies(List<InvalidMovie2b> newMovies)
        {
            return
                HTTP.Post("http://medirect-dev.azurewebsites.net/api/Movies/"
                , JsonConvert.SerializeObject(newMovies));
        }

        /// <summary>
        /// Add movies
        /// </summary>
        /// <param name="newMovies">new movies</param>
        /// <returns>status</returns>
        public static string AddMovies(List<InvalidMovie3> newMovies)
        {
            return
                HTTP.Post("http://medirect-dev.azurewebsites.net/api/Movies/"
                , JsonConvert.SerializeObject(newMovies));
        }

        /// <summary>
        /// Add movies
        /// </summary>
        /// <param name="newMovies">new movies</param>
        /// <returns>status</returns>
        public static string AddMovies(List<InvalidMovie4> newMovies)
        {
            return
                HTTP.Post("http://medirect-dev.azurewebsites.net/api/Movies/"
                , JsonConvert.SerializeObject(newMovies));
        }
    }
}
