namespace PhraseSearch.Indexing
{
    public class IndexItem<T> : IHitItem<T>
    {
        public IndexItem(T document, string term, int termPosition)
        {
            Document = document;
            Term = term.ToLower();
            TermPosition = termPosition;
        }

        public string Term { get; private set; }

        public int TermPosition { get; private set; }

        public T Document { get; private set; }
    }
}
