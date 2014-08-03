using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    /// <summary>
    /// FlatIndex is a very rough kind of MultiDictionary, without the semantics.
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class FlatIndex<TKey, TValue> : Dictionary<TKey, List<IndexItem<TValue>>>
    {
        public FlatIndex() { }

        public FlatIndex(int capacity)
            : base(capacity)
        {

        }
    }
}
