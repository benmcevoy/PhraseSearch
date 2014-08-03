namespace PhraseSearch.Searching
{
    public class SearchHit<T> : IHitItem<T>
    {
        public SearchHit(double score, IHitItem<T> hitItem)
        {
            Document = hitItem.Document;
            Score = score;
            Term = hitItem.Term;
            TermPosition = hitItem.TermPosition;
            Depth = hitItem.Depth;
        }

        public T Document { get; private set; }

        public int Depth { get; private set; }

        public string Term { get; private set; }

        public int TermPosition { get; set; }

        public double Score { get; private set; }
    }
}
