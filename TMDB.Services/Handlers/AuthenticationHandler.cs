using System.Net.Http.Headers;
using System.Web;
using TMDB.Domain.Constants;

namespace TMDB.Services.Handlers
{
    public class AuthenticationHandler : DelegatingHandler
    {
        public string SessionId { get; set; }

        public AuthenticationHandler(HttpMessageHandler handler)
            : base(handler) 
        {                    
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", SessionId);

            var uriBuilder = new UriBuilder(request.RequestUri);
            var queryParams = HttpUtility.ParseQueryString(uriBuilder.Query);
            queryParams["api_key"] = Constants.ApiKey;

            uriBuilder.Query = queryParams.ToString();
            request.RequestUri = uriBuilder.Uri;            
            
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
