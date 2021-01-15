using Newtonsoft.Json;

public class Photo
{
    public int Id { get; set; }
    [JsonProperty(PropertyName = "AlbumId")]
    public int PhotoBook { get; set; }
    public string Title { get; set; }
    public string Url { get; set; }
}