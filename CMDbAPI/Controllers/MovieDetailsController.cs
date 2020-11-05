using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;


namespace CMDbAPI.Controllers
{
    public class MovieDetailsController : Controller
    {
        private IMovieRepository movieRepository;

        public MovieDetailsController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        /// <summary>
        /// Hämtar relevenat information som vi vill visa i gränssnittet. Identiferar filmen i OMDB mha. av ImdbID
        /// </summary>
        /// <param name="imdbID"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string imdbID)
        {
            try
            {
                MovieDetailsViewModel movieDetailsViewModel = await movieRepository.GetSummarySingleMovie(imdbID);
                return View(movieDetailsViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
