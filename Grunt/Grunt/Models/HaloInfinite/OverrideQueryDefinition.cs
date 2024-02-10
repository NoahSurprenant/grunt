﻿// <copyright file="OverrideQueryDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Text.Json.Serialization;

namespace Surprenant.Grunt.Models.HaloInfinite;

[IsAutomaticallySerializable]
public class OverrideQueryDefinition
{
    [JsonPropertyName("schema_version")]
    public int SchemaVersion { get; set; }
    public int Version { get; set; }
    public OverrideQuery[] Overrides { get; set; }
}
