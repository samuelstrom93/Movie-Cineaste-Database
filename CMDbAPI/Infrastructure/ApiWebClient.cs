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

        /// <summary>
        /// Generisk metod för att hämta JSON-data via HTTP
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="urlString"></param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string urlString)
        {
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(urlString, HttpCompletionOption.ResponseHeadersRead);
                response.EnsureSuccessStatusCode();
                var data = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
        }
    }
}
