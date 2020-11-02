using CMDbAPI.Models;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
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
        /// 
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
        /// Hämtar information från både OMDb och CMDb till en summaryViewModel
        /// </summary>
        /// <param name="imdbId"></param>
        /// <returns></returns>
        Task<MovieDetailsViewModel> GetSummarySingleMovie(string imdbId);
        //Task<SummaryViewModel> GetTopListAggregatedDataDefaultValues();
        Task<HomeViewModel> GetTopListAggregatedData(Parameter parameter);
        // Task<IEnumerable<SummaryViewModel>> GetTopListAggregatedData(int count=5, string sortorder="asc", string type="ratings");


        Task<SearchViewModel> GetAllMoviesContaining(string searchString);

    }
}