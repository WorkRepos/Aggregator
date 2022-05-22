using NewsAggregator.Data.Entities.Interfaces;

namespace NewsAggregator.Data.Entities
{
    public abstract class BaseEntity<T> : IHasKey<T>
    {
        public T Id { get; set; }
    }
}
