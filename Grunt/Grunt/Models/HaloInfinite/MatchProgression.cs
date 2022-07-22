﻿namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable]
    public class MatchProgression
    {
        public string ClearanceId { get; set; }
        public string RewardId { get; set; }
        public ChallengeProgressState[] ChallengeProgressState { get; set; }
    }
}
