using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CMDbAPI.Controllers
{
    public class MovieDetailsController : Controller
    {
        private IMovieRepository movieRepository;

        public MovieDetailsController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

        }

        // GET: /<controller>/
        [HttpGet]
        public async Task<IActionResult> Index(string imdbID)
        {
            //TODO: skapa en konstruktor i SummaryViewModel där parametern imdbID förser med all info.
            // Får just nu problem med async
            //MovieSummaryDTO movieSummary = await movieRepository.GetSummarySingleMovie(imdbID);
            //MovieSummaryViewModel movieSummaryViewModel = new MovieSummaryViewModel();
            MovieDetailsViewModel movieSummaryViewModel = await movieRepository.GetSummarySingleMovie(imdbID);

            return View(movieSummaryViewModel);
        }
    }
}
