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


        [HttpGet]
        public async Task<IActionResult> NextPage(string searchString, int currentPage)
        {
            try
            {
                //Variables to pass into method GetAllContaining + inparameter "searchString"
                int nextPage = currentPage+1;
                //string type = null; //TODO: lägg till en inparameter för "type"

                //Creating an instance of SearchViewModel and get the results from search
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString, nextPage);

                //How many pages is needed for the search results  
                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);

                //ViewBags for view         
                ViewBag.searchString = searchString;
                ViewBag.totalSearchHits = searchViewModel.totalResults;
                ViewBag.totalPages = totalPages;
                ViewBag.currentPage = nextPage;


                if (searchViewModel.Search == null)
                {
                    ViewBag.search = searchString;
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
                return View("index", searchViewModel);
            }
            catch (Exception)
            {

                throw;
            }
           
        }




        [HttpGet]
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {                               
                int currentPage = 1;                

                //Creating an instance of SearchViewModel and get the results from search
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString);

                //How many pages is needed for the search results                
                int pageSize = 10;
                var totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
                
               
                //ViewBags for the view
                ViewBag.searchString = searchString;
                ViewBag.totalSearchHits = searchViewModel.totalResults;
                ViewBag.totalPages = totalPages;
                ViewBag.currentPage = currentPage;


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



    }
}
