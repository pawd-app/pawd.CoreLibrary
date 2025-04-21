using Ardalis.SmartEnum;

namespace pawd.CoreLibrary.HalForms.Enums;

/// <summary>
/// Represents an HTTP method used in a HAL-FORMS template submission,
/// implemented as a smart enum for type safety and flexibility.
/// </summary>
public sealed class HalFormHttpMethod : SmartEnum<HalFormHttpMethod, string>
{
    /// <summary>
    /// The HTTP GET method, typically used for retrieval operations.
    /// </summary>
    public static readonly HalFormHttpMethod GET = new(nameof(GET), nameof(GET));

    /// <summary>
    /// The HTTP DELETE method, used to remove a resource.
    /// </summary>
    public static readonly HalFormHttpMethod DELETE = new(nameof(DELETE), nameof(DELETE));

    /// <summary>
    /// The HTTP PATCH method, used for partial updates to a resource.
    /// </summary>
    public static readonly HalFormHttpMethod PATCH = new(nameof(PATCH), nameof(PATCH));

    /// <summary>
    /// The HTTP PUT method, used to completely replace a resource.
    /// </summary>
    public static readonly HalFormHttpMethod PUT = new(nameof(PUT), nameof(PUT));

    /// <summary>
    /// The HTTP POST method, used to create a new resource or perform an action.
    /// </summary>
    public static readonly HalFormHttpMethod POST = new(nameof(POST), nameof(POST));

    /// <summary>
    /// Initializes a new instance of the <see cref="HalFormHttpMethod"/> class.
    /// </summary>
    /// <param name="value">The string value of the HTTP method.</param>
    /// <param name="name">The name of the HTTP method.</param>
    private HalFormHttpMethod(string value, string name) : base(name, value) { }
}
