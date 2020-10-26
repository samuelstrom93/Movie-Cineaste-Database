using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class OmdbDTO
    {


        //TODO: Döp om till OMDbMovie?
        public string Title { get; set; }

        // Gjorde om till string
        // Rasmus på luffen hade Year: som "Year": "1986–", så var tvungen att formatera om till year
        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public string Poster { get; set; }
    }
}
