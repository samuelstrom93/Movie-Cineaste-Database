using System.Collections.Generic;

namespace CMDbAPI.Models.DTO
{
    public interface ISearchMovieDTO
    {
        string Title { get; set; }
        string Year { get; set; }
        string Poster { get; set; }
        string Type { get; set; }
        string ImdbID { get; set; }
        List<Ratings> Ratings { get; set; }
        string Director { get; set; }
        string Genre { get; set; }
    }
}