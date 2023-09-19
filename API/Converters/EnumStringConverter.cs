using System.Text.Json;
using System.Text.Json.Serialization;

namespace Core.Converters
{
    public class EnumStringConverters<TEnum> : JsonConverter<TEnum>
    {
        public override TEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                string enumString = reader.GetString();
                return (TEnum)Enum.Parse(typeof(TEnum), enumString, true);
            }
            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, TEnum value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
