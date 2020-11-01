﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class OmdbDTO
    {


        //TODO: Döp om till OMDbMovie?
        public string Title { get; set; }

        public string Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }
        public string Actors { get; set; }
        public string Poster { get; set; }
        public string Plot { get; set; }



        public List<Ratings> Ratings { get; set; }

        //TODO: imdbID finns i både OMDbDTO- och Movie-klasserna. Strukturera upp bättre
        public string imdbID { get; set; }
        public string Type { get; set; }


    }
}
