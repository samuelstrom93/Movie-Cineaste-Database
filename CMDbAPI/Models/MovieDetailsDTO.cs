using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.Models
{
    public class MovieDetailsDTO
    {
        public List<OmdbDTO> Search { get; set; }
        }
}
