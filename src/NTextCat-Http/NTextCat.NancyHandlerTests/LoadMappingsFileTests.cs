using System.Collections.Generic;
using System.Linq;
using NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping;
using NUnit.Framework;

namespace NTextCat.NancyHandlerTests
{
    [TestFixture]
    public class LoadMappingsFileTests
    {
        [Test]
        public void ReturnsAListOfIso639Mappings()
        {
            var classUnderTest = new Iso639CodeMappingLoader();
            IEnumerable<Iso639VariantMappings> mappings = classUnderTest.LoadMappings();

            Assert.That(mappings.Count(), Is.Not.Null);
        }

        [Test]
        public void CorrectNumberOfMappingsAreLoadedFromTestProfile()
        {
            var classUnderTest = new Iso639CodeMappingLoader();
            IEnumerable<Iso639VariantMappings> mappings = classUnderTest.LoadMappings();

            Assert.That(mappings.Count(), Is.EqualTo(10));
        }
    }
}