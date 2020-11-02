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

        public async Task< IActionResult> Index(string searchString)
        {
            var listOfMovies = await movieRepository.GetAllMoviesContaining(searchString);

            //SummarySearchViewModel summarySearchViewModel = new SummarySearchViewModel();
            //summarySearchViewModel = listOfMovies;

            //for (int i = 0; i < listOfMovies.Count; i++)
            //{
            //    summarySearchViewModel. (listOfMovies[i]);
               
            //        if (string.IsNullOrEmpty(Poster) || movie.Poster.Contains("N/A"))
            //        {
            //            movie.Poster = "/img/NoPosterAvaible.png";
            //        }
                
            //    summarySearchViewModels.Add(movie);
            //}

            return View(listOfMovies);
        }
    }
}
