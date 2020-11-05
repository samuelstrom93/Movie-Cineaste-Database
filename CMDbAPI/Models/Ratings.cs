using System;
namespace CMDbAPI.Models
{
    public class Ratings : IRatings
    {
        public string Source { get; set; }
        public string Value { get; set; }
    }
}
