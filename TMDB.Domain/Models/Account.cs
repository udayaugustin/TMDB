using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB.Domain.Models
{    
    public class Avatar
    {
        [JsonProperty("gravatar")]
        public Gravatar Gravatar { get; set; }

        [JsonProperty("tmdb")]
        public Tmdb Tmdb { get; set; }
    }

    public class Gravatar
    {
        [JsonProperty("hash")]
        public string Hash { get; set; }
    }

    public class Account
    {
        [JsonProperty("avatar")]
        public Avatar Avatar { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("iso_639_1")]
        public string Iso6391 { get; set; }

        [JsonProperty("iso_3166_1")]
        public string Iso31661 { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("include_adult")]
        public bool IncludeAdult { get; set; }

        [JsonProperty("username")]
        public string Username { get; set; }
    }

    public class Tmdb
    {
        [JsonProperty("avatar_path")]
        public object AvatarPath { get; set; }
    }


}
