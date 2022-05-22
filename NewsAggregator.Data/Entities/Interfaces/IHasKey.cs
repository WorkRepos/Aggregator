namespace NewsAggregator.Data.Entities.Interfaces
{
    public interface IHasKey<T>
    {
        T Id { get; set; }
    }
}
