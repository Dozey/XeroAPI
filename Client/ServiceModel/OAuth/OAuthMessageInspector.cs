using System;
using System.Collections.Specialized;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using OAuth.Net.Common;
using OAuth.Net.Components;
using System.Web;
using XeroAPI.Client.ServiceModel.OAuth;

namespace XeroAPI.Client.ServiceModel
{
    class OAuthMessageInspector : IClientMessageInspector
    {
        private OAuthClientCredentials _credentials;
        private GuidNonceProvider _nonceProvider;

        public OAuthMessageInspector(OAuthClientCredentials credentials)
        {
            _credentials = credentials;
            _nonceProvider = new GuidNonceProvider();
        }

        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            HttpResponseMessageProperty httpResponseProperty = reply.Properties[HttpResponseMessageProperty.Name] as HttpResponseMessageProperty;
            
            if(httpResponseProperty != null && httpResponseProperty.StatusCode != System.Net.HttpStatusCode.OK)
            {
                   
            }
        }

        public object BeforeSendRequest(ref Message request, IClientChannel channel)
        {
            HttpRequestMessageProperty httpRequestProperty = request.Properties[HttpRequestMessageProperty.Name] as HttpRequestMessageProperty;

            NameValueCollection rawParameters = HttpUtility.ParseQueryString(request.Headers.To.Query);
            UriBuilder uriBuilder = new UriBuilder(request.Headers.To);
            uriBuilder.Query = Rfc3986.EncodeAndJoin(rawParameters);

            request.Headers.To = uriBuilder.Uri;

            OAuthConsumer consumer = new OAuthConsumer(_credentials.OAuthKey, _credentials.OAuthSecret);


            int unixTime = UnixTime.ToUnixTime(DateTime.UtcNow);

            OAuthParameters parameters = new OAuthParameters();
            parameters.Version = Constants.Version1_0;
            parameters.SignatureMethod = "RSA-SHA1";
            parameters.ConsumerKey = _credentials.OAuthKey;
            parameters.Token = _credentials.OAuthToken;
            parameters.TokenSecret = _credentials.OAuthTokenSecret;
            parameters.Timestamp = unixTime.ToString();
            parameters.Nonce = _nonceProvider.GenerateNonce(unixTime);


            RsaSha1SigningProvider provider = new RsaSha1SigningProvider();
            provider.PfxFile = _credentials.OAuthPfxFile;
            provider.PfxPassword = _credentials.OAuthPfxPassword;

            parameters.Sign(request.Headers.To, httpRequestProperty.Method, consumer, null, provider);

            httpRequestProperty.Headers["Authorization"] = parameters.ToHeaderFormat();

            return null;
        }
    }
}
