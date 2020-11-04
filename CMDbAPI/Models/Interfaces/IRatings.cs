namespace CMDbAPI.Models
{
    public interface IRatings
    {
        string Source { get; set; }
        string Value { get; set; }
    }
}