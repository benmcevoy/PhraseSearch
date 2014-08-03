namespace PhraseSearch.Scoring
{
    public class GenericScorer<T> : IScorer<T>
    {
        public float Score(int searchTermPosition, IHitItem<T> searchHit)
        {
            return searchHit.Depth * 1f / searchTermPosition * 1f / searchHit.TermPosition;
        }
    }
}
