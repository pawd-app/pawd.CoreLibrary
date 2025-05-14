using pawd.CoreLibrary.Hal.Models;

namespace pawd.CoreLibrary.Hal;

public abstract class HalBuilderBase<T> where T : HalDocument
{
    protected readonly T Document;

    protected readonly Dictionary<string, object> RootProperties = new();

    public abstract T Build();

    protected HalBuilderBase(T document)
    {
        Document = document;
    }

    /// <summary>
    /// Adds a link to the HAL document.
    /// </summary>
    /// <param name="rel">The link relation (rel).</param>
    /// <param name="href">The hyperlink reference (href).</param>
    /// <param name="title">An optional human-readable title for the link.</param>
    /// <param name="templated">Indicates if the href is a URI template.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalBuilderBase<T> WithLink(string rel, string href, string? title = null, bool templated = false)
    {
        Document.Links[rel] = new HalLink { Href = href, Title = title!, Templated = templated };
        return this;
    }

    
    /// <summary>
    /// Adds an embedded resource to the HAL document.
    /// </summary>
    /// <param name="rel">The relation name (rel) for the embedded resource.</param>
    /// <param name="resource">The resource to embed.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalBuilderBase<T> WithEmbedded(string rel, object resource)
    {
        Document.Embedded[rel] = resource;
        return this;
    }

    /// <summary>
    /// Adds a custom property to the root of the HAL-FORMS document.
    /// </summary>
    /// <param name="name">The name of the property.</param>
    /// <param name="value">The value of the property.</param>
    /// <returns>The builder instance for chaining.</returns>
    public HalBuilderBase<T> WithProperty(string name, object value)
    {
        RootProperties[name] = value;
        return this;
    }
}