using System.Text.Json.Serialization;
using pawd.CoreLibrary.Hal;

namespace pawd.CoreLibrary.HalForms.Models;

/// <summary>
/// Represents a complete HAL-FORMS document, including hypermedia links, templates for actions, embedded resources, and additional properties.
/// </summary>
public class HalFormsDocument : HalDocument
{
    /// <summary>
    /// Gets or sets the collection of form templates available for interaction with the resource.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "_templates" property in HAL-FORMS. Each key is a template name, and the value is a <see cref="HalFormTemplate"/>.
    /// </remarks>
    [JsonPropertyName("_templates")]
    public Dictionary<string, HalFormTemplate> Templates { get; set; } = new();
}
