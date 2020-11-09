using System;
using System.Threading.Tasks;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;



namespace CMDbAPI.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository movieRepository;
        private IParameter parameter;
        private HomeViewModel homeViewModel;

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
            parameter = new Parameter();
            homeViewModel = new HomeViewModel(parameter);
        }

        /// <summary>
        /// Hämtar topplistan från CMDB-api och hämtar en del data ifrån OMDB - Plot och Title.
        /// Default är att hämta de fem filmerna med högst rating (defaultvärde på Parameter-klassen)  
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            try
            {
                homeViewModel.TopListMovies = await movieRepository.GetTopListAggregatedData(homeViewModel.Parameter);
                return View(homeViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Filterar vilka filmer som ska visas mha. inparametrar. 
        /// </summary>
        /// <param name="count"></param>
        /// <param name="sortOrder"></param>
        /// <param name="sortType"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Filter(string count, string sortOrder, string sortType)
        {
            try
            {
                homeViewModel.Parameter.Count = int.Parse(count);
                homeViewModel.Parameter.SortOrder = sortOrder;
                homeViewModel.Parameter.Type = sortType;
                homeViewModel.TopListMovies = await movieRepository.GetTopListAggregatedData(homeViewModel.Parameter);
                return View("index", homeViewModel);
            }

            catch (Exception)
            {
                throw;
            }

        }



    }
}
