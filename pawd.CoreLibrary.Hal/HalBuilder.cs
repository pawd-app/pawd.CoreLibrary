namespace pawd.CoreLibrary.Hal;

/// <summary>
/// Fluent builder for constructing <see cref="HalDocument"/> instances with links, embedded resources, and custom properties.
/// </summary>
public sealed class HalBuilder : HalBuilderBase<HalDocument>
{
    public HalBuilder() : base(new HalDocument()) { }

    public override HalDocument Build()
    {
        ApplyProperties();
        return _document;
    }
}