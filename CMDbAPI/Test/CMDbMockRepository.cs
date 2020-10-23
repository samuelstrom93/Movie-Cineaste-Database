using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace CMDbAPI.Test
{
    public class CMDbMockRepository : IMovieRepository
    {
        string basePath;
        public CMDbMockRepository(IWebHostEnvironment webHostEnvironment)
        {
            basePath = $"{webHostEnvironment.ContentRootPath}\\Test\\Mockdata\\";
        }

      
        public async Task<IEnumerable<Movie>> GetAllMovieRatings()
        {
            string testFile = "movies-CMDb.json";
            var result =  GetTestData<IEnumerable<Movie>>(testFile);
            await Task.Delay(0);
            return result;
        }      

       
        //Använder det riktiga api:t på GetMovieDeails
        public async Task<MovieDetailsDTO> GetMovieDetails(string id)
        {
            using (HttpClient client = new HttpClient())
            {
                //Använd baseUrl m.m på endpoint...
                string endpoint = "http://www.omdbapi.com/?i=tt3896198&apikey=698a3567";
                var respons = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                //TODO: Gör det här till en try/catch för att fånga exceptions
                respons.EnsureSuccessStatusCode();
                var data = await respons.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<MovieDetailsDTO>(data);
                return result;
            }
        }
        public async Task<Movie> GetMovieRatings(string imdbId)
        {
            var testfile = "single_movie_2.json";
            var result = GetTestData<Movie>(testfile);
            await Task.Delay(0);
            return result;
        }

        //public Task<SummaryViewModel> GetSummary(string imdbId)
        public async Task<SummaryViewModel> GetSummary(string id)
        {
            var movie = await GetMovieDetails(id);
            var ratings = await GetMovieRatings(id);

            SummaryViewModel summaryViewModel = new SummaryViewModel(movie, ratings);
            return summaryViewModel;
        }



        public Task<SummaryViewModel> GetSummaryViewModel(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Movie>> GetToplist(Parameter parameter = null)
        {
            var testFile= "toplistRatingsDesc.json";
            if (parameter.Type == "popularity")
            {
                if (parameter.SortOrder == "Asc")
                {
                    testFile = "toplistPopularityAsc.json";
                }
                else
                {
                    testFile = "toplistPopularityDesc.json";
                }
            }
            else if (parameter.Type == "ratings")
            {
                if (parameter.SortOrder == "Asc")
                {
                    testFile = "toplistRatingsAsc.json";
                }
            }

            var result = GetTestData<IEnumerable<Movie>>(testFile);
            await Task.Delay(0);
            return result;

        }

        public Task<Movie> Rate(string imdbId, Rating rating = Rating.Like)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Generisk klass
        /// </summary>
        /// <param name="testFile"></param>
        private T GetTestData<T>(string testFile)
        {
            string path = $"{basePath}{testFile}";
            string data = File.ReadAllText(path);
            var result = JsonConvert.DeserializeObject<T>(data);
            return result;
        }
    }
}
