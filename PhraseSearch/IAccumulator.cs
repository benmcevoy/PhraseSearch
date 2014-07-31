using System.Collections.Generic;
using PhraseSearch.Searching;

namespace PhraseSearch
{
    public interface IAccumulator<T>
    {
        IEnumerable<SearchHit<T>> Accumulate(IEnumerable<SearchHit<T>> hits);
    }
}
