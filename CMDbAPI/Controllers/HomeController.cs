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
        private Parameter parameter= new Parameter();

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

        }

        public async Task<IActionResult> Index()
        {
            var toplist = await movieRepository.GetTopListAggregatedData();

            foreach (var movie in toplist)
            {
                if (string.IsNullOrEmpty(movie.Poster))
                {
                    movie.Poster = "~/ img /NoPosterAvaible.png";
                }
            }

           

            return View(toplist);
        }


        [HttpGet]
        public async Task<IActionResult> Search(int count, string sortOrder, string type)

        {
            parameter.Count = count;
            parameter.SortOrder = sortOrder;
            parameter.Type = type;        

                       
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);

            foreach (var movie in toplist)
            {
                if (movie.Poster.Contains("N/A"))
                {
                    movie.Poster = "/img/NoPosterAvaible.png";
                }
            }

            return View("index", toplist);    
        }     

    }
}
