using System.Collections.Generic;
using PhraseSearch.Indexing;

namespace PhraseSearch.Searching
{
    public interface ISearcher<TKey, TValue>
    {
        IEnumerable<SearchHit<TValue>> Search(string[] searchTerms, Index<TKey, TValue> index);
    }
}
