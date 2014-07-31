using System.Collections.Generic;
using System.Linq;
using PhraseSearch.Searching;

namespace PhraseSearch
{
    public class DocumentAccumulator : IAccumulator<Document>
    {
        public IEnumerable<SearchHit<Document>> Accumulate(IEnumerable<SearchHit<Document>> hits)
        {
            var groupedById = hits.GroupBy(hit => hit.Document.Id);

            return groupedById.Select(idGroup => new SearchHit<Document>(idGroup.Sum(hit => hit.Score), idGroup.First()));
        }
    }
}
