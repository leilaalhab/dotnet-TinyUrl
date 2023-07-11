using System.Text.Json.Serialization;


namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RegionClass
    {
        NA = 1,
        EUR = 2,
        RUS = 3,
        SA = 4,
        ASIA = 5,
    }
}