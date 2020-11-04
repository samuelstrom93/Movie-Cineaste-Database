namespace CMDbAPI
{
    public interface IParameter
    {
        string SortOrder { get; set; }
        int? Count { get; set; }
        string Type { get; set; }
    }
}