using System.Collections.Generic;

namespace GillowWebApp
{

    public partial class Locations
    {
        public List<Prediction> Predictions { get; set; }
        public string Status { get; set; }
    }

    public partial class Prediction
    {
        public string Description { get; set; }
        public List<MatchedSubstring> MatchedSubstrings { get; set; }
        public string PlaceId { get; set; }
        public string Reference { get; set; }
        public StructuredFormatting StructuredFormatting { get; set; }
        public List<Term> Terms { get; set; }
        public List<string> Types { get; set; }
    }

    public partial class MatchedSubstring
    {
        public long Length { get; set; }
        public long Offset { get; set; }
    }

    public partial class StructuredFormatting
    {
        public string MainText { get; set; }
        public List<MatchedSubstring> MainTextMatchedSubstrings { get; set; }
        public string SecondaryText { get; set; }
    }

    public partial class Term
    {
        public long Offset { get; set; }
        public string Value { get; set; }
    }
}
