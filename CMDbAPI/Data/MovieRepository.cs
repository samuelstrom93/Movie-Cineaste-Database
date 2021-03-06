﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CMDbAPI.Controllers;
using CMDbAPI.Infrastructure;
using CMDbAPI.Models;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using Npgsql;

namespace CMDbAPI
{
    public class MovieRepository : IMovieRepository
    {
        #region private fields
        private readonly IConfiguration dbSettings;
        private readonly string connectionString;
        private readonly string baseUrl, accessKey;
        IApiWebClient apiWebClient;

        #endregion

        #region Constructor
        public MovieRepository(IConfiguration settings, IApiWebClient apiWebClient)
        {
            dbSettings = settings;
            connectionString = dbSettings.GetValue<string>("ConnectionString");
            baseUrl = settings.GetValue<string>("OMDbApi:BaseUrl");
            accessKey = settings.GetValue<string>("OMDbApi:AccessKey");
            this.apiWebClient = apiWebClient;
        }
        #endregion

        #region Public Methods


        #region Like or dislike
        public async Task<Movie> Rate(string imdbId, Rating rating = Rating.Like)
        {
            string sql = "INSERT INTO rating(liked, imdbid) VALUES(@like, @id)";
            StringBuilder stmt = new StringBuilder();
            stmt.AppendLine("(SELECT COUNT(*) ");
            stmt.AppendLine("FILTER (WHERE liked) AS like_count,");
            stmt.AppendLine("COUNT(*) FILTER (WHERE not liked) AS dislike_count ");
            stmt.AppendLine("FROM rating ");
            stmt.AppendLine("WHERE imdbid = @id)");


            //string stmt = "INSERT INTO rating(liked, imdbid) VALUES(@like, @id) returning (select count(*) + 1 as total from rating where liked = @like and imdbid = @id)";
            bool like = (int)rating == 0;
            Movie movie = null;
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (NpgsqlTransaction transaction = conn.BeginTransaction())
                    {
                        using (var command = new NpgsqlCommand(sql, conn))
                        {
                            command.Parameters.AddWithValue("id", imdbId);
                            command.Parameters.AddWithValue("like", like);
                            await command.ExecuteScalarAsync();
                        }
                        using (var command = new NpgsqlCommand(stmt.ToString(), conn))
                        {
                            command.Parameters.AddWithValue("id", imdbId);
                            using (var reader = await command.ExecuteReaderAsync())
                            {
                                while (reader.Read())
                                {
                                    var likes = (int)(long)reader["like_count"];
                                    var dislikes = (int)(long)reader["dislike_count"];
                                    if (likes == 0 && dislikes == 0)
                                        break;
                                    movie = new Movie { ImdbID = imdbId, NumberOfLikes = likes, NumberOfDislikes = dislikes };
                                }
                            }

                            // Commit changes
                            transaction.Commit();
                        }
                    }
                }
            }
            catch (PostgresException)
            {
                throw;
            }
            return movie;
        }
        #endregion

        #region Movie Ratings
        public async Task<Movie> GetMovieRatings(string imdbId)
        {
            StringBuilder stmt = new StringBuilder("SELECT COUNT(*) ");
            stmt.Append("FILTER (WHERE liked) AS like_count,");
            stmt.Append("COUNT(*) FILTER (WHERE not liked) AS dislike_count ");
            stmt.Append("FROM rating ");
            stmt.Append("WHERE imdbid = @id");
            Movie movie = null;
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand(stmt.ToString(), conn))
                    {
                        command.Parameters.AddWithValue("id", imdbId);
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var likes = (int)(long)reader["like_count"];
                                var dislikes = (int)(long)reader["dislike_count"];
                                if (likes == 0 && dislikes == 0)
                                    break;
                                movie = new Movie { ImdbID = imdbId, NumberOfLikes = likes, NumberOfDislikes = dislikes };
                            }
                        }
                    }
                }
            }
            catch (PostgresException)
            {
                throw;
            }
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetAllMovieRatings()
        {
            List<Movie> movies = new List<Movie>();
            StringBuilder stmt = new StringBuilder();
            stmt.Append("SELECT imdbid, ");
            stmt.Append("COUNT(*) ");
            stmt.Append("FILTER (WHERE liked) AS like_count,");
            stmt.Append("COUNT(*) FILTER (WHERE not liked) AS dislike_count ");
            stmt.Append("FROM rating ");
            stmt.Append("group by imdbid");
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand(stmt.ToString(), conn))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                var likes = (int)(long)reader["like_count"];
                                var dislikes = (int)(long)reader["dislike_count"];
                                var imdbid = (string)reader["imdbid"];
                                if (likes == 0 && dislikes == 0)
                                    break;
                                Movie movie = new Movie { ImdbID = imdbid, NumberOfLikes = likes, NumberOfDislikes = dislikes };
                                movies.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (PostgresException)
            {
                throw;
            }
            return movies;
        }
        #endregion

        #region Top lists
        public async Task<IEnumerable<Movie>> GetToplist(Parameter parameter = null)
        {
            StringBuilder stmt = new StringBuilder();
            stmt.AppendLine("select imdbid, like_count,dislike_count, like_count");
            stmt.Append(parameter.Type == "popularity" ? "+" : "-");
            stmt.AppendLine("dislike_count as diff ");
            stmt.AppendLine("from ( select imdbid, ");
            stmt.AppendLine("count(*) filter (where liked) as like_count, ");
            stmt.AppendLine("count(*) filter (where not liked) as dislike_count ");
            stmt.AppendLine("from rating group by imdbid ");
            stmt.AppendLine(") temp ");
            stmt.AppendLine("order by diff ");
            stmt.Append(parameter.SortOrder ?? "desc ");
            if (parameter.Count > 0)
            {
                stmt.AppendLine(" limit");
                stmt.Append(parameter.Count.ToString());
            }

            List<Movie> movies = new List<Movie>();
            Movie movie;
            try
            {
                using (var conn = new NpgsqlConnection(connectionString))
                {
                    conn.Open();
                    using (var command = new NpgsqlCommand(stmt.ToString(), conn))
                    {
                        using (var reader = await command.ExecuteReaderAsync())
                        {
                            while (reader.Read())
                            {
                                string imdbId = (string)reader["imdbid"];
                                int likes = (int)(long)reader["like_count"];
                                int dislikes = (int)(long)reader["dislike_count"];
                                if (likes == 0 && dislikes == 0)
                                    break;
                                movie = new Movie { ImdbID = imdbId, NumberOfLikes = likes, NumberOfDislikes = dislikes };
                                movies.Add(movie);
                            }
                        }
                    }
                }
            }
            catch (PostgresException)
            {
                throw;
            }
            return movies;
        }

        #region Movie Details   


        // Summary finns för nedanstående metoder i IMovieRepository. Håll över metodnamnet för mer en beskrivning.

        public async Task<MovieDetailsDTO> GetMovieDetails(string imdbId)
        {
            string urlString = baseUrl + "i=" + imdbId + "&plot=full" + accessKey;
            return await apiWebClient.GetAsync<MovieDetailsDTO>(urlString);
        }

        public async Task<HomeTopListMovieDTO> GetTopListMovieDetails(string imdbId)
        {
            string urlString = baseUrl + "i=" + imdbId + accessKey;
            return await apiWebClient.GetAsync<HomeTopListMovieDTO>(urlString);
        }



        public async Task<List<HomeTopListMovieDTO>> GetTopListAggregatedData(Parameter parameter)
        {
            List<Task<HomeTopListMovieDTO>> tasks = new List<Task<HomeTopListMovieDTO>>();
            var toplist = await GetToplist(parameter);

            foreach (var movie in toplist)
            {
                tasks.Add(GetTopListMovieDetails(movie.ImdbID));
            }
            var results = Task.WhenAll(tasks);

            var tempToplist = toplist.ToArray();
            for (int i = 0; i < tasks.Count; i++)
            {
                tasks[i].Result.NumberOfLikes = tempToplist[i].NumberOfLikes;
                tasks[i].Result.NumberOfDislikes = tempToplist[i].NumberOfDislikes;
            }

            List<HomeTopListMovieDTO> toplistMovies = results.Result.ToList();
            return toplistMovies;
        }


        public async Task<MovieDetailsViewModel> GetSummarySingleMovie(string imdbId)
        {
            var ratings = await GetMovieRatings(imdbId);
            var movie = await GetMovieDetails(imdbId);
            MovieDetailsViewModel movieSummaryViewModel = new MovieDetailsViewModel(movie, ratings);

            return movieSummaryViewModel;
        }


        public async Task<SearchViewModel> GetAllCinematicTypesContaining(string searchString, int pageNumber = 1, string type = null)
        {
            string urlString;

            if (type != null)
            {
                urlString = $"{baseUrl}s={searchString}&type={type}&page={pageNumber}{accessKey}";
                return await apiWebClient.GetAsync<SearchViewModel>(urlString);
            }
            urlString = $"{baseUrl}s={searchString}&page={pageNumber}{accessKey}";
            return await apiWebClient.GetAsync<SearchViewModel>(urlString);

        }

        public async Task<List<SearchMovieDTO>> GetResultsFromAllPages(SearchViewModel searchViewModelHelper, string searchString, string selectedType)
        {
            SearchViewModel searchViewModel = new SearchViewModel();
            int searchPageNmbr = 1;
            while (searchViewModelHelper.totalResults > searchViewModel.Search.Count)
            {
                searchViewModelHelper = await GetAllCinematicTypesContaining(searchString, searchPageNmbr, selectedType);

                foreach (var movie in searchViewModelHelper.Search)
                {
                    searchViewModel.Search.Add(movie);
                }
                searchPageNmbr++;
            }

            return searchViewModel.Search;
        }
    }

    #endregion

}
#endregion







#endregion


