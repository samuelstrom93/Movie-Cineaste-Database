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
        public async Task<MovieDetailsDTO> GetMovieDetails(string imdbId)
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
                var result = JsonConvert.DeserializeObject<MovieDetailsDTO>(data);
                return result;
            }
        }


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


        //private MovieDetailsDTO movieDetailsDTO;
        //private Movie movie;



        public async Task<SummaryViewModel> GetTopList()
        {

            //EN LÖSNING
            //Parameter parameter = new Parameter()
            //{
            //    Count = 10,
            //};

            //var toplist = await GetToplist(parameter);
            //List<MovieDetailsDTO> movieDetailsDTOs = new List<MovieDetailsDTO>();

            //foreach (var entry in toplist)
            //{
            //    MovieDetailsDTO movieDetailsDTO = await GetMovieDetails(entry.ImdbID);
                
            //    movieDetailsDTOs.Add(movieDetailsDTO);
            //}

            //SummaryViewModel summaryViewModel = new SummaryViewModel(movieDetailsDTOs, toplist);


            //return summaryViewModel;



            //TODO: behöver komprimeras
            Parameter parameter = new Parameter()
            {
                Count = 10,
            };

            var toplist = await GetToplist(parameter);
            //List<movieSummaryDTOs = new List<MovieSummaryDTO>();

            SummaryViewModel summaryViewModel = new SummaryViewModel();

            foreach (var entry in toplist)
            {
                MovieDetailsDTO movieDetailsDTO = await GetMovieDetails(entry.ImdbID);
                MovieSummaryDTO movieSummaryDTO = new MovieSummaryDTO(entry, movieDetailsDTO);
                // Bör kunna abstrahera bort och inte använda punktnotation för att komma åt
                summaryViewModel.movieSummaryDTOs.Add(movieSummaryDTO);
            }

            return summaryViewModel;




            //TODO
            /*********
             *LÖSNING TRE - EJ FÄRDIG
            * ********/

            //Parameter parameter = new Parameter()
            //{
            //    Count = 10,
            //};
            //var toplist = await GetToplist(parameter);

            //// FÖRST movies (toplist)
            //// Sedan MovieSummaryDTO

            //MovieSummaryDTO movieSummaryDTO = new MovieSummaryDTO();

            //List<SummaryViewModel> summaryViewModels = new List<SummaryViewModel>();
            //foreach (var entry in toplist)
            //{
            //    var summary = await GetMovieDetailsGeneric<MovieDetailsDTO>(entry.ImdbID);

            //    movieSummaryDTO.movieDetailsDTOs.Add(summary);

            //}
            // MovieDetailsDTO

            //List<MovieDetailsDTO> movieDetailsDTOs = new List<MovieDetailsDTO>();



            //SummaryViewModel summaryViewModel = new SummaryViewModel(movieDetailsDTOs, toplist);



            //return null;




        }




        public async Task<MovieDetailsDTO> GetSummarySingleMovie(string imdbId)
        {

            var movie = await GetMovieDetails(imdbId);
            var ratings = await GetMovieRatings(imdbId);

            //MovieDetailsDTO

            //SummaryViewModel summaryViewModel = new SummaryViewModel(movie, ratings);

            return null;
        }




        // TODO: Förstår inte riktigt hur Erik använder sig av en liknande i HomeController för att komma åt datan
        //public async Task<SummaryViewModel> GetSummaryViewModel(string imdb = null)
        //{
        //    // Jag vill returnera en lista som vi använder i index.cshtml - en IEnumerable. Då kan jag göra foreach i toppfilmerna 



        //    /******************
        //     * FUNKAR
        //    //var movie = await GetMovieDetails(imdb);
        //    //var ratings = await GetMovieRatings(imdb);

        //    //SummaryViewModel summaryViewModel = new SummaryViewModel(movie, ratings);

        //    //return summaryViewModel;
        //    *******/








        //    //var model = await movieRepository.GetSummary("tt3659388");



        //    //var movies = await movieRepository.GetAllMovieRatings();


        //    //Parameter parameter = new Parameter();
        //    //{
        //    //    parameter.Count = movies.Count();
        //    //    //parameter.Count = 3; Bestämmer hur många som ska vara i topplistan

        //    //    //parameter.SortOrder = "Asc"; //Lägst först
        //    //    parameter.SortOrder = "Desc";//Högst först (defaultvärde)

        //    //    //parameter.Type = "popularity"; // Sorterar enbart efter hur många som har betygsatt filmen, struntar i hur stor skillnaden är mellan likes & dislikes
        //    //    parameter.Type = "ratings"; // Sorterar efter hur stor skillnaden är mellan likes & dislikes (defaultvärde)
        //    //}
        //    //var toplist = await movieRepository.GetToplist(parameter);


        //    return null;
            
        //}

        




        #endregion

    }
    #endregion







    #endregion
}
