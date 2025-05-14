using System.Text.Json;
using System.Text.Json.Serialization;

namespace pawd.CoreLibrary.Hal.JsonFormatters;

public class HalDocumentJsonConverter : JsonConverter<HalDocument>
{
    public override HalDocument? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<HalDocument>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, HalDocument value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        writer.WritePropertyName("_links");
        JsonSerializer.Serialize(writer, value.Links, options);

        writer.WritePropertyName("_embedded");
        JsonSerializer.Serialize(writer, value.Embedded, options);

        var keyPolicy = options.DictionaryKeyPolicy ?? JsonNamingPolicy.CamelCase;
        foreach (var kv in value.Properties)
        {
            var camelKey = keyPolicy.ConvertName(kv.Key);
            writer.WritePropertyName(camelKey);
            JsonSerializer.Serialize(writer, kv.Value, kv.Value.GetType(), options);
        }

        writer.WriteEndObject();
    }
}