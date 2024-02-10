﻿// <copyright file="Achievement.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Surprenant.Grunt.Models.HaloInfinite;

[IsAutomaticallySerializable]
public class Achievement
{
    public int Id { get; set; }
    public string XboxLiveId { get; set; }
    public string SteamId { get; set; }
}
