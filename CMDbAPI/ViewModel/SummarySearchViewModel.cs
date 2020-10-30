using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class SummarySearchViewModel
    {
        public string Title { get; set; }

        public string Year { get; set; }

        public string Poster { get; set; }

        public string ImdbID { get; set; }

        public string Type { get; set; }



        public SummarySearchViewModel(OmdbDTO omdbDTO)
        {
            Title = omdbDTO.Title;
            Poster = omdbDTO.Poster;
            ImdbID = omdbDTO.imdbID;
            Year = omdbDTO.Year;
            Type = omdbDTO.Type;

            if (string.IsNullOrEmpty(omdbDTO.Poster) || omdbDTO.Poster.Contains("N/A"))
            {
                Poster = "/img/NoPosterAvaible.png";
            }

        }
    }
}
