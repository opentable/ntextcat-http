using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using IvanAkcheurov.NTextCat.Lib;
using Nancy;
using Nancy.ModelBinding;

namespace NTextCat.NancyHandler
{
    public class LanguageDetectModule : NancyModule
    {
        public LanguageDetectModule(INTextCatMatchingProfileLoader profileLoader = null)
        {
            if (profileLoader == null)
                profileLoader = new NTextCatMatchingProfileLoader();
            
            Get["/language-detector"] = _ =>
            {
                return View["languages", new DetectedLanguageResponse()];
            };

            Post["/language-detector"] = _ =>
            {
                var model = this.Bind<LanguageDetectRequest>();
                if (model == null || string.IsNullOrWhiteSpace(model.TextForLanguageClassification))
                    return HttpStatusCode.BadRequest;

                var detectedLanguageResponse = DetectLanguage(profileLoader, model);
                detectedLanguageResponse.OriginalText = model.TextForLanguageClassification;

                return Negotiate
                    .WithModel(detectedLanguageResponse)
                    .WithView("languages");
            };

        }

        private DetectedLanguageResponse DetectLanguage(INTextCatMatchingProfileLoader profileLoader, LanguageDetectRequest model)
        {
            var factory = new RankedLanguageIdentifierFactory();
            var identifier = factory.Load(profileLoader.MatchingProfileFile);

            var matches = identifier.Identify(model.TextForLanguageClassification);
            var detectedLanguageResponse = FormatResponse(matches);
            return detectedLanguageResponse;
        }

        private DetectedLanguageResponse FormatResponse(IEnumerable<Tuple<LanguageInfo, double>> matches)
        {
            var responseList = matches.Select(
                match => new DetectedLangage() {Iso6393LanguageCode = match.Item1.Iso639_3, MatchScore = match.Item2})
                .ToList();
            return new DetectedLanguageResponse() {RankedMatches = responseList};
        }
    }

    public interface INTextCatMatchingProfileLoader
    {
        string MatchingProfileFile { get; }
    }

    public class NTextCatMatchingProfileLoader : INTextCatMatchingProfileLoader
    {
        private string _matchingProfile;

        public string MatchingProfileFile
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_matchingProfile))
                    _matchingProfile = LoadFromAppSettings();

                return _matchingProfile;
            }
            set { _matchingProfile = value; }
        }

        private string LoadFromAppSettings()
        {
            return ConfigurationManager.AppSettings["NTextCat.MatchingProfile"];
        }
    }

    public class DetectedLanguageResponse
    {
        public string OriginalText { get; set; }
        public List<DetectedLangage> RankedMatches { get; set; }
    }

    public class DetectedLangage
    {
        public string Iso6393LanguageCode { get; set; }
        public double MatchScore { get; set; }
    }

    public class LanguageDetectRequest
    {
        public string TextForLanguageClassification { get; set; }
    }
}
