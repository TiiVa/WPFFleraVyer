using System.Text.Json.Serialization;

namespace Common.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HairColor
{
    None,
    Blonde,
    Brown,
    Red,
    Purple,
    Black,
    White,
    Grey
}