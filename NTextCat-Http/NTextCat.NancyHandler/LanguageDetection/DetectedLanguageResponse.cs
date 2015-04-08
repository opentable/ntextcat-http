using System.Collections.Generic;

namespace NTextCat.NancyHandler.LanguageDetection
{
    public class DetectedLanguageResponse
    {
        private readonly List<DetectedLangage> _rankedMatches;
        private readonly string _originalText;

        public DetectedLanguageResponse()
        {
            _rankedMatches = new List<DetectedLangage>();
        }

        public DetectedLanguageResponse(IEnumerable<DetectedLangage> languagesDetected, string originalText) : this()
        {
            _rankedMatches.AddRange(languagesDetected);
            _originalText = originalText;
        }

        public string OriginalText { get { return _originalText; } }
        public IEnumerable<DetectedLangage> RankedMatches { get { return _rankedMatches; }}
    }
}