using System.Reflection;

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
    /// Gets or sets the name of a static method that provides options dynamically.
    /// Method should return string[] or IEnumerable<string>.
    /// </summary>
    public string? OptionsProviderMethod { get; set; }

    /// <summary>
    /// Gets or sets the type containing the OptionsProviderMethod (if not in declaring type).
    /// </summary>
    public Type? OptionsProviderType { get; set; }

    /// <summary>
    /// Gets or sets the data type of the property (e.g., "text", "number", "email").
    /// Defaults to "Text".
    /// </summary>
    public string? Type { get; set; } = "Text";

    /// <summary>
    /// Gets the resolved options for this property.
    /// First checks Options, then falls back to OptionsProviderMethod if specified.
    /// </summary>
    public IEnumerable<string> GetResolvedOptions(Type declaringType)
    {
        if (Options != null)
        {
            return Options;
        }

        if (!string.IsNullOrEmpty(OptionsProviderMethod))
        {
            var providerType = OptionsProviderType ?? declaringType;
            var method = providerType.GetMethod(
                OptionsProviderMethod,
                BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
            );

            if (method == null)
            {
                throw new InvalidOperationException(
                    $"Options provider method '{OptionsProviderMethod}' not found in type '{providerType.Name}'");
            }

            var result = method.Invoke(null, null);
            return result switch
            {
                string[] array => array,
                IEnumerable<string> enumerable => enumerable,
                _ => throw new InvalidOperationException(
                    $"Options provider method '{OptionsProviderMethod}' must return string[] or IEnumerable<string>")
            };
        }

        return [];
    }
}