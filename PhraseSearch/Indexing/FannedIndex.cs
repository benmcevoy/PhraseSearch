using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    /// <summary>
    /// Represents a fanned index structure.
    /// </summary>
    /// <remarks>
    /// So... as it turns out
    /// 
    /// 1) fanning an in-memory structure is kinda pointless, unless there is some cost to the instantiation of the items in
    /// each dictionary "bucket"
    /// 2) a more efficient and a lot simpler implementation would be a "MultiDictionary" or "MultiHashMap"
    /// 3) even better would be a MultiKeyDictionary...  where a, ap, app all mapped to app... but that seems unpossible:)
    /// 
    /// is it possible to have multi key with some kind of freaky GetHash()?
    /// </remarks>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public class FannedIndex<TKey, TValue>
    {
        public FannedIndex()
            : this("", 32)
        {

        }

        public FannedIndex(int capacity)
            : this("", capacity)
        {

        }

        public FannedIndex(string name, int capacity)
        {
            Name = name;
            Indexer = new Dictionary<TKey, FannedIndex<TKey, TValue>>(capacity);
            Items = new List<IndexItem<TValue>>();
        }

        public Dictionary<TKey, FannedIndex<TKey, TValue>> Indexer { get; private set; }

        public List<IndexItem<TValue>> Items { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
