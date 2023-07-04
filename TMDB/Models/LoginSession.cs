using Newtonsoft.Json;

namespace TMDB.Models
{
    public class LoginSession
    {
        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("session_id")]
        public string SessionId { get; set; }
    }
}
