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
        public async Task< IActionResult> Index(string searchString)
        {
            var listOfMovies = await movieRepository.GetAllMoviesContaining(searchString);
            List<SummarySearchViewModel> summarySearchViewModels = new List<SummarySearchViewModel>();


            if (listOfMovies.Search != null)
            {
                foreach (var movie in listOfMovies.Search)
                {
                    SummarySearchViewModel summarySearchViewModel = new SummarySearchViewModel(movie);
                    summarySearchViewModels.Add(summarySearchViewModel);
                }
                return View(summarySearchViewModels);
            }
            else
            {
                return View();
            }


            //for (int i = 0; i < listOfMovies.Search.Count; i++)
            //    {
            //        SummarySearchViewModel movie = new SummarySearchViewModel(listOfMovies.Search[i]);
            //        summarySearchViewModels.Add(movie);
            //    }
            
            

            //return View(summarySearchViewModels) ?? View();
        }
    }
}
