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
            homeViewModel.TopListMovies = await movieRepository.GetTopListAggregatedData(parameter, homeViewModel);
            return View(homeViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Filter(string selectedCount, string selectedSortOrder, string selectedType)
        {
            parameter = new Parameter(int.Parse(selectedCount), selectedSortOrder, selectedType);
            homeViewModel.TopListMovies = await movieRepository.GetTopListAggregatedData(parameter, homeViewModel);
            return View("index", homeViewModel);
        }
    }
}
