using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class SummaryViewModel
    {

        //OMDbApi
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }

        public string Actors { get; set; }
        public string Poster { get; set; }


        // CMDbApi
        /// <summary>
        /// Number of likes from CMDb Community
        /// </summary>
        public int NumberOfLikes { get; set; }
        /// <summary>
        /// Number of dislikes from CMDb Community
        /// </summary>
        public int NumberOfDislikes { get; set; }

        public string ImdbID { get; set; }



        public SummaryViewModel(MovieDetailsDTO movieDetailsDTO, Movie movie)
        {
            this.Title = movieDetailsDTO.Title;
            this.Year = movieDetailsDTO.Year;
            this.Runtime = movieDetailsDTO.Runtime;
            this.Genre = movieDetailsDTO.Genre;
            this.Actors = movieDetailsDTO.Actors;
            this.Poster = movieDetailsDTO.Poster;

            this.NumberOfLikes = movie.NumberOfLikes;
            this.NumberOfDislikes = movie.NumberOfDislikes;
            this.ImdbID = movie.ImdbID;
        }
    }
}
