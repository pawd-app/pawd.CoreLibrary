using pawd.CoreLibrary.Hal;
using pawd.CoreLibrary.HalForms.Models;

namespace pawd.CoreLibrary.HalForms.Builders;

/// <summary>
/// Provides a fluent builder for constructing <see cref="HalFormsDocument"/> instances,
/// including links, templates, embedded resources, and additional properties.
/// </summary>
public class HalFormsBuilder : HalBuilderBase<HalFormsDocument>
{
    private readonly Dictionary<string, Dictionary<string, string[]>> _errors = new();

    public HalFormsBuilder() : base(new HalFormsDocument()) { }

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
        Document.Templates[name] = templateBuilder.Build();
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
    public override HalFormsDocument Build()
    {
        foreach (var prop in RootProperties)
        {
            Document.Properties[prop.Key] = prop.Value;
        }
        
        if (_errors.Count != 0)
        {
            Document.Properties["_errors"] = _errors;
        }
        
        return Document;
    }
}
