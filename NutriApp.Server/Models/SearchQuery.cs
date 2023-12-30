namespace NutriApp.Server.Models
{
    public class SearchQuery
    {
        public required string SearchPhrase { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}