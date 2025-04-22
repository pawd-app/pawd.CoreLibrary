using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using pawd.CoreLibrary.HalForms.Attributes;
using pawd.CoreLibrary.HalForms.Builders;
using pawd.CoreLibrary.HalForms.Enums;

namespace pawd.CoreLibrary.HalForms.Extensions;

/// <summary>
/// Provides extension methods for working with <see cref="HalFormsBuilder"/>.
/// </summary>
public static class HalFormsExtensions
{
    
    /// <summary>
    /// Adds a HAL-FORMS template based on a model type and attaches validation errors from the <see cref="ModelStateDictionary"/>.
    /// </summary>
    /// <typeparam name="T">The type of the model used to generate the template properties.</typeparam>
    /// <param name="builder">The <see cref="HalFormsBuilder"/> instance to extend.</param>
    /// <param name="templateName">The name of the template to be added to the HAL-FORMS document.</param>
    /// <param name="method">The HTTP method (e.g., PUT, POST) to use for the template.</param>
    /// <param name="target">The target URI where the form data will be submitted.</param>
    /// <param name="modelInstance">An instance of the model to extract property values and metadata.</param>
    /// <param name="modelState">
    /// The model state dictionary containing validation errors. 
    /// Errors will be attached under the <c>_errors</c> section of the HAL-FORMS document,
    /// with support for both field-specific and form-level errors (using the <c>__all__</c> key).
    /// </param>
    /// <param name="title">An optional title for the template.</param>
    /// <returns>The updated <see cref="HalFormsBuilder"/> instance for fluent chaining.</returns>

    public static HalFormsBuilder WithTemplateFromModelAndErrors<T>(
        this HalFormsBuilder builder,
        string templateName,
        HalFormHttpMethod method,
        string target,
        T modelInstance,
        ModelStateDictionary modelState,
        string? title = null)
    {
        builder.WithTemplateFromModel(templateName, method, target, modelInstance, title);

        var errors = modelState.ToHalFormErrors();
        return builder.WithErrors(templateName, errors);
    }
    
    /// <summary>
    /// Adds a template to the HAL-FORMS document by reflecting over a model instance
    /// annotated with <see cref="HalFormPropertyAttribute"/>.
    /// </summary>
    /// <typeparam name="T">The type of the model.</typeparam>
    /// <param name="builder">The HAL-FORMS builder instance.</param>
    /// <param name="templateName">The name of the template to add.</param>
    /// <param name="method">The HTTP method to use for the template.</param>
    /// <param name="target">The URI to which the template should be submitted.</param>
    /// <param name="modelInstance">An instance of the model to reflect over for default values and metadata.</param>
    /// <param name="title">An optional title for the template.</param>
    /// <returns>The builder instance for further chaining.</returns>
    public static HalFormsBuilder WithTemplateFromModel<T>(
        this HalFormsBuilder builder,
        string templateName,
        HalFormHttpMethod method,
        string target,
        T modelInstance,
        string? title = null)
    {
        return builder.WithTemplate(templateName, templateBuilder =>
        {
            templateBuilder
                .WithMethod(method)
                .WithTarget(target);

            if (title != null)
                templateBuilder.WithTitle(title);

            foreach (var prop in typeof(T).GetProperties())
            {
                var attribute = prop.GetCustomAttribute<HalFormPropertyAttribute>();
                if (attribute == null) continue;

                var value = prop.GetValue(modelInstance);

                templateBuilder.WithProperty(propBuilder =>
                {
                    propBuilder.WithName(prop.Name);
                
                    if (value != null)
                        propBuilder.WithValue(value);  // Pre-fill the value

                    if (attribute.Prompt != null)
                        propBuilder.WithPrompt(attribute.Prompt);
                    
                    if (attribute.Type != null)
                        propBuilder.WithType(attribute.Type);
                    
                    if (attribute.Required)
                        propBuilder.IsRequired();
                    
                    if (attribute.Options != null)
                        propBuilder.WithOptions(attribute.Options);
                });
            }
        });
    }
}
