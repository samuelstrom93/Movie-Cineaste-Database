using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class SummaryViewModel
    {


        //private List<Movie> movies;
        public List<MovieSummaryDTO> movieSummaryDTOs = new List<MovieSummaryDTO>();

        ////OMDbApi
        //public string Title { get; set; }
        //public string Year { get; set; }
        //public string Runtime { get; set; }
        //public string Genre { get; set; }

        //public string Actors { get; set; }
        //public string Poster { get; set; }


        //// CMDbApi
        ///// <summary>
        ///// Number of likes from CMDb Community
        ///// </summary>
        //public int NumberOfLikes { get; set; }
        ///// <summary>
        ///// Number of dislikes from CMDb Community
        ///// </summary>
        //public int NumberOfDislikes { get; set; }

        //public string ImdbID { get; set; }





        //TODO: Erik hade properties här för att han visade endast ett resultat åt gången?
        // Vi behöver alltså en lista av movieSummaryDTOS?
        //TODO:
        // Använd OrderBy och ToList() för att kunna ändra hur vi vill visa datan i gränssnittet
        //his.movies = movies
        //        .Select(m => new Movie
        //        {
        //            ImdbID = m.ImdbID,
        //            NumberOfDislikes = m.NumberOfDislikes,
        //            NumberOfLikes = m.NumberOfLikes,
        //        })
        //        .OrderBy(x => x.NumberOfLikes)
        //        .ToList();








        //public SummaryViewModel(MovieDetailsDTO movieDetailsDTO, Movie movie)
        //{
        //    this.Title = movieDetailsDTO.Title;
        //    this.Year = movieDetailsDTO.Year;
        //    this.Runtime = movieDetailsDTO.Runtime;
        //    this.Genre = movieDetailsDTO.Genre;
        //    this.Actors = movieDetailsDTO.Actors;
        //    this.Poster = movieDetailsDTO.Poster;

        //    this.NumberOfLikes = movie.NumberOfLikes;
        //    this.NumberOfDislikes = movie.NumberOfDislikes;
        //    this.ImdbID = movie.ImdbID;
        //}





        //public SummaryViewModel(List<MovieDetailsDTO> movieDetailsDTO, IEnumerable<Movie> movies)
        //{
        //    this.movieDetailsDTOs = movieDetailsDTO
        //        .Select(m => new MovieDetailsDTO
        //        {
        //            Title = m.Title,
        //            Year = m.Year,
        //            Runtime = m.Runtime,
        //            Genre = m.Genre,
        //            Actors = m.Actors,
        //            Poster = m.Poster
        //        }).ToList();

        //    this.movies = movies
        //        .Select(m => new Movie
        //        {
        //            ImdbID = m.ImdbID,
        //            NumberOfDislikes = m.NumberOfDislikes,
        //            NumberOfLikes = m.NumberOfLikes,
        //        })
        //        .OrderBy(x => x.NumberOfLikes)
        //        .ToList();
        //}

    }
}
