namespace CMDbAPI
{
    public interface IMovie
    {
        string ImdbID { get; set; }
        int NumberOfLikes { get; set; }
        int NumberOfDislikes { get; set; }
    }
}