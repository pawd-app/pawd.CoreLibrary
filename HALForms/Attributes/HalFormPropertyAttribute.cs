namespace pawd.CoreLibrary.HalForms.Attributes;

/// <summary>
/// Specifies metadata for a property to be included in a HAL-FORMS template.
/// Apply this attribute to model properties to configure their behavior in a HAL-FORMS form.
/// </summary>
[AttributeUsage(AttributeTargets.Property)]
public class HalFormPropertyAttribute : Attribute
{
    /// <summary>
    /// Gets or sets the human-readable prompt (label) for the property.
    /// </summary>
    public string? Prompt { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether the property is required.
    /// </summary>
    public bool Required { get; set; }

    /// <summary>
    /// Gets or sets the allowed options for the property.
    /// If set, the property will be treated as a selectable field.
    /// </summary>
    public string[]? Options { get; set; }

    /// <summary>
    /// Gets or sets the data type of the property (e.g., "text", "number", "email").
    /// Defaults to "Text".
    /// </summary>
    public string? Type { get; set; } = "Text";
}
