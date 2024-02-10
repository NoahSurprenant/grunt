﻿// <copyright file="Manifest.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Surprenant.Grunt.Models.HaloInfinite.Foundation;

namespace Surprenant.Grunt.Models.HaloInfinite;

[IsAutomaticallySerializable]
public class Manifest : Asset
{
    public ManifestCustomData CustomData { get; set; }
    public Map[] MapLinks { get; set; }
    public UGCGameVariant[] UgcGameVariantLinks { get; set; }
    public object[] PlaylistLinks { get; set; }
    public EngineGameVariant[] EngineGameVariantLinks { get; set; }
    public PlayAssetStats AssetStats { get; set; }
}
