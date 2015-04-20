namespace NTextCat.NancyHandler.LanguageDetection
{
    public class DetectedLangage
    {
        public DetectedLangage()
        { }

        public DetectedLangage(string iso6393LanguageCode, double matchScore)
        {
            Iso6393LanguageCode = iso6393LanguageCode;
            MatchScore = matchScore;
        }

        public string Iso6393LanguageCode { get; set; }
        public double MatchScore { get; set; }
    }
}