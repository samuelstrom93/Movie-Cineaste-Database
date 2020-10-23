using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class MovieSummaryDTO
    {


        //TODO: ta bort MovieSummary eftersom vi lägger allting i SummaryViewModel istället?
        //public List<MovieDetailsDTO> movieDetailsDTOs { get; set; }

        //public List<Movie> movies { get; set; }




        public string Title { get; set; }

        // Gjorde om till string
        // Rasmus på luffen hade Year: som "Year": "1986–", så var tvungen att formatera om till year
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public string Poster { get; set; }
        public string ImdbID { get; set; }


        // CMDB
        public int NumberOfLikes { get; set; }
        public int NumberOfDislikes { get; set; }


        public MovieSummaryDTO(Movie movie, MovieDetailsDTO movieDetailsDTO)
        {
            this.ImdbID = movie.ImdbID;
            this.NumberOfDislikes = movie.NumberOfDislikes;
            this.NumberOfLikes = movie.NumberOfLikes;

            this.Title = movieDetailsDTO.Title;
            this.Year = movieDetailsDTO.Year;
            this.Runtime = movieDetailsDTO.Runtime;
            this.Genre = movieDetailsDTO.Genre;
            this.Actors = movieDetailsDTO.Actors;
            this.Poster = movieDetailsDTO.Poster;

        }



    }
}
