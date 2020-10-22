using CMDbAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMDbAPI.ViewModel
{
    public class SummaryViewModel
    {
        //OMDbApi
        public string Title { get; set; }
        public int Year { get; set; }
        public string Runtime { get; set; }
        public string Genre { get; set; }


        // CMDbApi
        /// <summary>
        /// Number of likes from CMDb Community
        /// </summary>
        public int NumberOfLikes { get; set; }
        /// <summary>
        /// Number of dislikes from CMDb Community
        /// </summary>
        public int NumberOfDislikes { get; set; }

      

        public SummaryViewModel(MovieSummaryDTO movieSummaryDTO)
        {
            
        }
    }
}
