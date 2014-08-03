using System;

namespace PhraseSearch.Scoring
{
    public interface IScorer<in T>
    {
        Single Score(int searchTermPosition, IHitItem<T> searchHit);
    }
}
