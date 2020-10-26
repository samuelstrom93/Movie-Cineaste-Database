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



        //private IMovieRepository movieRepository;
        //public async Task SummaryViewModel(string imdbID)
        //{
        //    SummaryViewModel summaryViewModel = await movieRepository.GetSummarySingleMovie(imdbID);
        //}

        public SummaryViewModel(OmdbDTO movieDetailsDTO, Movie movie)
        {
            this.Title = movieDetailsDTO.Title;
            this.Year = movieDetailsDTO.Year;
            this.Runtime = movieDetailsDTO.Runtime;
            this.Genre = movieDetailsDTO.Genre;
            this.Actors = movieDetailsDTO.Actors;
            this.Poster = movieDetailsDTO.Poster;
            this.Plot = movieDetailsDTO.Plot;

            foreach (var ratings in movieDetailsDTO.Ratings)
            {
                Ratings.Add(ratings);
            }

            this.NumberOfLikes = movie.NumberOfLikes;
            this.NumberOfDislikes = movie.NumberOfDislikes;
            this.ImdbID = movie.ImdbID;
        }
    }
}
