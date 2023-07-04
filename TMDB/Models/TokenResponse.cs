using Newtonsoft.Json;

namespace TMDB.Models
{
    public class TokenResponse
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("expires_at")]
        public string ExpiresAt { get; set; }

        [JsonProperty("request_token")]
        public string RequestToken { get; set; }
    }
}
