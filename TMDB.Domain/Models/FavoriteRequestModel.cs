using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMDB.Domain.Models
{
    public class FavoriteRequestModel
    {
        [JsonProperty("media_type")]
        public string MediaType { get; set; }

        [JsonProperty("media_id")]
        public int MediaId { get; set; }

        [JsonProperty("favorite")]
        public bool Favorite { get; set; }
    }

}
