using System.Collections.Generic;
using System.IO;

namespace NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping
{
    public class Iso639CodeMappingLoader
    {
        public virtual IEnumerable<Iso639VariantMappings> LoadMappings()
        {
            var iso639VariantMappings = new List<Iso639VariantMappings>();
            using (TextReader reader = new StreamReader("ISOCodeLookup.txt"))
            {
                string line = reader.ReadLine();
                do
                {
                    iso639VariantMappings.Add(new Iso639VariantMappings(line));
                    line = reader.ReadLine();
                } while (line != null);
            }

            return iso639VariantMappings;
        }
    }
}