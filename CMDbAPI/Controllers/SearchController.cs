using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CMDB.Extensions;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMDbAPI.Controllers
{
    public class SearchController : Controller
    {

        private IMovieRepository movieRepository;
        private SearchViewModel searchViewModelHelper;
        private SearchViewModel searchViewModel = new SearchViewModel();
        private int pageSize = 10;

        public SearchController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;
        }

        /// <summary>
        /// Inspiration från Microsoft dokumentation: https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/sort-filter-page?view=aspnetcore-3.1#feedback
        /// Vi hämtar resultat ifrån OMDB utifrån inparameter 'searchString'.
        /// Vi använder sedan en pagination-lösning för att navigera till föregående och nästa sida.
        /// </summary>
        /// <param name="searchString"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, int? pageNumber, string currentFilter, string sortOrder)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            pageNumber = pageNumber ?? 1;
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CurrentFilter"] = searchString;


            
            if (!String.IsNullOrEmpty(searchString))
            {
                int searchPageNmbr = 1;
                do
                {
                    searchViewModelHelper = await movieRepository.GetAllCinematicTypesContaining(searchString, searchPageNmbr);

                    //if (searchViewModelHelper.Search.Count == 0)
                    //{
                    //    break;
                    //}

                    foreach (var movie in searchViewModelHelper.Search)
                    {
                        searchViewModel.Search.Add(movie);
                    }
                    searchPageNmbr++;
                    
                } while (searchViewModelHelper.totalResults > searchViewModel.Search.Count());

            }

            searchViewModelHelper = await movieRepository.GetAllCinematicTypesContaining(searchString);
            if (searchViewModelHelper.totalResults == 0)
            {
                return View(searchViewModel);

            }
            searchViewModel.totalResults = searchViewModelHelper.totalResults;

            searchViewModel.PageIndex = (int)pageNumber;
            searchViewModel.TotalPages = (int)Math.Ceiling(searchViewModel.totalResults / (double)pageSize);
            int excludeRecords = (int)((pageSize * pageNumber) - pageSize);

            SelectListItem item;

            for (int i = 1; i <= searchViewModel.TotalPages; i++)
            {
                item = new SelectListItem
                {
                    Value = i.ToString(),
                    Text = i.ToString(),
                };
                searchViewModel.PageList.Add(item);
            };


            ViewData["TitleSortParm"] = sortOrder == "Title" ? "title_desc" : "Title";
            ViewData["YearSortParm"] = sortOrder == "Year" ? "year_desc" : "Year";

            switch (sortOrder)
            {
                case "Title":
                    searchViewModel.Search = searchViewModel.Search.OrderBy(x => x.Title).Skip(excludeRecords).Take(pageSize).ToList();
                    break;
                case "title_desc":
                    searchViewModel.Search = searchViewModel.Search.OrderByDescending(x => x.Title).Skip(excludeRecords).Take(pageSize).ToList(); 
                    break;
                case "Year":
                    searchViewModel.Search = searchViewModel.Search.OrderBy(x => x.Year).Skip(excludeRecords).Take(pageSize).ToList();
                    break;
                case "year_desc":
                    searchViewModel.Search = searchViewModel.Search.OrderByDescending(x => x.Year).Skip(excludeRecords).Take(pageSize).ToList(); 
                    break;
                default:
                    searchViewModel.Search = searchViewModel.Search.Skip(excludeRecords).Take(pageSize).ToList();
                    break;
            }


            // Returnera en MovieDTO eller annan klass istället för en vymodell?
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
    }
}

