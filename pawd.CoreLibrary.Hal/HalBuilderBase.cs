using pawd.CoreLibrary.Hal.Models;

namespace pawd.CoreLibrary.Hal;

public abstract class HalBuilderBase<T> where T : class
{
    protected readonly T _document;
    private readonly Dictionary<string, object> _properties = new();

    protected HalBuilderBase(T document)
    {
        _document = document;
    }

    // Common HAL methods
    public HalBuilderBase<T> WithLink(string rel, string href, string? title = null, bool templated = false)
    {
        if (_document is IHasLinks hasLinks)
        {
            hasLinks.Links[rel] = new HalLink { Href = href, Title = title, Templated = templated };
        }
        return this;
    }

    public HalBuilderBase<T> WithEmbedded(string rel, object resource)
    {
        if (_document is IHasEmbedded hasEmbedded)
        {
            hasEmbedded.Embedded[rel] = resource;
        }
        return this;
    }

    public HalBuilderBase<T> WithProperty(string name, object value)
    {
        _properties[name] = value;
        return this;
    }

    protected void ApplyProperties()
    {
        if (_document is IHasProperties hasProps)
        {
            foreach (var prop in _properties)
            {
                hasProps.Properties[prop.Key] = prop.Value;
            }
        }
    }
}

public interface IHasLinks { Dictionary<string, HalLink> Links { get; } }
public interface IHasEmbedded { Dictionary<string, object> Embedded { get; } }
public interface IHasProperties { Dictionary<string, object> Properties { get; } }