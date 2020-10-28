using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
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

        public Task<MovieDetailsDTO> GetAllMoviesContaining(string searchString)
        {
            throw new NotImplementedException();
        }

        public Task<OmdbDTO> GetMovieDetails(string id)
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

        public Task<OmdbDTO> GetSummarySingleMovie(string imdbId)
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

        public Task<IEnumerable<SummaryViewModel>> GetTopListAggregatedData(Parameter parameter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<SummaryViewModel>> GetTopListAggregatedDataDefaultValues()
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

        //Task<IEnumerable<SummaryViewModel>> IMovieRepository.GetTopListAggregatedData()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
