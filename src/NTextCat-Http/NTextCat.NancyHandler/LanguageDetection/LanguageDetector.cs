using System;
using System.Collections.Generic;
using System.Linq;
using IvanAkcheurov.NTextCat.Lib;
using NTextCat.NancyHandler.LanguageDetection.Configuration;

namespace NTextCat.NancyHandler.LanguageDetection
{
    public class LanguageDetector
    {
        private readonly INTextCatMatchingProfileLoader _profileLoader;

        public LanguageDetector(INTextCatMatchingProfileLoader profileLoader)
        {
            _profileLoader = profileLoader;
        }

        public DetectedLanguageResponse DetectLanguage(LanguageDetectRequest model)
        {
            var identifier = new RankedLanguageIdentifierFactory().Load(_profileLoader.MatchingProfileFile);

            IEnumerable<Tuple<LanguageInfo, double>> matches = identifier.Identify(model.TextForLanguageClassification);
            DetectedLanguageResponse detectedLanguageResponse = FormatResponse(matches, model);

            return detectedLanguageResponse;
        }

        private DetectedLanguageResponse FormatResponse(IEnumerable<Tuple<LanguageInfo, double>> matches, LanguageDetectRequest model)
        {
            List<DetectedLangage> responseList = matches.Select(
                match => new DetectedLangage(match.Item1.Iso639_3, match.Item2))
                .ToList();
            return new DetectedLanguageResponse(responseList, model.TextForLanguageClassification);
        }
    }
}