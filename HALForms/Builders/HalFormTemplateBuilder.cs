using pawd.CoreLibrary.HalForms.Enums;
using pawd.CoreLibrary.HalForms.Models;

namespace pawd.CoreLibrary.HalForms.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="HalFormTemplate"/> instances,
/// including method, content type, target URI, and input properties.
/// </summary>
public sealed class HalFormTemplateBuilder
{
    private readonly HalFormTemplate _template = new();

    /// <summary>
    /// Sets the human-readable title for the template.
    /// </summary>
    /// <param name="title">The template title.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormTemplateBuilder WithTitle(string title)
    {
        _template.Title = title;
        return this;
    }

    /// <summary>
    /// Sets the HTTP method used to submit the template.
    /// </summary>
    /// <param name="method">The HTTP method (e.g., POST, PUT).</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormTemplateBuilder WithMethod(HalFormHttpMethod method)
    {
        _template.Method = method;
        return this;
    }

    /// <summary>
    /// Sets the content type for the request payload.
    /// </summary>
    /// <param name="contentType">The content type (e.g., "application/json").</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormTemplateBuilder WithContentType(string contentType)
    {
        _template.ContentType = contentType;
        return this;
    }

    /// <summary>
    /// Sets the target URI where the form should be submitted.
    /// </summary>
    /// <param name="target">The submission target URI.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormTemplateBuilder WithTarget(string target)
    {
        _template.Target = target;
        return this;
    }

    /// <summary>
    /// Adds a property (field) to the template.
    /// </summary>
    /// <param name="configure">An action to configure the property using <see cref="HalFormPropertyBuilder"/>.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormTemplateBuilder WithProperty(Action<HalFormPropertyBuilder> configure)
    {
        var propertyBuilder = new HalFormPropertyBuilder();
        configure(propertyBuilder);
        _template.Properties.Add(propertyBuilder.Build());
        return this;
    }

    /// <summary>
    /// Builds and returns the configured <see cref="HalFormTemplate"/> instance.
    /// </summary>
    /// <returns>The constructed <see cref="HalFormTemplate"/>.</returns>
    public HalFormTemplate Build() => _template;
}
