using NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping;
using NUnit.Framework;

namespace NTextCat.NancyHandlerTests
{
    [TestFixture]
    public class Iso639VariantMappingTests
    {
        [Test]
        public void ParseASingleRowIntoAnObject()
        {
            const string mappingData = "Abkhaz|аҧсуа бызшәа, аҧсшәа|ab|abk";
            Iso639VariantMappings classUnderTest = new Iso639VariantMappings(mappingData);

            Assert.That(classUnderTest.EnglishName, Is.EqualTo("Abkhaz"));
            Assert.That(classUnderTest.NativeName, Is.EqualTo("аҧсуа бызшәа, аҧсшәа"));
            Assert.That(classUnderTest.Iso6391Code, Is.EqualTo("ab"));
            Assert.That(classUnderTest.Iso6393Code, Is.EqualTo("abk"));
        }

        [TestCase("Abkhaz|ab|abk")]
        [TestCase("ab|abk")]
        [TestCase("")]
        public void WhenNotEnoughSegmentsThrowException(string mappingData)
        {
            Assert.Throws<Iso639VariantMappings.InvalidIso639MappingException>(() => new Iso639VariantMappings(mappingData));
        }

        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа|a|abk")]
        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа||abk")]
        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа|abk|abk")]
        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа|abkblahh|abk")]
        public void WhenISO6391SegmentNotTwoCharactersThrowException(string mappingData)
        {
            Assert.Throws<Iso639VariantMappings.InvalidIso6391CodeException>(() => new Iso639VariantMappings(mappingData));
        }

        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа|ab|ab")]
        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа|ab|a")]
        [TestCase("Abkhaz|аҧсуа бызшәа, аҧсшәа|ab|abza")]
        public void WhenIso6393SegmentNotTwoCharactersThrowException(string mappingData)
        {
            Assert.Throws<Iso639VariantMappings.InvalidIso6393CodeException>(() => new Iso639VariantMappings(mappingData));
        }
    }
}