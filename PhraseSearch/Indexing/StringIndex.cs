using System.Collections.Generic;

namespace PhraseSearch.Indexing
{
    public class StringIndex<T> : Index<string, T>
    {
        public StringIndex()
        {
            IndexItems = new List<IndexItem<T>>();
        }
    }
}
