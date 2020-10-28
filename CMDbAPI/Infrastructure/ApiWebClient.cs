using CMDbAPI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CMDbAPI.Infrastructure
{
    public class ApiWebClient:IApiWebClient
    {
        public async Task<T> GetAsync<T>(string searchString)
        {
            //TODO: Fixa så att koden inte upprepas
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(searchString, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();              
               var result = JsonConvert.DeserializeObject<T>(data);
               return result;
            }
        }
    }
}
