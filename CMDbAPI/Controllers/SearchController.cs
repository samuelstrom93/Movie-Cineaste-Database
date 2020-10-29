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

            List<SummarySearchViewModel> summarySearchViewModels = new List<SummarySearchViewModel>();

            for (int i = 0; i < listOfMovies.Search.Count; i++)
            {
                SummarySearchViewModel movie = new SummarySearchViewModel(listOfMovies.Search[i]);
               
                    if (string.IsNullOrEmpty(movie.Poster) || movie.Poster.Contains("N/A"))
                    {
                        movie.Poster = "/img/NoPosterAvaible.png";
                    }
                
                summarySearchViewModels.Add(movie);
            }

            return View(summarySearchViewModels);
        }
    }
}
