using System.Text.Json;
using System.Text.Json.Serialization;
using pawd.CoreLibrary.HalForms.Models;

namespace pawd.CoreLibrary.HalForms.JsonFormatters;

public class HalFormsDocumentJsonConverter : JsonConverter<HalFormsDocument>
{
    public override HalFormsDocument? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return JsonSerializer.Deserialize<HalFormsDocument>(ref reader, options);
    }

    public override void Write(Utf8JsonWriter writer, HalFormsDocument value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();

        if (value.Links is { Count: > 0 })
        {
            writer.WritePropertyName("_links");
            JsonSerializer.Serialize(writer, value.Links, options);
        }

        if (value.Embedded is { Count: > 0 })
        {
            writer.WritePropertyName("_embedded");
            JsonSerializer.Serialize(writer, value.Embedded, options);
        }

        if (value.Templates is { Count: > 0 })
        {
            writer.WritePropertyName("_templates");
            JsonSerializer.Serialize(writer, value.Templates, options);
        }

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