using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            try
            {              
                var toplist = await movieRepository.GetTopListAggregatedData(parameter);

                //TODO: Om antalet filmer i databasen är 0, så ska en text visas i vyn om att inga filmer finns lagrade i databasen.
                if (toplist.TopListMovies.Count == 0)
                {
                    return View("Error");
                }               
                return View(toplist);
            }
            catch (Exception)
            {
                throw;               
            }        
        }


        [HttpGet]
        public async Task<IActionResult> FilterTopList(int count, string sortOrder, string type)
        {
            parameter = new Parameter(count, sortOrder, type);

            try
            {
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);
            return View("index", toplist);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
