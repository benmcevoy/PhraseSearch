using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    public interface IIndexer<TKey, TValue>
    {
        Index<TKey, TValue> Index(IEnumerable<TValue> documents);
    }
}
