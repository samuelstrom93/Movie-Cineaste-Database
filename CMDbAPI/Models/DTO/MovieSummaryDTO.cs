﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models.DTO
{
    public class MovieSummaryDTO
    {
        public MovieDetailsDTO MovieDetailsDTO { get; set; }

        public Movie Movie { get; set; }

        public MovieSummaryDTO(MovieDetailsDTO movieDetailsDTO, Movie movie)
        {
            this.MovieDetailsDTO = movieDetailsDTO;
            this.Movie = movie;
        }

       

    }
}
