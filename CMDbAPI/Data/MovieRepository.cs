using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CMDbAPI.Controllers;
using CMDbAPI.Models.DTO;
using CMDbAPI.ViewModel;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Npgsql;

namespace CMDbAPI
{
    public class MovieRepository : IMovieRepository
    {
        #region private fields
        private readonly IConfiguration dbSettings;
        private readonly string connectionString;
        #endregion

        #region Constructor
        public MovieRepository(IConfiguration settings)
        {
            dbSettings = settings;
            connectionString = dbSettings.GetValue<string>("ConnectionString");
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

        // Egen metod som hämtar en film från OMDbApi - Körs EJ automatiskt för tillfället
        public async Task<OmdbDTO> GetMovieDetails(string imdbId)
        {
            using (HttpClient client = new HttpClient())
            {
                //TODO: lägg till baseUrl  config/settings
                //Använd baseUrl m.m på endpoint...

                string endpoint = "http://www.omdbapi.com/?i=" + imdbId + "&apikey=743f5535";
                var respons = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                //TODO: Gör det här till en try/catch för att fånga exceptions
                respons.EnsureSuccessStatusCode();
                var data = await respons.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OmdbDTO>(data);
                return result;
            }
        }

        //TODO: gör funktionen generisk och skicka till en separat folder? Typ Infrastructure/API - Kolla eriks FL och repo
        // Kanske seprarera i olika mappar på OMDb-anrop och CMDb-anrop?
        public async Task<T> GetMovieDetailsGeneric<T>(string imdbID)
        {
            using (HttpClient client = new HttpClient())
            {
                //Använd baseUrl m.m på endpoint...
                string endpoint = "http://www.omdbapi.com/?i=" + imdbID + "&apikey=743f5535";
                var respons = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                //TODO: Gör det här till en try/catch för att fånga exceptions
                respons.EnsureSuccessStatusCode();
                var data = await respons.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<T>(data);
                return result;
            }
        }


        //TODO: Fixa så att man kan skicka in parametrar för att styra hur du hämtar topplistna. Om T.ex. det ska vara en särskild count, type(rating eller popularity),
        // sort (ascending eller descinding). Om fältet lämnas tomt så hämtar den hela topplistan
        /// <summary>
        /// Kunna skicka in parametrar och styra vilken data som du vill använda 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SummaryViewModel>> GetTopListAggregatedData()
        {

            Parameter parameter = new Parameter()
            {
                Count = 10,
                SortOrder = "desc",
                Type = "popularity"
            };

            //Parameter parameter = new Parameter()
            //{
            //    Count = count,
            //    SortOrder = sortOrder,
            //    Type = type
            //};
            var toplist = await GetToplist(parameter);


            //    //    parameter.Count = movies.Count();
            //    //    //parameter.Count = 3; Bestämmer hur många som ska vara i topplistan

            //    //    //parameter.SortOrder = "Asc"; //Lägst först
            //    //    parameter.SortOrder = "Desc";//Högst först (defaultvärde)

            //    //    //parameter.Type = "popularity"; // Sorterar enbart efter hur många som har betygsatt filmen, struntar i hur stor skillnaden är mellan likes & dislikes
            //    //    parameter.Type = "ratings"; // Sorterar efter hur stor skillnaden är mellan likes & dislikes (defaultvärde)


            //var toplist = await GetToplist();

            List<SummaryViewModel> summaryViewModels = new List<SummaryViewModel>();

            foreach (var movie in toplist)
            {
                OmdbDTO omdbDTO = await GetMovieDetails(movie.ImdbID);
                SummaryViewModel summaryViewModel = new SummaryViewModel(omdbDTO, movie); //movie och moviedetailsDTO som parametrar
                summaryViewModels.Add(summaryViewModel);
            }
            return summaryViewModels;
        }


        //TODO: Fixa så att man kan skicka in parametrar för att styra hur du hämtar topplistna. Om T.ex. det ska vara en särskild count, type(rating eller popularity),
        // sort (ascending eller descinding). Om fältet lämnas tomt så hämtar den hela topplistan
        /// <summary>
        /// Kunna skicka in parametrar och styra vilken data som du vill använda 
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SummaryViewModel>> GetTopListAggregatedData(Parameter parameter=null)
        {            
            var toplist = await GetToplist(parameter);


            List<SummaryViewModel> summaryViewModels = new List<SummaryViewModel>();

            foreach (var movie in toplist)
            {
                OmdbDTO omdbDTO = await GetMovieDetails(movie.ImdbID);
                SummaryViewModel summaryViewModel = new SummaryViewModel(omdbDTO, movie); //movie och moviedetailsDTO som parametrar
                summaryViewModels.Add(summaryViewModel);
            }
            return summaryViewModels;
        }



        public async Task<SummaryViewModel> GetSummarySingleMovie(string imdbId)
        {
            var movie = await GetMovieDetails(imdbId);
            var ratings = await GetMovieRatings(imdbId);
            SummaryViewModel summaryViewModel = new SummaryViewModel(movie, ratings);

            return summaryViewModel;
        }

        #endregion

    }
    #endregion







    #endregion
}
