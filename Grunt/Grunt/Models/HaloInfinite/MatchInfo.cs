// <copyright file="MatchInfo.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System;

namespace Surprenant.Grunt.Models.HaloInfinite;

/// <summary>
/// Container for general match information.
/// </summary>
[IsAutomaticallySerializable]
public class MatchInfo
{
    public DateTimeOffset StartTime { get; set; }
    public DateTimeOffset EndTime { get; set; }
    public TimeSpan Duration { get; set; }
    public int LifecycleMode { get; set; }
    public GameVariantCategory? GameVariantCategory { get; set; }
    public Guid LevelId { get; set; }
    public GenericAsset MapVariant { get; set; }
    public UGCGameVariant UgcGameVariant { get; set; }
    public Guid ClearanceId { get; set; }
    public GenericAsset? Playlist { get; set; }
    public PlaylistExperience? PlaylistExperience { get; set; }
    public GenericAsset? PlaylistMapModePair { get; set; }
    public string? SeasonId { get; set; }
    public TimeSpan PlayableDuration { get; set; }
    public bool TeamsEnabled { get; set; }
    public bool TeamScoringEnabled { get; set; }
}
