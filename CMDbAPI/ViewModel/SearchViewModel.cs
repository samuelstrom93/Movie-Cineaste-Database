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
        
    }
}
