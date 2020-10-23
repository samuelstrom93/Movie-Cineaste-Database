using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
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
            var model = await movieRepository.GetSummarySingleMovie("tt3659388");



            var movies =await movieRepository.GetAllMovieRatings();


            Parameter parameter = new Parameter();
            {
                parameter.Count = movies.Count();
                //parameter.Count = 3; Bestämmer hur många som ska vara i topplistan

                //parameter.SortOrder = "Asc"; //Lägst först
                parameter.SortOrder = "Desc";//Högst först (defaultvärde)

                //parameter.Type = "popularity"; // Sorterar enbart efter hur många som har betygsatt filmen, struntar i hur stor skillnaden är mellan likes & dislikes
                parameter.Type = "ratings"; // Sorterar efter hur stor skillnaden är mellan likes & dislikes (defaultvärde)
            }



            //var toplist = await movieRepository.GetToplist(parameter);
            
            var topplistan = await movieRepository.GetTopList();


            return View(topplistan);
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
