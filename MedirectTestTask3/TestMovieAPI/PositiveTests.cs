using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Data;
using Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestMovieAPI
{
    [TestClass]
    public class PositiveTests
    {
        [TestMethod]
        //Check that API URL is accessible online
        public void Test01_APIStatus()
        {
            try
            {
                HTTP.Get("http://medirect-dev.azurewebsites.net/api/Movies");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Inspect that movie data obeys the mandated structure
        //MANUAL INSPECTION
        public void Test02_MovieDatastructure()
        {
            try
            {
                //Assert.AreEqual(MessageBox.Show(MovieAPI.GetMoviesAsJSON(),"Is structure correct?", MessageBoxButtons.YesNo), DialogResult.Yes);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Check that API returns a list of all movies
        public void Test03_GetAllMovies()
        {
            try
            {
                MovieAPI.GetMovies();
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Check that each movie returned in test 3 is accessible individually
        public void Test04_GetAllMoviesIndividually()
        {
            try
            {
                List<Movie> movies = MovieAPI.GetMovies();
                foreach (Movie movie in movies)
                {
                    Movie APIMovie = MovieAPI.GetMovie(movie.id);
                    Assert.AreEqual(movie.id, APIMovie.id);
                    Assert.AreEqual(movie.name, APIMovie.name);
                    Assert.AreEqual(movie.rating, APIMovie.rating);
                    Assert.AreEqual(movie.genre, APIMovie.genre);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Check that a movie can be updated
        public void Test07_UpdateMovie()
        {
            try
            {
                List<Movie> movies = MovieAPI.GetMovies();

                if (movies.Count > 0)
                {
                    Movie toUpdate = movies[0];

                    toUpdate.name = Guid.NewGuid().ToString();

                    Assert.AreEqual(MovieAPI.UpdateMovie(toUpdate), "OK");
                }
                else
                    throw new Exception(Constants.emptyUpdate);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Check that movie is updated properly
        public void Test08_UpdateMovieDetails()
        {
            try
            {
                List<Movie> movies = MovieAPI.GetMovies();

                if (movies.Count > 0)
                {
                    Movie toUpdate = movies[0];

                    string newName = Guid.NewGuid().ToString();
                    string newGenre = Guid.NewGuid().ToString();
                    int newRating = new Random().Next(10);

                    toUpdate.name = newName;
                    toUpdate.genre = newGenre;
                    toUpdate.rating = newRating;

                    Assert.AreEqual(MovieAPI.UpdateMovie(toUpdate), "OK");

                    Movie updatedMovie = MovieAPI.GetMovie(toUpdate.id);

                    Assert.AreEqual(toUpdate.name, updatedMovie.name);
                    Assert.AreEqual(toUpdate.genre, updatedMovie.genre);
                    Assert.AreEqual(toUpdate.rating, updatedMovie.rating);
                }
                else
                    throw new Exception(Constants.emptyUpdate);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Delete a random movie and ensure that it is gone
        public void Test12_DeleteMovie()
        {
            try
            {
                List<Movie> movies = MovieAPI.GetMovies();

                if (movies.Count > 0)
                {
                    Movie toDelete = movies[new Random().Next(movies.Count)];

                    Assert.AreEqual(MovieAPI.DeleteMovie(toDelete.id), "OK");

                    Movie deletedMovie = MovieAPI.GetMovie(toDelete.id);

                    Assert.IsNull(deletedMovie);
                }
                else
                    throw new Exception("Cannot delete without any movies present");
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Add a new movie and check that it exists properly
        public void Test14_AddMovie()
        {
            try
            {
                List<Movie> toAdd = new List<Movie>()
                {
                    new Movie()
                    {
                        name = Guid.NewGuid().ToString(),
                        genre = Guid.NewGuid().ToString(),
                        rating = new Random().Next(10)
                    }
                };

                Assert.AreEqual(MovieAPI.AddMovies(toAdd), "OK");

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
        //Add a thousand movies
        public void Test19_BulkAdd()
        {
            try
            {
                int beforeCount = MovieAPI.GetMovies().Count;

                List<Movie> toAdd = new List<Movie>();

                for (int i = 0; i < 1000; i++)
                    toAdd.Add(
                        new Movie()
                        {
                            name = Guid.NewGuid().ToString(),
                            genre = Guid.NewGuid().ToString(),
                            rating = new Random().Next(10)
                        });

                Assert.AreEqual(MovieAPI.AddMovies(toAdd), "OK");

                //this part can fail on a live URL where movies are frequently added
                //to solve this, have the API return the inserted ID and check data using that
                int afterCount = MovieAPI.GetMovies().Count;

                Assert.AreEqual(beforeCount + 1000, afterCount);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [TestMethod]
        //Rapidly add a hundred movies
        public void Test20_MassAdd()
        {
            try
            {
                int beforeCount = MovieAPI.GetMovies().Count;

                for (int i = 0; i < 100; i++)
                {

                    List<Movie> toAdd = new List<Movie>(){
                        new Movie(){
                            name = Guid.NewGuid().ToString(),
                            genre = Guid.NewGuid().ToString(),
                            rating = new Random().Next(10)
                        } };

                    Assert.AreEqual(MovieAPI.AddMovies(toAdd), "OK");
                }

                //this part can fail on a live URL where movies are frequently added
                //to solve this, have the API return the inserted ID and check data using that
                int afterCount = MovieAPI.GetMovies().Count;

                Assert.AreEqual(beforeCount + 100, afterCount);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }
    }
}
