using CMDbAPI.Models;
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


        // TODO: skapa en dataannotation för att visa ett default ifall den är null/tom [displayname]
        public string Poster { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }


        public List<Ratings> Ratings { get; set; } = new List<Ratings>();


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

        public string Type { get; set; }




        public SummaryViewModel(OmdbDTO movieDetailsDTO, Movie movie)
        {
            Title = movieDetailsDTO.Title;
            Year = movieDetailsDTO.Year;
            Runtime = movieDetailsDTO.Runtime;
            Genre = movieDetailsDTO.Genre;
            Actors = movieDetailsDTO.Actors;
            Poster = movieDetailsDTO.Poster;
            Plot = movieDetailsDTO.Plot;
            Director = movieDetailsDTO.Director;

            foreach (var ratings in movieDetailsDTO.Ratings)
            {
                Ratings.Add(ratings);
            }

            if (string.IsNullOrEmpty(movieDetailsDTO.Poster) || movieDetailsDTO.Poster.Contains("N/A"))
                {
                Poster = "/img/NoPosterAvaible.png";
            }

            //TODO: Den här if-koden hör hemma i SummarySearchViewModel, den ska inte vara här.
            if (movie != null)
            {
                NumberOfLikes = movie.NumberOfLikes;
                NumberOfDislikes = movie.NumberOfDislikes;
                ImdbID = movie.ImdbID;

            }
        }
     
    }
}
