using System.Text.Json.Serialization;

namespace pawd.CoreLibrary.HalForms.Models;

/// <summary>
/// Represents a property definition in a HAL-FORMS template, describing the expected input fields for a client interaction.
/// </summary>
public sealed class HalFormProperty
{
    /// <summary>
    /// Gets or sets the name of the property.
    /// </summary>
    /// <remarks>
    /// This is a required field and typically matches the name of the property in the target resource.
    /// Corresponds to the "name" field in HAL-FORMS.
    /// </remarks>
    [JsonPropertyName("name")]
    public string Name { get; set; }

    /// <summary>
    /// Gets or sets the data type of the property (e.g., "text", "number", "boolean").
    /// </summary>
    /// <remarks>
    /// Corresponds to the "type" field in HAL-FORMS. Common values include "text", "number", "date", etc.
    /// </remarks>
    [JsonPropertyName("type")]
    public string Type { get; set; }

    /// <summary>
    /// Gets or sets the default value of the property.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "value" field in HAL-FORMS. This value may be pre-populated in client forms.
    /// </remarks>
    [JsonPropertyName("value")] 
    public object Value { get; set; }

    /// <summary>
    /// Gets or sets a human-readable label or description for the property.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "prompt" field in HAL-FORMS. Useful for UI rendering or documentation.
    /// </remarks>
    [JsonPropertyName("prompt")] 
    public string Prompt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this property is required.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "required" field in HAL-FORMS.
    /// </remarks>
    [JsonPropertyName("required")]
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether this property is read-only.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "readOnly" field in HAL-FORMS. Read-only fields should not be altered by clients.
    /// </remarks>
    [JsonPropertyName("readOnly")] 
    public bool ReadOnly { get; set; }

    /// <summary>
    /// Gets or sets a regular expression that the value of the property must match.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "regex" field in HAL-FORMS. Clients may use this for validation.
    /// </remarks>
    [JsonPropertyName("regex")]
    public string Regex { get; set; }

    /// <summary>
    /// Gets or sets a list of predefined values that the property may accept.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "options" field in HAL-FORMS. Useful for dropdowns or select inputs.
    /// </remarks>
    [JsonPropertyName("options")]
    public List<string>? Options { get; set; }
}
