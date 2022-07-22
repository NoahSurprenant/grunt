﻿namespace Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable(IsReady = true)]
    public class ManifestCustomData
    {
        public string BranchName { get; set; }
        public string BuildNumber { get; set; }
        public int Kind { get; set; }
        public string ContentVersion { get; set; }
        public string BuildGuid { get; set; }
        public int Visibility { get; set; }
    }
}
