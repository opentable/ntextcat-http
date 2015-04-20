using System.Collections.Generic;

namespace NTextCat.NancyHandler.LanguageDetection
{
    public class DetectedLanguageResponse
    {
        public DetectedLanguageResponse()
        {
            RankedMatches = new List<DetectedLangage>();
        }

        public DetectedLanguageResponse(List<DetectedLangage> languagesDetected, string originalText)
        {
            RankedMatches = languagesDetected;
            OriginalText = originalText;
        }

        public string OriginalText { get; set; }
        public List<DetectedLangage> RankedMatches { get; set; }
    }
}