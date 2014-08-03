using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    public interface IIndexer<TKey, TValue>
    {
        FannedIndex<TKey, TValue> FannedIndex(IEnumerable<TValue> documents);

        FlatIndex<TKey, TValue> FlatIndex(IEnumerable<TValue> documents);
    }
}
