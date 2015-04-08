using System.Configuration;

namespace NTextCat.NancyHandler.LanguageDetection.Configuration
{
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
}