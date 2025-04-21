using System.Text.Json.Serialization;
using pawd.CoreLibrary.HalForms.Enums;

namespace pawd.CoreLibrary.HalForms.Models;

/// <summary>
/// Represents a HAL-FORMS template, describing a set of input fields and request metadata for client-side interactions.
/// </summary>
public sealed class HalFormTemplate
{
    /// <summary>
    /// Gets or sets the human-readable title of the template.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "title" field in HAL-FORMS. Useful for display purposes or documentation.
    /// </remarks>
    [JsonPropertyName("title")]
    public string Title { get; set; }

    /// <summary>
    /// Gets or sets the HTTP method to be used when submitting the template.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "method" field in HAL-FORMS. Common values include "GET", "POST", "PUT", and "DELETE".
    /// </remarks>
    [JsonPropertyName("method")]
    public HalFormHttpMethod Method { get; set; }

    /// <summary>
    /// Gets or sets the content type of the request payload.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "contentType" field in HAL-FORMS. Typically "application/json" or "application/x-www-form-urlencoded".
    /// </remarks>
    [JsonPropertyName("contentType")]
    public string ContentType { get; set; }

    /// <summary>
    /// Gets or sets the URI to which the form should be submitted.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "target" field in HAL-FORMS. If omitted, the current resource URI is used.
    /// </remarks>
    [JsonPropertyName("target")]
    public string Target { get; set; }

    /// <summary>
    /// Gets or sets the collection of input fields defined by this template.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "properties" field in HAL-FORMS. Each property describes a single input parameter.
    /// </remarks>
    [JsonPropertyName("properties")]
    public List<HalFormProperty> Properties { get; set; } = new();
}
