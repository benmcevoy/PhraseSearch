using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    public class Index<TKey, TValue>
    {
        public Index()
            : this("", 32)
        {

        }

        public Index(int capacity)
            : this("", capacity)
        {

        }

        public Index(string name, int capacity)
        {
            Name = name;
            Indexer = new Dictionary<TKey, Index<TKey, TValue>>(capacity);
            Items = new List<IndexItem<TValue>>();
        }

        public Dictionary<TKey, Index<TKey, TValue>> Indexer { get; private set; }

        public List<IndexItem<TValue>> Items { get; private set; }

        public string Name { get; private set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
