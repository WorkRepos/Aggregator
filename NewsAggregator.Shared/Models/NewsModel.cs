namespace NewsAggregator.Shared.Models
{
    public class NewsModel
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
