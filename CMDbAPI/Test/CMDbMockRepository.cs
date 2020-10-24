using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;

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

        public Task<MovieDetailsDTO> GetMovieDetails(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieRatings(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<SummaryViewModel> GetSummary(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsDTO> GetSummarySingleMovie(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<SummaryViewModel> GetSummaryViewModel(string id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetToplist(Parameter parameter = null)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetToplist()
        {
            throw new NotImplementedException();
        }

        public Task<SummaryViewModel> GetTopListAggregatedData()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Rate(string imdbId, Rating rating = Rating.Like)
        {
            throw new NotImplementedException();
        }

        Task<SummaryViewModel> IMovieRepository.GetSummarySingleMovie(string imdbId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<SummaryViewModel>> IMovieRepository.GetTopListAggregatedData()
        {
            throw new NotImplementedException();
        }
    }
}
