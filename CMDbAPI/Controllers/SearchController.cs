using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
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
        /// Har inte hunnit putsa till koden så mycket vi har velat i denna controller. Hade gärna flyttat mycket kod till SearchViewModel istället för ren kod här i controllern.
        /// Hade gärna velat slimma till mycket och få bort onödig kod men har inte hunnit pga deadline.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <param name="currentFilter"></param>
        /// <param name="sortOrder"></param>
        /// <param name="selectedType"></param>
        /// <returns></returns>
        public async Task<IActionResult> Index(string searchString, int? pageNumber, string currentFilter, string sortOrder, string selectedType)
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
            ViewData["SelectedType"] = selectedType;
            searchViewModelHelper = await movieRepository.GetAllCinematicTypesContaining(searchString, (int)pageNumber, selectedType);

            if (!String.IsNullOrEmpty(searchString))
            {
                searchViewModel.Search = await movieRepository.GetResultsFromAllPages(searchViewModelHelper, searchString, selectedType);
            }

            if (searchViewModelHelper.totalResults == 0 || String.IsNullOrEmpty(searchString))
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

