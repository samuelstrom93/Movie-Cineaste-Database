﻿using CMDbAPI.Models;
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

        public Parameter Parameter { get; set; } = new Parameter();

        // Listor för att filtera topplistan
        public IEnumerable<SelectListItem> Counts { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> SortOrders { get; set; }

        public HomeViewModel(IParameter parameter)
        {
            List<SelectListItem> itemsCount = new List<SelectListItem>
            {
                new SelectListItem { Text = "5", Value = "5" },
                new SelectListItem { Text = "10", Value = "10" },
                new SelectListItem { Text = "20", Value = "20" },
                new SelectListItem { Text = "50", Value = "50" },
                new SelectListItem { Text = "All", Value = "0" },
            };
            Counts = itemsCount;

            List<SelectListItem> itemsSort = new List<SelectListItem>
            {
                new SelectListItem { Text = "Descending", Value = "desc" },
                new SelectListItem { Text = "Ascending", Value = "asc" },
            };
            SortOrders = itemsSort;


            List<SelectListItem> itemsType = new List<SelectListItem>
            {
                new SelectListItem { Text = "By popularity", Value = "popularity" },
                new SelectListItem { Text = "By rating", Value = "rating" },
            };
            Types = itemsType;

            Parameter.Count = (int)parameter.Count;
            Parameter.SortOrder = parameter.SortOrder;
            Parameter.Type = parameter.Type;
        }
    }
}
