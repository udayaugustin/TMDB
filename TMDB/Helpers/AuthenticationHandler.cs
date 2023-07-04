using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TMDB.Helpers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        private readonly string accessToken;
        
        public AuthenticationHandler(HttpMessageHandler handler, string accessToken)
            : base(handler) 
        {            
            this.accessToken = accessToken;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", await SecureStorage.GetAsync("SessionId"));

            var uriBuilder = new UriBuilder(request.RequestUri);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["api_key"] = Constants.ApiKey;            

            uriBuilder.Query = queryParams.ToString();
            request.RequestUri = uriBuilder.Uri;            
            
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
