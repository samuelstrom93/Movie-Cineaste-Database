using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CMDbAPI.DataTypes;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace CMDbAPI.Controllers
{
    public class SearchController : Controller
    {

        private IMovieRepository movieRepository;
        private CinematicType cinematicType;

        public SearchController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
            this.cinematicType = new CinematicType();
        }

        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string type=null)
        {
            try
            {                               
                int firstPage = 1;                

                //Creating an instance of SearchViewModel and get the results from search
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString);
                //searchViewModel.SearchString = searchString;

                //How many pages is needed for the search results                
                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
                
               
                //ViewBags for the view
                ViewBag.searchString = searchString;
                ViewBag.totalSearchHits = searchViewModel.totalResults;
                ViewBag.totalPages = totalPages;
                ViewBag.currentPage = firstPage;
                ViewBag.firstPage = firstPage;


                if (searchViewModel.Search == null)
                {

                    return View(searchViewModel);
                }

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


        [HttpGet]
        public async Task<IActionResult> NextPage(string searchString, int currentPage, string cinematicType)
        {
            try
            {               
                //Variables to pass into method GetAllContaining + inparameter "searchString"
                int nextPage = currentPage + 1;                

                //Creating an instance of SearchViewModel and get the results from search
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString, nextPage, cinematicType);
                searchViewModel.SelectedType = cinematicType;
              

                //Number of pages needed for the search results  
                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);

                //ViewBags for view         
                ViewBag.searchString = searchString;
                ViewBag.totalSearchHits = searchViewModel.totalResults;
                ViewBag.totalPages = totalPages;
                ViewBag.currentPage = nextPage;
                ViewBag.cinematicType = cinematicType;

                if (searchViewModel.Search == null)
                {
                    ViewBag.search = searchString;
                    return View("index",searchViewModel);
                }

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

        [HttpGet]
        public async Task<IActionResult> PreviousPage(string searchString, int currentPage, string cinematicType)
        {
            try
            {
                int firstPage = 1;
                //Variables to pass into method GetAllContaining + inparameter "searchString"
                int nextPage = currentPage-1;

                //Creating an instance of SearchViewModel and get the results from search
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString, nextPage, cinematicType);
                searchViewModel.SelectedType = cinematicType;


                //Number of pages needed for the search results  
                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);

                //ViewBags for view         
                ViewBag.searchString = searchString;
                ViewBag.totalSearchHits = searchViewModel.totalResults;
                ViewBag.totalPages = totalPages;
                ViewBag.currentPage = nextPage;
                ViewBag.cinematicType = cinematicType;
                ViewBag.firstPage = firstPage;

                if (searchViewModel.Search == null)
                {
                    ViewBag.search = searchString;
                    return View("index", searchViewModel);
                }

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

        [HttpGet]
        public async Task<IActionResult> CinematicSelection(SearchViewModel oldSearchViewModel, string searchString)
        {

            int page = 1;
            var cinematicType = oldSearchViewModel.SelectedType;
            var searchViewModel=await movieRepository.GetAllMoviesContaining(searchString, page, cinematicType);
           

            //Number of pages needed for the search results  
            int pageSize = 10;
            var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);

            //ViewBags for view         
            ViewBag.searchString = searchString;
            ViewBag.totalSearchHits = searchViewModel.totalResults;
            ViewBag.totalPages = totalPages;
            ViewBag.currentPage = page;
            ViewBag.firstPage = page;
            ViewBag.cinematicType = cinematicType;
            return View("index",searchViewModel);          
        }
    }
}
