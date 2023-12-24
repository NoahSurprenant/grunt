﻿// <copyright file="ApiSettingsContainer.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite.ApiIngress
{
    [IsAutomaticallySerializable]
    public class ApiSettingsContainer
    {
        public Dictionary<string, ApiAuthority> Authorities { get; set; }
        public Dictionary<string, ApiRetryPolicy> RetryPolicies { get; set; }
        public ApiSettings Settings { get; set; }
        public Dictionary<string, ApiEndpoint> Endpoints { get; set; }
    }
}
