using System;
using System.Collections.Generic;
using System.Linq;
using Nancy;
using Nancy.Testing;
using NTextCat.NancyHandler;
using NTextCat.NancyHandler.LanguageDetection;
using NTextCat.NancyHandler.LanguageDetection.Configuration;
using NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping;
using NUnit.Framework;

namespace NTextCat.NancyHandlerTests
{
    [TestFixture]
    public class LangugeDetectModuleTests
    {
        private Browser _browser;

        [SetUp]
        public void ConfigureBrowserToTestLanguageDetector()
        {
            _browser = new Browser(with => with.Module(BuildSystemUnderTest()));
        }

        private BrowserResponse Post(Action<BrowserContext> with = null)
        {
            return _browser.Post("/language-detector", with);
        }

        [Test]
        public void WhenNoBodyBadRequest()
        {
            var response = Post();
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void WhenBodyInvalidBadRequest()
        {
            BrowserResponse response = Post(with =>
            {
                with.Body("random");
            });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.BadRequest));
        }

        [Test]
        public void WhenBodyCorrectOkResponse()
        {
            BrowserResponse response = Post(with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = "some words" });
            });
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public void TestSupportsXml()
        {
            BrowserResponse response = Post(with =>
            {
                with.Header("Accept", "application/xml");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = "some words" });
            });

            var body = response.Body.DeserializeXml<DetectedLanguageResponse>();

            Assert.That(body, Is.Not.Null);
        }

        [Test]
        public void TestSupportsJson()
        {
            BrowserResponse response = Post(with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = "some words" });
            });

            var body = response.Body.DeserializeJson<DetectedLanguageResponse>();

            Assert.That(body, Is.Not.Null);
        }

        [Test]
        public void ReturnsOriginalTextWithResponse()
        {
            const string sampleText = "some words";

            BrowserResponse response = Post(with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = sampleText });
            });

            var body = response.Body.DeserializeJson<DetectedLanguageResponse>();

            Assert.That(body.OriginalText, Is.EqualTo(sampleText));
        }

        [Test]
        public void ReturnsListOfPossibleLanguagesWithResponse()
        {
            const string sampleText = "This is a sample of english language text";

            var response = Post(with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = sampleText });
            });

            var body = response.Body.DeserializeJson<DetectedLanguageResponse>();

            Assert.That(body.RankedMatches.Count, Is.GreaterThan(1));
        }

        [Test]
        public void CheckEnglishIsDetectedAsExpected()
        {
            const string sampleText = "This is a sample of english language text";

            var response = Post(with =>
            {
                with.Header("Accept", "application/json");
                with.JsonBody(new LanguageDetectRequest() { TextForLanguageClassification = sampleText });
            });

            var body = response.Body.DeserializeJson<DetectedLanguageResponse>();

            Assert.That(body.RankedMatches.First().Iso6393LanguageCode, Is.EqualTo("eng"));
            Assert.That(body.RankedMatches.First().MatchScore, Is.GreaterThan(0));
        }
                
        private LanguageDetectModule BuildSystemUnderTest()
        {
            return new LanguageDetectModule(new LanguageDetector(new NTextCatMatchingProfileLoader(), new DetectedLanguageBuilder(new Iso639CodeMappingLoader())));
        }

        public class DetectedLanguageResponse
        {
            public List<DetectedLangage> RankedMatches;
            public string OriginalText;
        }

        public class DetectedLangage
        {
            public string EnglishName;
            public string NativeName;
            public string Iso6391LanguageCode;
            public string Iso6393LanguageCode;
            public double MatchScore;
        }
    }
}
