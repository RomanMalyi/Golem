using Nest;

namespace Golem.Data.Elasticsearch.Models
{
    public class Project
    {
        public string Id { get; set; }

        [Keyword] public string Title { get; set; }

        [Text(
            Analyzer = Indices.IndexAnalyzerName,
            SearchAnalyzer = Indices.SearchAnalyzerName
        )]
        public string Description { get; set; }
    }
}
