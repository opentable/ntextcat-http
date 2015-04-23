namespace NTextCat.NancyHandler.LanguageDetection
{
    public class DetectedLangage
    {
        public DetectedLangage(string iso6393LanguageCode, string iso6391LanguageCode, string englishName, string nativeName, double matchScore)
        {
            Iso6393LanguageCode = iso6393LanguageCode;
            MatchScore = matchScore;
            Iso6391LanguageCode = iso6391LanguageCode;
            EnglishName = englishName;
            NativeName = nativeName;
        }

        public string Iso6393LanguageCode { get; private set; }
        public double MatchScore { get; private set; }
        public string Iso6391LanguageCode { get; private set; }
        public string EnglishName { get; private set; }
        public string NativeName { get; private set; }
    }
}