using pawd.CoreLibrary.HalForms.Models;

namespace pawd.CoreLibrary.HalForms.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="HalFormProperty"/> instances.
/// </summary>
public class HalFormPropertyBuilder
{
    private readonly HalFormProperty _property = new();

    /// <summary>
    /// Sets the name of the property.
    /// </summary>
    /// <param name="name">The property name.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder WithName(string name)
    {
        _property.Name = name;
        return this;
    }

    /// <summary>
    /// Sets the data type of the property.
    /// </summary>
    /// <param name="type">The property type (e.g., "text", "number").</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder WithType(string type)
    {
        _property.Type = type;
        return this;
    }

    /// <summary>
    /// Sets the default value of the property.
    /// </summary>
    /// <param name="value">The default value.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder WithValue(object value)
    {
        _property.Value = value;
        return this;
    }

    /// <summary>
    /// Sets the human-readable prompt (label) for the property.
    /// </summary>
    /// <param name="prompt">The prompt text.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder WithPrompt(string prompt)
    {
        _property.Prompt = prompt;
        return this;
    }

    /// <summary>
    /// Specifies whether the property is required.
    /// </summary>
    /// <param name="required">True if required; otherwise, false.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder IsRequired(bool required = true)
    {
        _property.Required = required;
        return this;
    }

    /// <summary>
    /// Specifies whether the property is read-only.
    /// </summary>
    /// <param name="readOnly">True if read-only; otherwise, false.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder IsReadOnly(bool readOnly = true)
    {
        _property.ReadOnly = readOnly;
        return this;
    }

    /// <summary>
    /// Sets a regular expression for validating the property value.
    /// </summary>
    /// <param name="regex">The regex pattern.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder WithRegex(string regex)
    {
        _property.Regex = regex;
        return this;
    }

    /// <summary>
    /// Sets a list of valid options for the property.
    /// </summary>
    /// <param name="options">An array of valid option values.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormPropertyBuilder WithOptions(params string[] options)
    {
        _property.Options = options.ToList();
        return this;
    }

    /// <summary>
    /// Builds and returns the configured <see cref="HalFormProperty"/> instance.
    /// </summary>
    /// <returns>The constructed <see cref="HalFormProperty"/>.</returns>
    public HalFormProperty Build() => _property;
}
