using System.Text.Json.Serialization;


namespace dotnet_rpg.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SubscriptionTypeClass
    {
        Normal = 1,
        Premium = 2,
        Ultra = 3,
    }
}