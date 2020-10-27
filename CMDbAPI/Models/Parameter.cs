using System.ComponentModel;

namespace CMDbAPI
{
    /// <summary>
    /// DTO object for passing parameters to repository
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Sorting order 
        /// Desc = descending
        /// Asc = ascending
        /// </summary>
        public string SortOrder { get; set; } = "desc";
        /// <summary>
        /// Number of movies  to recevie
        /// </summary>
        public int? Count { get; set; } = 5;

        /// <summary>
        /// Type of movielist
        /// Ratings = sort by CMDb rating quota (like - dislike = quota)
        /// Popularity = sort by sum of likes and dislikes. Many reactions equals high popularity 
        /// </summary>

        public string Type { get; set; } = "rating";

        public Parameter()
        {

        }

        public Parameter(int count, string sortorder, string type)
        {
            this.Count = count;
            this.SortOrder = sortorder;
            this.Type = type;
        }

    }

   
}
