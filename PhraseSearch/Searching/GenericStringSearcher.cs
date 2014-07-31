using System;
using System.Collections.Generic;
using System.Linq;
using PhraseSearch.Indexing;
using PhraseSearch.Scoring;

namespace PhraseSearch.Searching
{
    public class GenericStringSearcher<T> : ISearcher<string, T>
    {
        private readonly int _depth;
        private readonly Func<string, string, bool> _matchPredicate;
        private readonly IScorer<T> _scorer;
        private readonly IAccumulator<T> _accumulator;

        /// <summary>
        /// Search strings
        /// </summary>
        /// <param name="depth"></param>
        /// <param name="matchPredicate">A predicate to compare two terms</param>
        /// <param name="scorer"></param>
        /// <param name="accumulator"></param>
        public GenericStringSearcher(int depth, Func<string, string, bool> matchPredicate, IScorer<T> scorer, IAccumulator<T> accumulator)
        {
            _depth = depth;
            _matchPredicate = matchPredicate;
            _scorer = scorer;
            _accumulator = accumulator;
        }

        public IEnumerable<SearchHit<T>> Search(string[] searchTerms, Index<string, T> index)
        {
            var searchTermPosition = 0;
            var results = new List<SearchHit<T>>();

            foreach (var searchTerm in searchTerms)
            {
                searchTermPosition++;

                if (searchTerm.Length < _depth) continue;

                var s = searchTerm.ToLowerInvariant();
                var key = s.Substring(0, _depth);

                if (!index.ContainsKey(key)) continue;

                results.AddRange(SearchImpl(s, searchTermPosition, index[key].IndexItems));
            }

            return _accumulator.Accumulate(results)
                .OrderByDescending(hit => hit.Score);
        }

        private IEnumerable<SearchHit<T>> SearchImpl(string searchTerm, int searchTermPosition, IEnumerable<IndexItem<T>> indexItems)
        {
            return indexItems
                .Where(indexItem => _matchPredicate(indexItem.Term, searchTerm))
                .Select(indexItem => new SearchHit<T>(_scorer.Score(searchTermPosition, indexItem), indexItem));
        }
    }
}
