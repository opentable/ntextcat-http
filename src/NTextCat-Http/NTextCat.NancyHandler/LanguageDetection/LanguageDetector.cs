using System;
using System.Collections.Generic;
using System.Linq;
using IvanAkcheurov.NTextCat.Lib;
using NTextCat.NancyHandler.LanguageDetection.Configuration;
using NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping;

namespace NTextCat.NancyHandler.LanguageDetection
{
    public class LanguageDetector
    {
        private readonly INTextCatMatchingProfileLoader _profileLoader;
        private readonly DetectedLanguageBuilder _builder;

        public LanguageDetector(INTextCatMatchingProfileLoader profileLoader, DetectedLanguageBuilder builder)
        {
            _profileLoader = profileLoader;
            _builder = builder;
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
                match => _builder.BuildFromResult(match.Item1, match.Item2))
                .ToList();
            return new DetectedLanguageResponse(responseList, model.TextForLanguageClassification);
        }
    }
}