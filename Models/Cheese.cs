namespace CheezeGallery.Models;

using Newtonsoft.Json;

public class Cheese
{
    [JsonProperty(PropertyName = "id")]
    public string? Id { get; set; }
    [JsonProperty(PropertyName = "name")]
    public string? Name {get; set;}
    [JsonProperty(PropertyName = "country")]
    public string? Country { get; set; }
}