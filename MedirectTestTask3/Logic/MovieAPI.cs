using Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Logic
{
    public static class MovieAPI
    {
        /// <summary>
        /// Get a JSON of all movies
        /// </summary>
        /// <returns>a JSONof all movies</returns>
        public static string GetMoviesAsJSON()
        {
            WebResponse webResponse = HTTP.Get("http://medirect-dev.azurewebsites.net/api/Movies");
            Stream responseStream = webResponse.GetResponseStream();

            string result = "";
            using (var reader = new StreamReader(responseStream))
            {
                //get textual response
                result = reader.ReadToEnd();
            }

            webResponse.Close();

            return result;
        }

        /// <summary>
        /// Get a list of all movies
        /// </summary>
        /// <returns>a list of all movies</returns>
        public static List<Movie> GetMovies()
        {
            try
            {
                List<Movie> results = new List<Movie>();

                string movieJSON = GetMoviesAsJSON();

                //convert from JSON
                results = JsonConvert.DeserializeObject<List<Movie>>(movieJSON);
                return results;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get a movie by ID
        /// </summary>
        /// <param name="id">movie ID</param>
        /// <returns>movie</returns>
        public static Movie GetMovie(int id)
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
        /// Update a movie
        /// </summary>
        /// <param name="newMovie">new movie details</param>
        /// <returns>status</returns>
        public static string UpdateMovie(Movie newMovie)
        {
            return
                HTTP.Put("http://medirect-dev.azurewebsites.net/api/Movies/"
                + newMovie.id
                , JsonConvert.SerializeObject(newMovie));
        }

        /// <summary>
        /// Update a movie to a new id
        /// </summary>
        /// <param name="id">movie id</param>
        /// <param name="newMovie">new movie details</param>
        /// <returns>status</returns>
        public static string UpdateMovie(int id, Movie newMovie)
        {
            return
                HTTP.Put("http://medirect-dev.azurewebsites.net/api/Movies/"
                + id
                , JsonConvert.SerializeObject(newMovie));
        }

        /// <summary>
        /// Delete a movie
        /// </summary>
        /// <param name="id">ID of movie to delete</param>
        /// <returns>status</returns>
        public static string DeleteMovie(int id)
        {
            return
                HTTP.Delete(
                    "http://medirect-dev.azurewebsites.net/api/Movies/"
                    + id);
        }

        /// <summary>
        /// Add movies
        /// </summary>
        /// <param name="newMovies">new movies</param>
        /// <returns>status</returns>
        public static string AddMovies(List<Movie> newMovies)
        {
            return
                HTTP.Post("http://medirect-dev.azurewebsites.net/api/Movies/"
                , JsonConvert.SerializeObject(newMovies));
        }

        /// <summary>
        /// Gets an unused id
        /// </summary>
        /// <returns>id</returns>
        public static int GetNonExistentID()
        {
            List<Movie> movies = GetMovies().OrderBy(x=>x.id).ToList();

            if (movies.Count == 0)
                //return 0 since this id is definitely not in use
                return 0;
            int lastId = movies.Last().id;

            return lastId + 10;
        }
    }
}