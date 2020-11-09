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
        public List<SearchMovieDTO> Search { get; set; } = new List<SearchMovieDTO>();

        public string SelectedType { get; set; }
        public string SearchString { get; set; }

        public int totalResults { get; set; }
        public int PageIndex { get; set; } 
        public int TotalPages { get; set; }

        public List<SelectListItem> PageList = new List<SelectListItem>();

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }

        private List<SelectListItem> types;

        public IEnumerable<SelectListItem> Types
        {
            get
            {
                if (types != null)
                {
                    return types.Select(x =>
                    new SelectListItem()
                    {
                        Text = x.Text,
                        Value = x.Value
                    });
                }
                return null;
            }
        }


        public SearchViewModel()
        {
            types = new List<SelectListItem>
            {
                new SelectListItem { Value = null, Text = "All"},
                new SelectListItem { Value = "movie", Text = "Movie" },
                new SelectListItem { Value = "series", Text = "Series" },
                new SelectListItem { Value = "game", Text = "Game" },
            };


            this.types = types
               .Select(t => new SelectListItem
               {
                   Text = t.Text,
                   Value = t.Value
               }).ToList();
        }


    }
}
