using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMDbAPI.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository movieRepository;

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

        }

        public async Task<IActionResult> Index()
        {
            //TODO: Erik använder en tom GetSummaryViewModel i hans repo
            //var model = await movieRepository.GetSummaryViewModel();

            // Döp denna till GetSummaryViewModel istället?
            var model = await movieRepository.GetSummary("tt3659388");
            
            return View(model);
        }


       



        //public async Task<IActionResult> Summary()
        //{
        //    //TODO: Fixa så att man kan skicka in både summary och country
        //    var summary = await movieRepository.GetSummary();
        //    // var model = new SummaryViewModel(summary);


        //    return View(summary);
        //}
    }
}
