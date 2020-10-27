using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
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

<<<<<<< HEAD
        public Task<OmdbDTO> GetMovieDetails(string id)
=======
        public Task<MovieDetailsDTO> GetMovieDetails(string id)
>>>>>>> 028ac1b... implementerade alla metoder av IMovieRepository i CMDMockRepository.
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

<<<<<<< HEAD
        public Task<OmdbDTO> GetSummarySingleMovie(string imdbId)
        {
            throw new NotImplementedException();
        }

=======
>>>>>>> 028ac1b... implementerade alla metoder av IMovieRepository i CMDMockRepository.
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
