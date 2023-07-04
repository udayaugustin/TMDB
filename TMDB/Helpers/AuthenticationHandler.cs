using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            
            return await base.SendAsync(request, cancellationToken);
        }
    }

}
