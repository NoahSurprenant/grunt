﻿// <copyright file="VehicleCoreTheme.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using Surprenant.Grunt.Models.HaloInfinite.Foundation;
using System.Collections.Generic;

namespace Surprenant.Grunt.Models.HaloInfinite;

[IsAutomaticallySerializable]
public class VehicleCoreTheme : Theme
{
    public string CoatingPath { get; set; }
    public string HornPath { get; set; }
    public string VehicleFxPath { get; set; }
    public string VehicleCharmPath { get; set; }
    public List<Emblem> Emblems { get; set; }
    public string AlternateGeometryRegionPath { get; set; }
}
