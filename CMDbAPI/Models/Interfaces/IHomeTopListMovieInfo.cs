using System.Collections.Generic;

namespace CMDbAPI.Models.DTO
{
    public interface IHomeTopListMovieDTO
    {
        string Title { get; set; }
        string Year { get; set; }
        string Poster { get; set; }
        List<Ratings> Ratings { get; set; }
        string Plot { get; set; }
    }
}