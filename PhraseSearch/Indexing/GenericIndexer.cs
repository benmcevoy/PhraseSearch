using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace PhraseSearch.Indexing
{
    public class GenericStringIndexer<T> : IIndexer<string, T>
    {
        private readonly string[] _splitOn = { " " };
        private readonly Func<T, string> _termSelector;
        private readonly int _depth;

        public GenericStringIndexer(Func<T, string> phraseSelector, int depth)
        {
            _termSelector = phraseSelector;
            _depth = depth;
        }

        public Index<string, T> Index(IEnumerable<T> documents)
        {
            var index = new StringIndex<T>();
            var sortedTerms = GetSortedTerms(documents);

            foreach (var sortedTerm in sortedTerms)
            {
                var key = sortedTerm.Term.Substring(0, _depth);

                if (index.ContainsKey(key))
                {
                    index[key].IndexItems.Add(sortedTerm);
                    continue;
                }

                index[key] = new StringIndex<T>();
                index[key].IndexItems.Add(sortedTerm);
            }

            return index;
        }

        private IEnumerable<IndexItem<T>> GetSortedTerms(IEnumerable<T> documents)
        {
            var documentArray = documents as T[] ?? documents.ToArray();
            var sortedIndexItems = new List<IndexItem<T>>(documentArray.Count() * 2);

            foreach (var document in documentArray)
            {
                var phrase = _termSelector(document).ToLowerInvariant();
                var terms = phrase.Split(_splitOn, StringSplitOptions.RemoveEmptyEntries);
                var termPosition = 0;

                foreach (var term in terms)
                {
                    if (term.Length < _depth) continue;

                    var cleanTerm = Clean(term);

                    if (cleanTerm.Length < _depth) continue;

                    termPosition++;
                    sortedIndexItems.Add(new IndexItem<T>(document, term, termPosition));
                }
            }

            return sortedIndexItems.OrderBy(item => item.Term);
        }

        private readonly Regex _cleanRegex = new Regex("[^a-zA-Z]");
        private string Clean(string term)
        {
            return _cleanRegex.Replace(term, "");
        }
    }
}
