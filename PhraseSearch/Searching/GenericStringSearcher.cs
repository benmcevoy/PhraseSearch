using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using PhraseSearch.Indexing;
using PhraseSearch.Scoring;

namespace PhraseSearch.Searching
{
    public class GenericStringSearcher<T> : ISearcher<string, T>
    {
        private readonly IScorer<T> _scorer;
        private readonly IAccumulator<T> _accumulator;

        /// <summary>
        /// Search strings
        /// </summary>
        /// <param name="scorer"></param>
        /// <param name="accumulator"></param>
        public GenericStringSearcher(IScorer<T> scorer, IAccumulator<T> accumulator)
        {
            _scorer = scorer;
            _accumulator = accumulator;
        }

        public IEnumerable<SearchHit<T>> Search(string[] searchTerms, FannedIndex<string, T> index)
        {
            var searchTermPosition = 0;
            var results = new List<SearchHit<T>>(1024);

            foreach (var searchTerm in searchTerms)
            {
                searchTermPosition++;

                var s = searchTerm.ToLowerInvariant();
                var current = index;
                var previous = index;
                var notFound = false;
                var termLength = s.Length;

                for (var i = 0; i < termLength; i++)
                {
                    var key = s[i].ToString(CultureInfo.InvariantCulture);

                    if (!current.Indexer.ContainsKey(key))
                    {
                        notFound = true;
                        break;
                    }

                    previous = current;
                    current = current.Indexer[key];
                }

                results.AddRange(notFound
                    ? SearchImpl(s, searchTermPosition, previous.Items)
                    : SearchImpl(s, searchTermPosition, current.Items));
            }

            return _accumulator
                .Accumulate(results)
                .OrderByDescending(hit => hit.Score);
        }

        public IEnumerable<SearchHit<T>> Search(string[] searchTerms, FlatIndex<string, T> index)
        {
            var searchTermPosition = 0;
            var results = new List<SearchHit<T>>(1024);

            foreach (var searchTerm in searchTerms)
            {
                searchTermPosition++;

                var key = searchTerm.ToLowerInvariant();

                for (var i = key.Length; i > 0; i--)
                {
                    var targetKey = key.Substring(0, i);

                    if (!index.ContainsKey(targetKey)) continue;

                    results.AddRange(SearchImpl(key, searchTermPosition, index[targetKey]));
                    break;
                }
            }

            return _accumulator
                .Accumulate(results)
                .OrderByDescending(hit => hit.Score);
        }

        private IEnumerable<SearchHit<T>> SearchImpl(string searchTerm, int searchTermPosition, IEnumerable<IndexItem<T>> indexItems)
        {
            return indexItems
                .Where(indexItem => indexItem.Term.StartsWith(searchTerm))
                .Select(indexItem => new SearchHit<T>(_scorer.Score(searchTermPosition, indexItem), indexItem));
        }
    }
}
