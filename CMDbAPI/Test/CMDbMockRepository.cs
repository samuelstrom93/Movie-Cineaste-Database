using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMDbAPI.Models;

namespace CMDbAPI.Test
{

    //TODO implementera rätt interface
    public class CMDbMockRepository : IMovieRepository
    {
        public CMDbMockRepository()
        {
        }

        public Task<IEnumerable<Movie>> GetAllMovieRatings()
        {
            throw new NotImplementedException();
        }

        public Task<SearchViewModel> GetAllMoviesContaining(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsDTO> GetMovieDetails(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieRatings(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsViewModel> GetSummarySingleMovie(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetToplist(Parameter parameter = null)
        {
            throw new NotImplementedException();
        }

        public Task<HomeViewModel> GetTopListAggregatedData(Parameter parameter)
        {
            throw new NotImplementedException();
        }

        public Task<HomeTopListMovieDTO> GetTopListMovieDetails(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Rate(string imdbId, Rating rating = Rating.Like)
        {
            throw new NotImplementedException();
        }
    }
}
