﻿namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable]
    public class Team
    {
        public int TeamId { get; set; }
        public int Outcome { get; set; }
        public int Rank { get; set; }
        public Stats Stats { get; set; }
    }
}
