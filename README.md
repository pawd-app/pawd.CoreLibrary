# Halforms

A lightweight, strongly-typed, and fluent builder for working with [HAL-FORMS](https://rwcbook.github.io/hal-forms/) in .NET.

Easily generate HAL-FORMS documents using C# models, reflection-based metadata, and a fluent API.

---

## ✨ Features

- ✅ Strongly-typed HTTP method enum using `SmartEnum`
- ✅ Fluent builders for templates, properties, and documents
- ✅ Model-based template generation using attributes
- ✅ JSON (de)serialization ready
- ✅ Easily extensible

---

## 📦 Installation

```bash
dotnet add package <package_name_when_decided>
```

## 🚀 Quick Start

### 1\. Define a Model with Metadata

```
public class RegisterUser
{
    [HalFormProperty(Prompt = "Username", Required = true)]
    public string Username { get; set; }

    [HalFormProperty(Prompt = "Email", Type = "email", Required = true)]
    public string Email { get; set; }

    [HalFormProperty(Prompt = "Role", Options = new[] { "User", "Admin" })]
    public string Role { get; set; }
}
```

### 2\. Build a HAL-FORMS Document

```
var model = new RegisterUser
{
    Username = "testuser",
    Email = "test@example.com",
    Role = "User"
};

var halForms = new HalFormsBuilder()
    .WithLink("self", "/users")
    .WithTemplateFromModel("register", HalFormHttpMethod.POST, "/users", model, title: "Register New User")
    .Build();

var json = JsonSerializer.Serialize(halForms, new JsonSerializerOptions { WriteIndented = true });
Console.WriteLine(json);
```

---

## 🔧 Attribute Reference

```
[AttributeUsage(AttributeTargets.Property)]
public class HalFormPropertyAttribute : Attribute
{
    public string? Prompt { get; set; }           // Label for UI
    public bool Required { get; set; }            // Whether field is required
    public string[]? Options { get; set; }        // Selectable options
    public string? Type { get; set; } = "Text";   // Field type (e.g., "text", "email")
}
```

---

## 🧱 Fluent API Overview

### HalFormsBuilder

```
new HalFormsBuilder()
    .WithLink("self", "/users")
    .WithTemplate("template-name", template => { ... })
    .WithEmbedded("rel", new { ... })
    .WithProperty("customField", "value")
    .Build();
```

### HalFormTemplateBuilder

```
templateBuilder
    .WithTitle("Create")
    .WithMethod(HalFormHttpMethod.POST)
    .WithTarget("/api/resource")
    .WithProperty(property => { ... });
```

### HalFormPropertyBuilder

```
propertyBuilder
    .WithName("Email")
    .WithType("email")
    .WithPrompt("Email Address")
    .IsRequired()
    .WithOptions("Admin", "User");
```

---

## 📚 Example Output

```
{
  "_links": {
    "self": { "href": "/users" }
  },
  "_templates": {
    "register": {
      "title": "Register New User",
      "method": "POST",
      "target": "/users",
      "contentType": null,
      "properties": [
        { "name": "Username", "type": "Text", "prompt": "Username", "required": true },
        { "name": "Email", "type": "email", "prompt": "Email", "required": true },
        { "name": "Role", "type": "Text", "prompt": "Role", "options": ["User", "Admin"] }
      ]
    }
  }
}
```

---

## 💡 Planned Improvements

- Property ordering support via attribute
- Custom JSON converters
- More metadata options (e.g., Regex, ReadOnly)


# Logging

TODO