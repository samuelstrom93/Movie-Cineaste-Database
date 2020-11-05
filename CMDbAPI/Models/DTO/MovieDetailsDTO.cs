using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class MovieDetailsDTO : IMovieDetailsDTO
    {
        public string ImdbID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public List<Ratings> Ratings { get; set; } = new List<Ratings>();
    }
}
