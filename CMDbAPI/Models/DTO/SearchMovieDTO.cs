using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class SearchMovieDTO : ISearchMovieDTO
    {
        public string Title { get; set; }
        public string Year { get; set; }
        public string Poster { get; set; }
        public string Type { get; set; }
        public string ImdbID { get; set; }


        public List<Ratings> Ratings { get; set; } = new List<Ratings>();
        public string Director { get; set; }
        public string Genre { get; set; }



    }
}
