using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using PhraseSearch.Indexing;
using PhraseSearch.Scoring;
using PhraseSearch.Searching;

namespace PhraseSearch
{
    class Program
    {
        static void Main(string[] args)
        {
            var documents = CreateDocuments();

            const int keyDepth = 3;

            var sw = new Stopwatch();

            Console.WriteLine("creating index (1 million documents)...");

            sw.Start();

            var index = new GenericStringIndexer<Document>(document => document.Phrase, keyDepth).Index(documents);

            sw.Stop();

            Console.WriteLine("took {0}ms", sw.ElapsedMilliseconds);

            sw.Restart();

            var searcher = new GenericStringSearcher<Document>(keyDepth,
                (phraseTerm, searchTerm) => phraseTerm.StartsWith(searchTerm),
                new GenericScorer<Document>(),
                new DocumentAccumulator());

            var hits = searcher.Search(new[] { "parad", "par" }, index);

            var results = hits
                .Take(10)
                .ToList();

            sw.Stop();

            foreach (var hit in results)
            {
                Console.WriteLine("{0},{1},{2}", hit.Score, hit.Document.Id, hit.Document.Phrase);
            }

            Console.WriteLine("search 1 million docs took {0}ms", sw.ElapsedMilliseconds);
            Console.WriteLine("press a key to finish");
            Console.ReadKey();

        }

        public static IEnumerable<Document> CreateDocuments()
        {
            var documents = new List<Document>(1000000);

            var phrases = File.ReadAllLines("phrases.txt");
            var id = 1;

            foreach (var phrase in phrases)
            {
                documents.Add(new Document
                {
                    Id = id,
                    Phrase = phrase
                });

                id++;
            }

            return documents;
        }
    }
}
