using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace CMDbAPI.Controllers
{
    public class SearchController : Controller
    {

        private IMovieRepository movieRepository;

        public SearchController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        [HttpGet]
        public async Task< IActionResult> Index(string searchString)
        {

            try
            {

            var listOfMovies = await movieRepository.GetAllMoviesContaining(searchString);
                if (listOfMovies.Search == null)
                {
                    throw new Exception("Error");
                }
                return View(listOfMovies);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //if (listOfMovies.Search == null)
            //{
            //    ViewBag.search = searchString;
            //    return View(listOfMovies);
            //}
            

        }
    }
}
