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

        public Task<T> GetMovieDetails<T>(string imdbId)
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

        public Task<HomeViewModel> GetSummary(string id)
        {
            throw new NotImplementedException();
        }

        public Task<MovieDetailsViewModel> GetSummarySingleMovie(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<HomeViewModel> GetSummaryViewModel(string id)
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

        public Task<HomeViewModel> GetTopListAggregatedData()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HomeViewModel>> GetTopListAggregatedData(Parameter parameter)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<HomeViewModel>> GetTopListAggregatedDataDefaultValues()
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


        Task<SearchViewModel> IMovieRepository.GetAllMoviesContaining(string searchString)
        {
            throw new NotImplementedException();
        }

       


        Task<HomeViewModel> IMovieRepository.GetTopListAggregatedData(Parameter parameter)
        {
            throw new NotImplementedException();
        }

        //Task<SummaryViewModel> IMovieRepository.GetTopListAggregatedDataDefaultValues()
        //{
        //    throw new NotImplementedException();
        //}

        //Task<IEnumerable<SummaryViewModel>> IMovieRepository.GetTopListAggregatedData()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
