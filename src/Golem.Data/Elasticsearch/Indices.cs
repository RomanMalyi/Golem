using Nest;

namespace Golem.Data.Elasticsearch
{
    public static class Indices
    {
        public const string IndexAnalyzerName = "autocomplete";
        public const string SearchAnalyzerName = "autocomplete_search";
        
        public static IAnalysis AddCustomSearchAnalyzer(this AnalysisDescriptor analysis)
        {
            const string lowercase = nameof(lowercase);

            // https://www.elastic.co/guide/en/elasticsearch/reference/current/analysis-edgengram-tokenizer.html
            // names aren't really important, they are just keys
            return 
                analysis
                    .Analyzers(a => a
                        .Custom(IndexAnalyzerName, c => c
                            .Tokenizer(IndexAnalyzerName)
                            .Filters(lowercase)
                        )
                        .Custom(SearchAnalyzerName, c =>
                            c.Tokenizer(lowercase)
                        )
                    )
                    .Tokenizers(t => t
                        .EdgeNGram(IndexAnalyzerName, e => e
                            .MinGram(1)
                            .MaxGram(20)
                            .TokenChars(TokenChar.Letter)
                        )
                    );
        }
    }
}
