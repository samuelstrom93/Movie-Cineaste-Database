using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Localization;
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

        /// <summary>
        /// Vi hämtar resultat ifrån OMDB utifrån inparameter 'searchString'.
        /// Vi använder sedan en pagination-lösning för att navigera till föregående och nästa sida.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {

                // Instansierar en vymodell och hämtar resultatet ifrån sökningen
                var searchViewModel = await movieRepository.GetAllCinematicTypesContaining(searchString);


                // Hur många sidor som behövs för kunna navigera mellan alla sökningsträffar
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
                searchViewModel.TotalPages = totalPages;
                searchViewModel.SearchString = searchString;
                
               
                if (searchViewModel.Search == null)
                {
                    return View(searchViewModel);
                }

                // Hämtar mer detaljer till varje respektive film ifrån OMDB.
                MovieDetailsViewModel movieDetailsViewModel;
                foreach (var movie in searchViewModel.Search)
                {
                    movieDetailsViewModel = await movieRepository.GetSummarySingleMovie(movie.ImdbID);
                    movie.Director = movieDetailsViewModel.Director;
                    movie.Genre = movieDetailsViewModel.Genre;
                    movie.Ratings = movieDetailsViewModel.Ratings;
                }
                return View(searchViewModel);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Används för att navigera framåt.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="currentPage"></param>
        /// <param name="cinematicType">Film/serie/spel</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> NextPage(string searchString, int currentPage, string cinematicType)
        {
            try
            {
                int nextPage = currentPage+1;
                SearchViewModel searchViewModel = new SearchViewModel();
                searchViewModel = await movieRepository.GetAllCinematicTypesContaining(searchString, nextPage, cinematicType );
                searchViewModel.SearchString = searchString;
                searchViewModel.CurrentPage = nextPage;
                searchViewModel.SelectedType = cinematicType;

                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
                searchViewModel.TotalPages = totalPages;

                MovieDetailsViewModel movieDetailsViewModel;
                foreach (var movie in searchViewModel.Search)
                {
                    movieDetailsViewModel = await movieRepository.GetSummarySingleMovie(movie.ImdbID);
                    movie.Director = movieDetailsViewModel.Director;
                    movie.Genre = movieDetailsViewModel.Genre;
                    movie.Ratings = movieDetailsViewModel.Ratings;
                }    
                
                return View("index", searchViewModel);
            }
            catch (Exception)
            {

                throw;
            }         
        }

        /// <summary>
        /// Används för att navigera bakåt
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="currentPage"></param>
        /// <param name="cinematicType">Film/serie/spel</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> PreviousPage(string searchString, int currentPage, string cinematicType)
        {
            try
            {
                int previousPage = currentPage - 1;
                SearchViewModel searchViewModel = new SearchViewModel();
                searchViewModel = await movieRepository.GetAllCinematicTypesContaining(searchString, previousPage, cinematicType);
                searchViewModel.SearchString = searchString;
                searchViewModel.CurrentPage = previousPage;
                searchViewModel.SelectedType = cinematicType;

                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
                searchViewModel.TotalPages = totalPages;

                MovieDetailsViewModel movieDetailsViewModel;
                foreach (var movie in searchViewModel.Search)
                {
                    movieDetailsViewModel = await movieRepository.GetSummarySingleMovie(movie.ImdbID);
                    movie.Director = movieDetailsViewModel.Director;
                    movie.Genre = movieDetailsViewModel.Genre;
                    movie.Ratings = movieDetailsViewModel.Ratings;
                }

                return View("index", searchViewModel);
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Metod för att välja typ av medium som användaren vill visa i sökningen - Film, serie eller spel.
        /// </summary>
        /// <param name="oldSearchViewModel"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> CinematicSelection(SearchViewModel oldSearchViewModel)
        {
            try
            {
                int page = 1;
                var searchString = oldSearchViewModel.SearchString;
                var cinematicType = oldSearchViewModel.SelectedType;
                var searchViewModel = await movieRepository.GetAllCinematicTypesContaining(searchString, page, cinematicType);

                if (searchViewModel.Search == null)
                {
                    searchViewModel.SearchString = searchString;
                    return View("index",searchViewModel);
                }
              
                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);

                searchViewModel.SearchString = searchString;
                searchViewModel.SelectedType = cinematicType;
                searchViewModel.TotalPages = totalPages;

                MovieDetailsViewModel movieDetailsViewModel;
                foreach (var movie in searchViewModel.Search)
                {
                    movieDetailsViewModel = await movieRepository.GetSummarySingleMovie(movie.ImdbID);
                    movie.Director = movieDetailsViewModel.Director;
                    movie.Genre = movieDetailsViewModel.Genre;
                    movie.Ratings = movieDetailsViewModel.Ratings;
                }

                return View("index", searchViewModel);
            }
            catch (Exception)
            {
                throw;
            }
           
         
        }
    }
}

