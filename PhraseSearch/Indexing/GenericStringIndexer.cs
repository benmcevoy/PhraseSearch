using System;
using System.Collections.Generic;
using System.Globalization;
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
            var rootIndex = new Index<string, T>("root", 26);
            var sortedTerms = GetTerms(documents);

            foreach (var sortedTerm in sortedTerms)
            {
                var keyToIndex = sortedTerm.Term;

                for (var depth = 0; depth < _depth; depth++)
                {
                    if (keyToIndex.Length < depth) break;

                    var key = keyToIndex[depth].ToString(CultureInfo.InvariantCulture);
                    var index = GetIndexForDepth(keyToIndex, depth, rootIndex);

                    if (index.Indexer.ContainsKey(key))
                    {
                        index.Indexer[key].Items.Add(sortedTerm);
                        continue;
                    }

                    index.Indexer[key] = new Index<string, T>(key, 26);
                    index.Indexer[key].Items.Add(sortedTerm);
                }
            }

            return rootIndex;
        }

        private Index<string, T> GetIndexForDepth(string keyToIndex, int depth, Index<string, T> index)
        {
            if (string.IsNullOrWhiteSpace(keyToIndex)) return index;
            if (keyToIndex.Length < depth) return index;

            var current = index;

            for (var i = 0; i < depth; i++)
            {
                var key = keyToIndex.Substring(i, 1);

                if (!current.Indexer.ContainsKey(key))
                {
                    current.Indexer[key] = new Index<string, T>(key, 26);
                }

                current = current.Indexer[key];
            }

            return current;
        }

        private IEnumerable<IndexItem<T>> GetTerms(IEnumerable<T> documents)
        {
            var documentArray = documents as T[] ?? documents.ToArray();
            var indexItems = new List<IndexItem<T>>(documentArray.Count() * 2);

            foreach (var document in documentArray)
            {
                var phrase = _termSelector(document);
                var terms = phrase.Split(_splitOn, StringSplitOptions.RemoveEmptyEntries);
                var termPosition = 0;

                foreach (var term in terms)
                {
                    if (term.Length < _depth) continue;

                    var cleanTerm = Clean(term);

                    if (cleanTerm.Length < _depth) continue;

                    termPosition++;
                    indexItems.Add(new IndexItem<T>(document, term, termPosition));
                }
            }

            return indexItems;
        }

        private readonly Regex _cleanRegex = new Regex("[^a-zA-Z]");
        private string Clean(string term)
        {
            return _cleanRegex.Replace(term, "");
        }
    }
}
