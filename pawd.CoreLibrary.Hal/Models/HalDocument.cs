using System.Text.Json.Serialization;
using pawd.CoreLibrary.Hal.Models;

namespace pawd.CoreLibrary.Hal;

public class HalDocument
{
    /// <summary>
    /// Gets or sets the collection of hypermedia links for the resource.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "_links" property in HAL and HAL-FORMS. Each key is a relation (rel), and the value is a <see cref="HalLink"/>.
    /// </remarks>
    [JsonPropertyName("_links")]
    public Dictionary<string, HalLink> Links { get; set; } = new();
    
    /// <summary>
    /// Gets or sets additional properties not explicitly defined in the model.
    /// </summary>
    /// <remarks>
    /// This uses <see cref="JsonExtensionDataAttribute"/> to capture any extra JSON fields not mapped to class members.
    /// Useful for forward compatibility or extensions.
    /// </remarks>
    [JsonExtensionData]
    public Dictionary<string, object> Properties { get; set; } = new();
    
    /// <summary>
    /// Gets or sets the collection of embedded resources.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "_embedded" property in HAL. Each key is a relation (rel), and the value is typically a nested resource or collection of resources.
    /// </remarks>
    [JsonPropertyName("_embedded")]
    public Dictionary<string, object> Embedded { get; set; } = new();
}