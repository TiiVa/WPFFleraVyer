using System.Text.Json.Serialization;

namespace Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum AnimalType
{
    None,
    Dog,
    Cat,
    Rabbit,
    Lizard,
    Snake,
    Bird,
    Spider,
    Piglet
}