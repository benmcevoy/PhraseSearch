using System;

namespace PhraseSearch.Scoring
{
    public interface IScorer<T>
    {
        Single Score(int searchTermPosition, IHitItem<T> searchHit);
    }
}
