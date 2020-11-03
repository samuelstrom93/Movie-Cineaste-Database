using CMDbAPI.Models;
using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class MovieDetailsViewModel
    {
        public string ImdbID { get; set; }
        public string Title { get; set; }
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Type { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }
        public string Director { get; set; }
        public List<Ratings> Ratings { get; set; } = new List<Ratings>();
        public int NumberOfLikes { get; set; }        
        public int NumberOfDislikes { get; set; }


        public MovieDetailsViewModel(MovieDetailsDTO movieSummary, Movie movie)
        {
            Title = movieSummary.Title;
            Year = movieSummary.Year;
            Runtime = movieSummary.Runtime;
            Genre = movieSummary.Genre;
            Actors = movieSummary.Actors;
            Poster = movieSummary.Poster;
            Plot = movieSummary.Plot;
            Director = movieSummary.Director;
            Ratings = movieSummary.Ratings;
            Type = movieSummary.Type;
            ImdbID = movieSummary.ImdbID;


            if (movie != null)
            {
                NumberOfLikes = movie.NumberOfLikes;
                NumberOfDislikes = movie.NumberOfDislikes;

            }
        }
    }


}
