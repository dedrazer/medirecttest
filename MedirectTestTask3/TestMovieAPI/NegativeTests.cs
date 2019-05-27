using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMovieAPI
{
    [TestClass]
    public class NegativeTests
    {
        [TestMethod]
        //Try to load a movie with a non-existent ID value
        public void Test05_GetNonExistentIDValue()
        {
            try
            {
                Movie APIMovie = MovieAPI.GetMovie(MovieAPI.GetNonExistentID());
                Assert.IsNull(APIMovie);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        //Try to load a movie with a string ID
        public void Test06_GetInvalidIDDataType()
        {
            Movie APIMovie = InvalidMovieAPI.GetMovie("test");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        //Update movie using a string rating
        public void Test09_InvalidMovieUpdate_DataType()
        {
            List<Movie> movies = MovieAPI.GetMovies();

            if (movies.Count > 0)
            {
                InvalidMovie1 toUpdate = new InvalidMovie1()
                {
                    id = movies[0].id,
                    name = movies[0].name,
                    genre = movies[0].genre,
                    rating = "science"
                };

                Assert.AreNotEqual(MovieAPI.UpdateMovie(toUpdate), "OK");
            }
            else
                throw new Exception(Constants.emptyUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        //Update movie using a non-existent ID

        //NB!!!: this failed since the Update in question is actually an upsert
        public void Test10_InvalidMovieUpdate_ID()
        {
            List<Movie> movies = MovieAPI.GetMovies();

            if (movies.Count > 0)
            {
                Movie toUpdate = movies[0];
                toUpdate.id = -1;

                Assert.AreNotEqual(MovieAPI.UpdateMovie(toUpdate), "OK");
            }
            else
                throw new Exception(Constants.emptyUpdate);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        //Try to change a movie’s ID using a conflicting ID
        public void Test11_UpdateMovieID()
        {
            List<Movie> movies = MovieAPI.GetMovies();

            if (movies.Count > 1)
            {
                Movie sample1 = movies[0];
                Movie sample2 = movies[1];

                Movie newMovie = new Movie()
                {
                    id = sample1.id,
                    name = sample2.name,
                    genre = sample2.genre,
                    rating = sample2.rating
                };

                Assert.AreNotEqual(MovieAPI.UpdateMovie(sample1.id, newMovie), "OK");
            }
            else
                throw new Exception("Cannot create conflict with only 1 movie");
        }

        [TestMethod]
        //Delete a non-existent movie
        //this may break if the ID or any movie is inserted in between methods
        public void Test13_DeleteInvalidMovie()
        {
            try
            {
                int beforeCount = MovieAPI.GetMovies().Count;
                Assert.AreEqual(
                    MovieAPI.DeleteMovie(MovieAPI.GetNonExistentID()),
                    "OK");
                int afterCount = MovieAPI.GetMovies().Count;

                Assert.AreEqual(beforeCount, afterCount);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        //Add a movie with missing title
        public void Test16_AddInvalidMovie_DataStructure1()
        {
            List<InvalidMovie2> toAdd = new List<InvalidMovie2>()
                {
                    new InvalidMovie2()
                    {
                        genre = Guid.NewGuid().ToString(),
                        rating = new Random().Next(10)
                    }
                };

            InvalidMovieAPI.AddMovies(toAdd);
        }

        [TestMethod]
        //Add a movie with missing rating
        public void Test16b_AddInvalidMovie_DataStructure1b()
        {
            List<InvalidMovie2b> toAdd = new List<InvalidMovie2b>()
                {
                    new InvalidMovie2b()
                    {
                        name = Guid.NewGuid().ToString(),
                        genre = Guid.NewGuid().ToString()
                    }
                };

            Assert.AreNotEqual(InvalidMovieAPI.AddMovies(toAdd), "OK");
        }

        [TestMethod]
        //Add a movie with excess fields rating
        public void Test17_AddInvalidMovie_DataStructure2()
        {
            try
            {
                List<InvalidMovie3> toAdd = new List<InvalidMovie3>()
                {
                    new InvalidMovie3()
                    {
                        name = Guid.NewGuid().ToString(),
                        genre = Guid.NewGuid().ToString(),
                        rating = new Random().Next(10),
                        gibberish = "gibberish"
                    }
                };

                Assert.AreEqual(InvalidMovieAPI.AddMovies(toAdd), "OK");

                //this part can fail on a live URL where movies are frequently added
                //to solve this, have the API return the inserted ID and check data using that
                List<Movie> movies = MovieAPI.GetMovies();

                if (movies.Count > 0)
                {
                    Movie latestMovie = movies[movies.Count - 1];

                    Assert.AreEqual(toAdd[0].name, latestMovie.name);
                    Assert.AreEqual(toAdd[0].genre, latestMovie.genre);
                    Assert.AreEqual(toAdd[0].rating, latestMovie.rating);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        //Add a movie with a string ID and rating
        public void Test18_AddInvalidMovie_DataTypes()
        {
            List<InvalidMovie4> toAdd = new List<InvalidMovie4>()
                {
                    new InvalidMovie4()
                    {
                        id = Guid.NewGuid().ToString(),
                        name = Guid.NewGuid().ToString(),
                        genre = Guid.NewGuid().ToString(),
                        rating = Guid.NewGuid().ToString()
                    }
                };

            Assert.AreNotEqual(InvalidMovieAPI.AddMovies(toAdd), "OK");
        }

        [TestMethod]
        //Add a movie with an excessively long name
        public void Test21_AddOverloadedString()
        {
            try
            {
                Movie _toAdd = new Movie()
                {
                    genre = Guid.NewGuid().ToString(),
                    rating = new Random().Next(10)
                };

                for (int i = 0; i < 20000; i++)
                    _toAdd.name += "a";

                List<Movie> toAdd = new List<Movie>()
                {
                    _toAdd
                };

                Assert.AreEqual(MovieAPI.AddMovies(toAdd), "OK");
            }

            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
