using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CMDbAPI.Controllers
{
    public class HomeController : Controller
    {
        private IMovieRepository movieRepository;
        private Parameter parameter;
       

        public HomeController(IMovieRepository movieRepository)
        {
            this.movieRepository = movieRepository;

        }

        public async Task<IActionResult> Index()
        {
            parameter = new Parameter();
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Värde 1", Value = "1" });
            items.Add(new SelectListItem { Text = "Värde 5", Value = "5" });
            items.Add(new SelectListItem { Text = "Värde 20", Value = "20" });

            toplist.Counts = items;

            List<SelectListItem> itemsSort = new List<SelectListItem>();
            itemsSort.Add(new SelectListItem { Text = "Descending", Value = "desc" });
            itemsSort.Add(new SelectListItem { Text = "Ascending", Value = "asc" });

            toplist.SortOrders = itemsSort;

            List<SelectListItem> itemsType = new List<SelectListItem>();
            itemsType.Add(new SelectListItem { Text = "By popularity", Value = "popularity" });
            itemsType.Add(new SelectListItem { Text = "By rating-quota", Value = "rating" });

            toplist.Types = itemsType;


            return View(toplist);
        }

        [HttpPost("Home/Filter")]
        public async Task<IActionResult> Filter(string selectedCount, string selectedSortOrder, string selectedType)
        {
            //var toplist = await movieRepository.GetTopListAggregatedData(parameter);

            HomeViewModel toplist = new HomeViewModel();
            Parameter parameter = new Parameter();

            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "Värde 1", Value = "1" });
            items.Add(new SelectListItem { Text = "Värde 5", Value = "5" });
            items.Add(new SelectListItem { Text = "Värde 10", Value = "10" });
            items.Add(new SelectListItem { Text = "Värde 20", Value = "20" });


            List<SelectListItem> itemsSort = new List<SelectListItem>();
            itemsSort.Add(new SelectListItem { Text = "Descending", Value = "desc" });
            itemsSort.Add(new SelectListItem { Text = "Ascending", Value = "asc" });

            List<SelectListItem> itemsType = new List<SelectListItem>();
            itemsType.Add(new SelectListItem { Text = "By popularity", Value = "popularity" });
            itemsType.Add(new SelectListItem { Text = "By rating-quota", Value = "rating" });

            if (selectedCount != null)
            {
                parameter.Count = int.Parse(selectedCount);
            }

            if (selectedSortOrder != null)
            {
                parameter.SortOrder = selectedSortOrder;
            }

            if (selectedType != null)
            {
                parameter.Type = selectedType;
            }

            toplist = await movieRepository.GetTopListAggregatedData(parameter);
            toplist.Counts = items;
            toplist.SortOrders = itemsSort;
            toplist.Types = itemsType;


            //parameter = new Parameter(count, sortOrder, type);

            return View("index", toplist);
        }


        [HttpGet]
        public async Task<IActionResult> FilterTopList(int count, string sortOrder, string type)
        {
            parameter = new Parameter(count, sortOrder, type);
            var toplist = await movieRepository.GetTopListAggregatedData(parameter);
            return View("index", toplist);
        }

    }
}
