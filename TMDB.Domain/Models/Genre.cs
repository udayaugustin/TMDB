using Newtonsoft.Json;

namespace TMDB.Models
{
    public class Genre
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class GenreList
    {
        [JsonProperty("genres")]
        public List<Genre> Genres { get; set; }
    }
}
