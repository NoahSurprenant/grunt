﻿// <copyright file="AuthoringAssetVersion.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Surprenant.Grunt.Models.HaloInfinite;

[IsAutomaticallySerializable]
public class AuthoringAssetVersion
{
    public AuthoringAssetCustomData CustomData { get; set; }
    public AssetVersionFile AssetVersionFiles { get; set; }
    public Dictionary<string, List<TargetAsset>> Links { get; set; }
    public string AssetId { get; set; }
    public string AssetVersionId { get; set; }
    public string PublicName { get; set; }
    public string Description { get; set; }
    public APIFormattedDate CreatedDate { get; set; }
    public APIFormattedDate LastModifiedDate { get; set; }
    public int VersionNumber { get; set; }
    public string Note { get; set; }
    public int AssetState { get; set; }
    public List<string> Tags { get; set; }
    public List<string> Contributors { get; set; }
    public int AssetHome { get; set; }
    public bool ExemptFromAutoDelete { get; set; }
    public int InspectionResult { get; set; }
    public int CloneBehavior { get; set; }
    public string Player { get; set; }
    public string StringCulture { get; set; }
    public string? PreviousAssetVersionId { get; set; }
}
