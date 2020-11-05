using System.Collections.Generic;

namespace CMDbAPI.Models.DTO
{
    public interface IMovieDetailsDTO
    {
        string ImdbID { get; set; }
        string Title { get; set; }
        string Year { get; set; }
        string Runtime { get; set; }
        string Genre { get; set; }
        string Actors { get; set; }
        string Poster { get; set; }
        string Plot { get; set; }
        string Director { get; set; }
        List<Ratings> Ratings { get; set; }
    }
}