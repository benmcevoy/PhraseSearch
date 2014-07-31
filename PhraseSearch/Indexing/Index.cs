using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    public abstract class Index<TKey, TValue> : Dictionary<TKey, Index<TKey, TValue>>
    {
        public List<IndexItem<TValue>> IndexItems { get; set; }    
    }
}