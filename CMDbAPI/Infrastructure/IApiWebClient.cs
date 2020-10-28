using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Infrastructure
{
    public interface IApiWebClient
    {
        Task<T> GetAsync<T>(string searchString);
    }
}
