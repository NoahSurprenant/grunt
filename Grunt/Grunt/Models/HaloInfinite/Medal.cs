﻿// <copyright file="Medal.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

namespace Surprenant.Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable]
    public class Medal
    {
        public long NameId { get; set; }
        public int Count { get; set; }
        public int TotalPersonalScoreAwarded { get; set; }
    }
}
