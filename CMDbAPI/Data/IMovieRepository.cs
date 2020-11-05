using CMDbAPI.Models;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMDbAPI
{
    public interface IMovieRepository
    {
        /// <summary>
        /// Rate one CMDd movie
        /// </summary>
        /// <param name="imdbId">unique movie id</param>
        /// <param name="rating">Like or dislike</param>
        /// <returns>Latest count for likes or dislikes for current movie</returns>
        Task<Movie> Rate(string imdbId, Rating rating = Rating.Like);
        /// <summary>
        /// Get counts for likes and dislikes
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<Movie> GetMovieRatings(string imdbId);

        /// <summary>
        /// Generates a list of movies sorted by input parameter. 
        /// </summary>
        /// <param name="parameter">Object of type <see cref="Parameter"/></param>
        /// <remarks>Default is null</remarks>
        /// <returns></returns>
        Task<IEnumerable<Movie>> GetToplist(Parameter parameter = null);

        /// <summary>
        /// Generates a list of all rated movies . 
        /// </summary>
        /// <remarks>This operation will take time if there are many movies in the datatabase</remarks>
        Task<IEnumerable<Movie>> GetAllMovieRatings();


        /// <summary>
        /// Hämtar detaljer om filmen ifrån OMDb  
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<MovieDetailsDTO> GetMovieDetails(string imdbId);

        /// <summary>
        /// Hämtar detaljer om filmen ifrån OMDb till filmer som visas i en topplista  
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<HomeTopListMovieDTO> GetTopListMovieDetails(string imdbId);


        /// <summary>
        /// Hämtar information från både OMDb och CMDb till en movieDetailsViewModel
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<MovieDetailsViewModel> GetSummarySingleMovie(string imdbId);

        /// <summary>
        /// Hämtar data ifrån både CMDB (likes, dislikes, imdbID) och använder sedan imdbID för att hämta mer information i OMDB.
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        Task<List<HomeTopListMovieDTO>> GetTopListAggregatedData(Parameter parameter);


        /// <summary>
        /// Hämtar sökningar som matchar 'searchString'-parametern.
        /// OMDB-api returnerar endast 10 filmer i taget även fast sökträffen har fler filmer än så. Därför kan 'pageNumber' användas för att hämta kommande tio filmer.
        /// Går att filtrera sökträffarna att endast söka efter en 'type' som kan vara filmer, serier eller spel. Är 'type' null så hämtas alla typer.
        /// </summary>
        /// <param name="searchString"></param>
        /// <param name="pageNumber"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        Task<SearchViewModel> GetAllCinematicTypesContaining(string searchString, int pageNumber=1, string type=null);

    }
}