namespace pawd.CoreLibrary.Hal;

/// <summary>
/// Fluent builder for constructing <see cref="HalDocument"/> instances with links, embedded resources, and custom properties.
/// </summary>
public sealed class HalBuilder : HalBuilderBase<HalDocument>
{
    public HalBuilder() : base(new HalDocument()) { }

    /// <summary>
    /// Finalizes and builds the configured <see cref="HalDocument"/> instance.
    /// </summary>
    /// <returns>The constructed <see cref="HalDocument"/>.</returns>
    public override HalDocument Build()
    {
        foreach (var prop in RootProperties)
        {
            Document.Properties[prop.Key] = prop.Value;
        }
        
        return Document;
    }
}