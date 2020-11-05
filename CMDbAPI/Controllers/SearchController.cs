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
        public async Task<IActionResult> Index(string searchString)
        {
            try
            {                               
                //Creating an instance of SearchViewModel and get the results from search
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString);


                //How many pages is needed for the search results                
                int pageSize = 10;
                int totalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
                searchViewModel.TotalPages = totalPages;
                searchViewModel.SearchString = searchString;
                
               
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
                int nextPage = currentPage+1;
                SearchViewModel searchViewModel = new SearchViewModel();
                searchViewModel = await movieRepository.GetAllMoviesContaining(searchString, nextPage, cinematicType );
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

        [HttpGet]
        public async Task<IActionResult> PreviousPage(string searchString, int currentPage, string cinematicType)
        {
            try
            {
                int previousPage = currentPage - 1;
                SearchViewModel searchViewModel = new SearchViewModel();
                searchViewModel = await movieRepository.GetAllMoviesContaining(searchString, previousPage, cinematicType);
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

        [HttpGet]
        public async Task<IActionResult> CinematicSelection(SearchViewModel oldSearchViewModel)
        {
            try
            {
                int page = 1;
                var searchString = oldSearchViewModel.SearchString;
                var cinematicType = oldSearchViewModel.SelectedType;
                var searchViewModel = await movieRepository.GetAllMoviesContaining(searchString, page, cinematicType);

                if (searchViewModel.Search == null)
                {
                    searchViewModel.SearchString = searchString;
                    return View("index",searchViewModel);
                }
              

                //Number of pages needed for the search results  
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

