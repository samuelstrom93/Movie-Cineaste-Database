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
        private readonly ILogger<HomeController> logger;

        private Parameter parameter;
       

        public HomeController(IMovieRepository movieRepository, ILogger<HomeController> logger)
        {
            this.movieRepository = movieRepository;
            this.logger = logger;

        }


        public async Task<IActionResult> Index()
        {
            
            parameter = new Parameter();
            try
            {
                var toplist = await movieRepository.GetTopListAggregatedData(parameter); //Har kommmenterat bort movie.add i movierepositorymetoden "GetTopListAggregatedData". Så att det ska bli ett error.

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
                //ErrorViewModel errorViewModel = new ErrorViewModel(ex, "Error", "Index");
               // return View(errorViewModel); //1:a 2:a paramtern. 1:a=vilken vy, 2:a model med inparametrar (new ErrorViewModel (ex, "Error", index)
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
               
                return View("Error");
            }
        }



        //[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        //public IActionResult Error()
        //{

        //    return View("Error",new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        //}

    }
}
