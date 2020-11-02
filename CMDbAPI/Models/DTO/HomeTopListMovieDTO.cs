using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class HomeTopListMovieDTO
    {
        public string ImdbID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }    
        public string Poster { get; set; }  
        public int NumberOfLikes { get; set; }   
        public int NumberOfDislikes { get; set; }
        public List<Ratings> Ratings { get; set; } = new List<Ratings>();
    }


}
