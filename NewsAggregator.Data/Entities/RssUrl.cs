namespace NewsAggregator.Data.Entities
{
    public class RssUrl : BaseEntity<int>
    {
        public string Url { get; set; }
        public ICollection<News>? News { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
