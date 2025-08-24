// <copyright file="GenerateInterfaceAttribute.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace Surprenant.Grunt.Attributes;

/// <summary>
/// Indicates that an interface should be automatically generated for the marked class.
/// Classes marked with this attribute will have a corresponding interface generated 
/// that includes all public methods and properties.
/// </summary>
[AttributeUsage(AttributeTargets.Class)]
public class GenerateInterfaceAttribute : Attribute
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateInterfaceAttribute"/> class.
    /// </summary>
    public GenerateInterfaceAttribute()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GenerateInterfaceAttribute"/> class.
    /// </summary>
    /// <param name="interfaceName">The name of the interface to generate. If null, defaults to I{ClassName}.</param>
    public GenerateInterfaceAttribute(string? interfaceName)
    {
        InterfaceName = interfaceName;
    }

    /// <summary>
    /// Gets the name of the interface to generate.
    /// </summary>
    public string? InterfaceName { get; }
}