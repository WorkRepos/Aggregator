namespace NewsAggregator.Data.Entities
{
    public class News : BaseEntity<int>
    {
        public string Title { get; set; }
        public string Link { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime PublicationDate { get; set; }
        public int RssUrlId { get; set; }
        public RssUrl? RssUrl { get; set; }
    }
}
