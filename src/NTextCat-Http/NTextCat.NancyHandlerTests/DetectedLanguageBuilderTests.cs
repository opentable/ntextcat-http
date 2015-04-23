using System;
using System.Collections.Generic;
using IvanAkcheurov.NTextCat.Lib;
using NTextCat.NancyHandler.LanguageDetection;
using NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping;
using NUnit.Framework;

namespace NTextCat.NancyHandlerTests
{
    [TestFixture]
    public class DetectedLanguageBuilderTests
    {
        [Test]
        public void CopiesLangugeInfoValuesToDetectedLanguageResponse()
        {
            var classUnderTest = new DetectedLanguageBuilder(CreateLanguageIsoCodeMappings());
            var iso6393 = "eng";
            var languageInfo = new LanguageInfo("", iso6393, "", "");
            const double score = 0;
            DetectedLangage result = classUnderTest.BuildFromResult(languageInfo, score);

            Assert.That(result.Iso6393LanguageCode, Is.EqualTo(iso6393));
        }

        [Test]
        public void CopiesTheScoreToTheResult()
        {
            var classUnderTest = new DetectedLanguageBuilder(CreateLanguageIsoCodeMappings());
            var languageInfo = new LanguageInfo("", "eng", "", "");
            double score = new Random().NextDouble();
            DetectedLangage result = classUnderTest.BuildFromResult(languageInfo, score);

            Assert.That(result.MatchScore, Is.EqualTo(score));
        }

        [Test]
        public void GivenListOfIso639VariantMappingsCorrectlyBuilDetectedLanauge    ()
        {
            var classUnderTest = new DetectedLanguageBuilder(CreateLanguageIsoCodeMappings());
            var languageInfo = new LanguageInfo("", "eng", "", "");
            DetectedLangage result = classUnderTest.BuildFromResult(languageInfo, 0);

            Assert.That(result.Iso6391LanguageCode, Is.EqualTo("en"));
            Assert.That(result.EnglishName, Is.EqualTo("English"));
            Assert.That(result.NativeName, Is.EqualTo("English"));
        }

        [Test]
        public void WhenNoMatchingMappingReturnsOnlyValuesInLanguageInfo()
        {
            var classUnderTest = new DetectedLanguageBuilder(new StubIso639CodeMappingLoader());
            var languageInfo = new LanguageInfo("", "eng", "", "");
            double score = new Random().NextDouble();
            DetectedLangage result = classUnderTest.BuildFromResult(languageInfo, score);

            Assert.That(result.MatchScore, Is.EqualTo(score));
            Assert.That(result.Iso6393LanguageCode, Is.EqualTo("eng"));
        }

        private static StubIso639CodeMappingLoader CreateLanguageIsoCodeMappings()
        {
            return new StubIso639CodeMappingLoader();
        }
    }

    public class StubIso639CodeMappingLoader : Iso639CodeMappingLoader
    {
        public override IEnumerable<Iso639VariantMappings> LoadMappings()
        {
            var iso639Mappings = new List<Iso639VariantMappings>
            {
                new Iso639VariantMappings("en", "eng", "English", "English")
            };
            return iso639Mappings;
        }
    }
}