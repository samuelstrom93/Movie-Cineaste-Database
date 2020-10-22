using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CMDbAPI.Test
{
    public class CMDbMockRepository : IMovieRepository
    {
        public CMDbMockRepository()
        {
        }

        public Task<IEnumerable<Movie>> GetAllMovieRatings()
        {
            throw new NotImplementedException();
        }

        public Task<Movie> GetMovieRatings(string imdbId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Movie>> GetToplist(Parameter parameter = null)
        {
            throw new NotImplementedException();
        }

        public Task<Movie> Rate(string imdbId, Rating rating = Rating.Like)
        {
            throw new NotImplementedException();
        }
    }
}
