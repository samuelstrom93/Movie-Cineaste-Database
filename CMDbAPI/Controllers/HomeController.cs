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

            //TODO: ta bort?
            var listOfMovies = await movieRepository.GetAllMoviesContaining("sunshine");


            var toplist = await movieRepository.GetTopListAggregatedDataDefaultValues();
            return View(toplist);
        }


        [HttpGet]
        public async Task<IActionResult> Search(int count, string sortOrder, string type)

        {
            parameter = new Parameter(count,sortOrder,type);


            //TODO: ta bort dessa?
            parameter.Count = count;
            parameter.SortOrder = sortOrder;
            parameter.Type = type;        

            //TODO: sätt ett defaultvärde som kan behållas i propertyn om värdet är N/A           
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);

            return View("index", toplist);    
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
