using System;

namespace ProductCatalogManager.Models
{
    public interface BaseDbModel
    {
    }

    public interface BaseDbModel<out TKey> : BaseDbModel where TKey : IEquatable<TKey>
    {
        public TKey Id { get; }
        public DateTime CreatedAt { get; set; }
    }
}