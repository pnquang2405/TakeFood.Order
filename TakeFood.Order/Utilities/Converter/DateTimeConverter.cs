using System.Text.Json;
using System.Text.Json.Serialization;

namespace Order.Utilities.Converter;

public class DateTimeConverter : JsonConverter<DateTime?>
{
    /// <summary>
    /// Đọc từ json sang kiểu DateTime?
    /// </summary>
    /// <param name="reader"></param>
    /// <param name="typeToConvert"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        DateTime? rs = null;
        switch (reader.TokenType)
        {
            case JsonTokenType.String:
                if (DateTime.TryParse(reader.GetString(), out DateTime date))
                {
                    rs = date;
                }
                break;
            case JsonTokenType.Number:
                rs = reader.GetDateTime();
                break;
            case JsonTokenType.Null:
                break;
            case JsonTokenType.None:
            case JsonTokenType.True:
            case JsonTokenType.False:
            case JsonTokenType.Comment:
            case JsonTokenType.PropertyName:
            case JsonTokenType.StartObject:
            case JsonTokenType.EndObject:
            case JsonTokenType.StartArray:
            case JsonTokenType.EndArray:
            default:
                rs = new DateTime();
                break;
        }
        return rs;
    }

    /// <summary>
    /// Write từ kiểu DateTime? sang json
    /// </summary>
    /// <param name="writer"></param>
    /// <param name="value"></param>
    /// <param name="options"></param>
    public override void Write(Utf8JsonWriter writer, DateTime? value, JsonSerializerOptions options)
    {
        if (value != null)
        {
            writer.WriteNumberValue(value.Value.ToFileTimeUtc());
        }
        else
        {
            throw new Exception("Expected date object value.");
        }
    }
}
