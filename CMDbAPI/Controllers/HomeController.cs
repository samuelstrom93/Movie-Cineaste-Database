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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
            items.Add(new SelectListItem { Text = "Descending", Value = "desc" });
            items.Add(new SelectListItem { Text = "Ascending", Value = "asc" });

            toplist.SortOrders = itemsSort;






            //TODO: flytta validering till modellen ist
            //foreach (var item in toplist.TopListMovies)
            //{
            //    if (string.IsNullOrEmpty(item.Poster) || item.Poster.Contains("N/A"))
            //    {
            //        item.Poster = "/img/NoPosterAvaible.png";
            //    }

            //    if (string.IsNullOrEmpty(item.Plot) || item.Plot.Contains("N/A"))
            //    {
            //        item.Plot = "No plot available";
            //    }
            //}



            return View(toplist);
        }

        [HttpGet("Home/Filter")]
        public async Task<IActionResult> Filter(string selectedCount)
        {
            //var toplist = await movieRepository.GetTopListAggregatedData(parameter);

            HomeViewModel toplist = new HomeViewModel();
            Parameter parameter = new Parameter();

            List<SelectListItem> items = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Värde 1", Value = "1", Selected = true });
            items.Add(new SelectListItem { Text = "Värde 1", Value = "1" });
            items.Add(new SelectListItem { Text = "Värde 5", Value = "5" });
            items.Add(new SelectListItem { Text = "Värde 10", Value = "10" });
            items.Add(new SelectListItem { Text = "Värde 20", Value = "20" });
            //items.Add(new SelectListItem { Value = "3" });


            //List<SelectListItem> itemsSort = new List<SelectListItem>();
            //items.Add(new SelectListItem { Text = "Descending", Value = "desc" });
            //items.Add(new SelectListItem { Text = "Ascending", Value = "asc" });


            parameter.Count = int.Parse(selectedCount);

            toplist = await movieRepository.GetTopListAggregatedData(parameter);
            toplist.Counts = items;
            //toplist.SortOrders = itemsSort;


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
