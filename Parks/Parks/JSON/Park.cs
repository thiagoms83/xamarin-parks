using Newtonsoft.Json;
using Xamarin.Forms;

namespace Parks.JSON
{
    [JsonObject]
    public class Park
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("createDate")]
        public string CreateDate { get; set; }

        [JsonProperty("lat")]
        public double Latitude { get; set; }

        [JsonProperty("long")]
        public double Longitude { get; set; }

        [JsonProperty("distance")]
        public float Distance { get; set; }

        [JsonProperty("image")]
        public string Image { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("data")]
        public string Data { get; set; }

        public ImageSource Imagem { get; set; }
    }
}
