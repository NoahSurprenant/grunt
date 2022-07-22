﻿using System.Collections.Generic;

namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable]
    public class RewardSummary
    {
        public List<RewardTrack> UpdatedRewardTracks { get; set; }
        public List<AwardedReward> AwardedRewards { get; set; }
        public List<GrantedCurrency> GrantedCurrencies { get; set; }
        public List<PlayerItem> GrantedItems { get; set; }
    }

}
