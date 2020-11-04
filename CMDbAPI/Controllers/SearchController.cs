using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CMDbAPI.Controllers
{
    public class SearchController : Controller
    {

        private IMovieRepository movieRepository;

        public SearchController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            var listOfMovies = await movieRepository.GetAllMoviesContaining(searchString);

            try
            {
                var listOfMovies = await movieRepository.GetAllMoviesContaining(searchString);
                if (listOfMovies.Search == null)
                {
                    ViewBag.search = searchString;
                    return View(listOfMovies);
                }

                MovieDetailsViewModel movieDetailsViewModel;
                foreach (var movie in listOfMovies.Search)
                {
                    movieDetailsViewModel = await movieRepository.GetSummarySingleMovie(movie.ImdbID);
                    movie.Director = movieDetailsViewModel.Director;
                    movie.Genre = movieDetailsViewModel.Genre;
                    movie.Ratings = movieDetailsViewModel.Ratings;
                }
                return View(listOfMovies);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
