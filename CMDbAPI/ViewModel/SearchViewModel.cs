using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class SearchViewModel
    {
        public List<SearchMovieDTO> Search { get; set; }
    }
}
