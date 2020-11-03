using CMDbAPI.Models;
using CMDbAPI.Models.DTO;
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

        public HomeViewModel(IParameter parameter)
        {
            SelectedCount =(int)parameter.Count;
            SelectedSortOrder = parameter.SortOrder;
            SelectedType = parameter.Type;
        }
    }
}
