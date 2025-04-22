using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace pawd.CoreLibrary.HalForms.Extensions;

public static class ModelStateExtensions
{
    /// <summary>
    /// Converts a <see cref="ModelStateDictionary"/> to a dictionary of HAL-FORMS-compatible validation errors.
    /// </summary>
    /// <param name="modelState">
    /// The model state dictionary containing field-level and model-level validation errors.
    /// </param>
    /// <returns>
    /// A dictionary where the keys are property names (or <c>__all__</c> for global/form-level errors),
    /// and the values are arrays of error messages associated with each key.
    /// </returns>
    /// <remarks>
    /// This method supports HAL-FORMS conventions for validation feedback by returning a structure
    /// suitable for embedding in the <c>_errors</c> section of a HAL-FORMS document.
    /// </remarks>

    public static Dictionary<string, string[]> ToHalFormErrors(this ModelStateDictionary modelState)
    {
        return modelState
            .Where(kvp => kvp.Value.Errors.Any())
            .ToDictionary(
                kvp => string.IsNullOrWhiteSpace(kvp.Key) ? "__all__" : kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            );
    }
}