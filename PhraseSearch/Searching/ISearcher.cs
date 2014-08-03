using System.Collections.Generic;
using PhraseSearch.Indexing;

namespace PhraseSearch.Searching
{
    public interface ISearcher<TKey, TValue>
    {
        IEnumerable<SearchHit<TValue>> Search(string[] searchTerms, FannedIndex<TKey, TValue> index);

        IEnumerable<SearchHit<TValue>> Search(string[] searchTerms, FlatIndex<TKey, TValue> index);
    }
}
