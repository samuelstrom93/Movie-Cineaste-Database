using CMDbAPI.Models;
using CMDbAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class HomeViewModel
    {
        public List<HomeTopListMovieDTO> TopListMovies { get; set; } = new List<HomeTopListMovieDTO>();
        public int SelectedCount { get; set; }
        public string SelectedType { get; set; }
        public string SelectedSortOrder { get; set; }


        public IEnumerable<SelectListItem> Counts { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> SortOrders { get; set; }

        public HomeViewModel(IParameter parameter)
        {
            List<SelectListItem> items = new List<SelectListItem>
            {
                new SelectListItem { Text = "Värde 5", Value = "5" },
                new SelectListItem { Text = "Värde 10", Value = "10" },
                new SelectListItem { Text = "Värde 20", Value = "20" },
                new SelectListItem { Text = "Värde 50", Value = "50" },
                new SelectListItem { Text = "Alla", Value = "0" },
            };
            Counts = items;

            List<SelectListItem> itemsSort = new List<SelectListItem>
            {
                new SelectListItem { Text = "Descending", Value = "desc" },
                new SelectListItem { Text = "Ascending", Value = "asc" },
            };
            SortOrders = itemsSort;


            List<SelectListItem> itemsType = new List<SelectListItem>
            {
                new SelectListItem { Text = "By popularity", Value = "popularity" },
                new SelectListItem { Text = "By rating-quota", Value = "rating" },
            };
            Types = itemsType;

            SelectedCount = (int)parameter.Count;
            SelectedSortOrder = parameter.SortOrder;
            SelectedType = parameter.Type;
        }
    }
}
