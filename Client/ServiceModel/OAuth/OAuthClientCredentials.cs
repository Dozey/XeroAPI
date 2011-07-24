using System.IdentityModel.Selectors;
using System.ServiceModel.Description;

namespace XeroAPI.Client.ServiceModel.OAuth
{
    class OAuthClientCredentials : ClientCredentials
    {
        public OAuthClientCredentials() { }

        private OAuthClientCredentials(string key, string secret, string token, string tokenSecret, string pfxFile, string pfxPassword, string realm)
        {
            OAuthKey = key;
            OAuthToken = token;
            OAuthPfxFile = pfxFile;
            OAuthPfxPassword = pfxPassword;
            OAuthRealm = realm;
        }


        public string OAuthKey { get; set; }
        public string OAuthSecret { get; set; }
        public string OAuthToken { get; set; }
        public string OAuthTokenSecret { get; set; }
        public string OAuthPfxFile { get; set; }
        public string OAuthPfxPassword { get; set; }
        public string OAuthRealm { get; set; }

        protected override ClientCredentials CloneCore()
        {
            return new OAuthClientCredentials(OAuthKey, OAuthSecret, OAuthToken, OAuthTokenSecret, OAuthPfxFile, OAuthPfxPassword, OAuthRealm);
        }
    }
}
