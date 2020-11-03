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
        //public IEnumerable<SelectListItem> Types { get; set; }
        //public IEnumerable<SelectListItem> SortOrders { get; set; }


        //public IEnumerable<SelectListItem> Counts
        //{
        //    get
        //    {
        //        if (countries != null)
        //        {
        //            return countries.Select(x =>
        //            new SelectListItem()
        //            {
        //                Text = x.Name,
        //                Value = x.Name
        //            });
        //        }
        //        return null;
        //    }
        //}

        public HomeViewModel()
        {

        }


        public HomeViewModel(IParameter parameter)
        {
            SelectedCount = (int)parameter.Count;
            SelectedSortOrder = parameter.SortOrder;
            SelectedType = parameter.Type;
        }
    }
}
