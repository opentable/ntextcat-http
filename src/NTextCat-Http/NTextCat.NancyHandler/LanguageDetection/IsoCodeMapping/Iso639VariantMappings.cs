using System;

namespace NTextCat.NancyHandler.LanguageDetection.IsoCodeMapping
{
    public class Iso639VariantMappings
    {
        public string Iso6391Code { get; private set; }
        public string Iso6393Code { get; private set; }
        public string EnglishName { get; private set; }
        public string NativeName { get; private set; }

        public Iso639VariantMappings(string iso639_1Code, string iso639_3Code, string englishName, string nativeName)
        {
            Iso6391Code = iso639_1Code;
            Iso6393Code = iso639_3Code;
            EnglishName = englishName;
            NativeName = nativeName;
        }

        public Iso639VariantMappings(string input)
        {
            string[] segments = input.Split('|');

            if (segments.Length < 4)
                throw new InvalidIso639MappingException();
            
            EnglishName = segments[0];
            NativeName = segments[1];
            Iso6391Code = GetValidIso6391Segment(segments);
            Iso6393Code = GetValidIso6393Segment(segments);
        }

        private static string GetValidIso6393Segment(string[] segments)
        {
            string iso6393Code = segments[3];
            if (iso6393Code.Length != 3)
                throw new InvalidIso6393CodeException(iso6393Code);
            return iso6393Code;
        }

        private static string GetValidIso6391Segment(string[] segments)
        {
            string iso6391Code = segments[2];
            if (iso6391Code.Length != 2)
                throw new InvalidIso6391CodeException(iso6391Code);
            return iso6391Code;
        }

        public class InvalidIso639MappingException : Exception
        {
            public InvalidIso639MappingException()
                : base("Bad format of ISO639 mapping file. Format should be '{englishName}|{nativeName}|{ISO639-1Code}|{ISO639-2Code}'")
            {
            }
        }

        public class InvalidIso6391CodeException : Exception
        {
            public InvalidIso6391CodeException(string incorrectIsoCode) : base("ISO639-1 Codes are exactly two characters. Code provided: " + incorrectIsoCode)
            {
            }
        }

        public class InvalidIso6393CodeException : Exception
        {
            public InvalidIso6393CodeException(string incorrectIsoCode) : base("ISO639-3 Codes are exactly three characters. Code provided: " + incorrectIsoCode)
            {
            }
        }
    }
}