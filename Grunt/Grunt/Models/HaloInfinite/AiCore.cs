// <copyright file="AiCore.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Surprenant.Grunt.Models.HaloInfinite;

/// <summary>
/// AI Core available in Halo Infinite.
/// </summary>
[IsAutomaticallySerializable]
public class AiCore : Foundation.Core
{
    /// <summary>
    /// Gets or sets a list of themes associated with an AI Core.
    /// </summary>
    public List<AiCoreTheme>? Themes { get; set; }
}
