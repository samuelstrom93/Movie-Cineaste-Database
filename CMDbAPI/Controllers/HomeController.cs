using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMDbAPI.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository movieRepository;
        private Parameter parameter;
       

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

        }

        public async Task<IActionResult> Index()
        {


            

            parameter = new Parameter();
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);

            //TODO: flytta validering till modellen ist
            //foreach (var item in toplist.TopListMovies)
            //{
            //    if (string.IsNullOrEmpty(item.Poster) || item.Poster.Contains("N/A"))
            //    {
            //        item.Poster = "/img/NoPosterAvaible.png";
            //    }

            //    if (string.IsNullOrEmpty(item.Plot) || item.Plot.Contains("N/A"))
            //    {
            //        item.Plot = "No plot available";
            //    }
            //}



            return View(toplist);
        }


        [HttpGet]
        public async Task<IActionResult> FilterTopList(int count, string sortOrder, string type)
        {
            parameter = new Parameter(count, sortOrder, type);
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);
            return View("index", toplist);
        }

    }
}
