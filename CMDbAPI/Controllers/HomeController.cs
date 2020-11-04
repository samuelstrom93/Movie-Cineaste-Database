using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMDbAPI.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository movieRepository;
        private Parameter parameter;
        private HomeViewModel homeViewModel;

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
            parameter = new Parameter();
            homeViewModel = new HomeViewModel(parameter);
        }

        public async Task<IActionResult> Index()
        {
            homeViewModel.TopListMovies = await movieRepository.GetTopListAggregatedData(homeViewModel);

            return View(homeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string count, string sortOrder, string sortType)
        {
            homeViewModel.Parameter.Count = int.Parse(count);
            homeViewModel.Parameter.SortOrder = sortOrder;
            homeViewModel.Parameter.Type = sortType;
            homeViewModel.TopListMovies = await movieRepository.GetTopListAggregatedData(homeViewModel);
            return View("index", homeViewModel);
        }
    }
}
