namespace pawd.CoreLibrary.HalForms.Models;

using System.Collections.Generic;
using System.Text.Json.Serialization;

/// <summary>
/// Represents a HAL-FORMS link object, which defines a hyperlink reference used for resource navigation.
/// </summary>
public sealed class HalLink
{
    /// <summary>
    /// Gets or sets the target URI of the link. This is the primary reference to another resource.
    /// </summary>
    /// <remarks>
    /// Corresponds to the "href" property in a HAL-FORMS link object.
    /// </remarks>
    [JsonPropertyName("href")]
    public string Href { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the <see cref="Href"/> value is a URI template.
    /// </summary>
    /// <remarks>
    /// If true, the client may need to substitute variables into the URI. Optional.
    /// Corresponds to the "templated" property in a HAL-FORMS link object.
    /// </remarks>
    [JsonPropertyName("templated")]
    public bool? Templated { get; set; }

    /// <summary>
    /// Gets or sets a human-readable title for the link.
    /// </summary>
    /// <remarks>
    /// This value is primarily intended for documentation or UI display purposes.
    /// Corresponds to the "title" property in a HAL-FORMS link object.
    /// </remarks>
    [JsonPropertyName("title")]
    public string Title { get; set; }
}
