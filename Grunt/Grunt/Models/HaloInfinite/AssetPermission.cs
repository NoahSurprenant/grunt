﻿namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable]
    public class AssetPermission
    {
        public string CanonicalToken { get; set; }
        public int AuthoringRole { get; set; }
        public string GrantedBy { get; set; }
        public APIFormattedDate GrantedOnDateUtc { get; set; }
    }
}
