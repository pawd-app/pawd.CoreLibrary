using pawd.CoreLibrary.HalForms.Models;

namespace pawd.CoreLibrary.HalForms.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="HalFormsDocument"/> instances,
/// including links, templates, embedded resources, and additional properties.
/// </summary>
public class HalFormsBuilder
{
    private readonly HalFormsDocument _document = new();
    
    private readonly Dictionary<string, object> _rootProperties = new();
    
    private readonly Dictionary<string, Dictionary<string, string[]>> _errors = new();

    /// <summary>
    /// Adds a custom property to the root of the HAL-FORMS document.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormsBuilder WithProperty(string name, object value)
    {
        _rootProperties[name] = value;
        return this;
    }

    /// <summary>
    /// Adds a link to the HAL-FORMS document.
    /// </summary>
    /// <param name="rel">The link relation (rel).</param>
    /// <param name="href">The hyperlink reference (href).</param>
    /// <param name="title">An optional human-readable title for the link.</param>
    /// <param name="templated">Indicates if the href is a URI template.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormsBuilder WithLink(string rel, string href, string? title = null, bool? templated = null)
    {
        _document.Links[rel] = new HalLink { Href = href, Title = title!, Templated = templated };
        return this;
    }

    /// <summary>
    /// Adds a named template to the HAL-FORMS document.
    /// </summary>
    /// <param name="name">The name of the template.</param>
    /// <param name="configure">An action to configure the template using <see cref="HalFormTemplateBuilder"/>.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormsBuilder WithTemplate(string name, Action<HalFormTemplateBuilder> configure)
    {
        var templateBuilder = new HalFormTemplateBuilder();
        configure(templateBuilder);
        _document.Templates[name] = templateBuilder.Build();
        return this;
    }

    /// <summary>
    /// Adds an embedded resource to the HAL-FORMS document.
    /// </summary>
    /// <param name="rel">The relation name (rel) for the embedded resource.</param>
    /// <param name="resource">The resource to embed.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormsBuilder WithEmbedded(string rel, object resource)
    {
        _document.Embedded[rel] = resource;
        return this;
    }
    
    /// <summary>
    /// Adds any errors to the HAL-FORMS document.
    /// </summary>
    /// <param name="templateName">The template name the errors apply to</param>
    /// <param name="errors">The errors associated with this template.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalFormsBuilder WithErrors(string templateName, Dictionary<string, string[]> errors)
    {
        _errors[templateName] = errors;
        return this;
    }

    /// <summary>
    /// Finalizes and builds the configured <see cref="HalFormsDocument"/> instance.
    /// </summary>
    /// <returns>The constructed <see cref="HalFormsDocument"/>.</returns>
    public HalFormsDocument Build()
    {
        foreach (var prop in _rootProperties)
        {
            _document.Properties[prop.Key] = prop.Value;
        }
        
        if (_errors.Count != 0)
        {
            _document.Properties["_errors"] = _errors;
        }
        
        return _document;
    }
}

