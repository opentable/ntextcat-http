using System.Collections.Generic;
using System.Linq;
using IvanAkcheurov.NTextCat.Lib;

namespace NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping
{
    public class DetectedLanguageBuilder
    {
        private readonly IEnumerable<Iso639VariantMappings> _iso639Mappings;

        public DetectedLanguageBuilder(Iso639CodeMappingLoader loader)
        {
            _iso639Mappings = loader.LoadMappings();
        }

        public DetectedLangage BuildFromResult(LanguageInfo languageInfo, double score)
        {
            Iso639VariantMappings matchingMapping =
                _iso639Mappings.SingleOrDefault(mapping => mapping.Iso6393Code == languageInfo.Iso639_3);

            if (matchingMapping == null)
                matchingMapping = new Iso639VariantMappings("", "", "", "");

            return new DetectedLangage(languageInfo.Iso639_3, matchingMapping.Iso6391Code, matchingMapping.EnglishName, matchingMapping.NativeName ,score);
        }
    }
}