using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CMDbAPI.Controllers
{
    [Route("api/[controller]")]
    [Route("api/")]

    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieRepository context;
        public MovieController(IMovieRepository repository)
        {
            context = repository;
        }

        [HttpGet]
        // GET: api/
        public async Task<IEnumerable<Movie>> Get()
        {
            return await context.GetAllMovieRatings();
        }

        [HttpGet("{imdbId}")]
        // GET: api/Movie/3
        public async Task<ActionResult<Movie>> MovieRating(string imdbId)
        {
            return await context.GetMovieRatings(imdbId);
        }

        [HttpGet("{imdbId}/like")]
        // GET: api/Movie/3/Like
        public async Task<ActionResult<Movie>> LikeMovie(string imdbId)
        {
            if (!imdbId.IsValidImdbId())
                return BadRequest();

            return await context.Rate(imdbId);
        }

        [HttpGet("{imdbId}/dislike")]
        // GET: api/Movie/3/Dislike
        public async Task<ActionResult<Movie>> DislikeMovie(string imdbId)
        {
            if (!imdbId.IsValidImdbId())
                return BadRequest();

            return await context.Rate(imdbId, Rating.Dislike);
        }

        #region Movie Details

        // Egen metod som hämtar en film från OMDbApi - Körs EJ automatiskt för tillfället
        public async Task<Movie> GetMovieDetails()
        {
            using (HttpClient client = new HttpClient())
            {
                //Använd baseUrl m.m på endpoint...
                string endpoint = "http://www.omdbapi.com/?i=tt0111161&apikey=698a3567";
                var respons = await client.GetAsync(endpoint, HttpCompletionOption.ResponseHeadersRead);
                //TODO: Gör det här till en try/catch för att fånga exceptions
                respons.EnsureSuccessStatusCode();
                var data = await respons.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<Movie>(data);
                return result;
            }
        }
        #endregion

    }
}
