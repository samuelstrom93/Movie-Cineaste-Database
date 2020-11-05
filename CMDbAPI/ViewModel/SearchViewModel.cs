using CMDbAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class SearchViewModel
    {
        public List<SearchMovieDTO> Search { get; set; }

        public int totalResults { get; set; }

        public List<SelectListItem> PageNumbers { get; set; }


        public SearchViewModel()
        {
            List<SelectListItem> pages = new List<SelectListItem>
            {
                new SelectListItem { Text = "Värde 5", Value = "5" },
                new SelectListItem { Text = "Värde 10", Value = "10" },
                new SelectListItem { Text = "Värde 20", Value = "20" },
                new SelectListItem { Text = "Värde 50", Value = "50" },
                new SelectListItem { Text = "Alla", Value = "0" },
            };
            PageNumbers = pages;
        }
    }
}
