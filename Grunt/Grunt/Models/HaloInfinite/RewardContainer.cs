﻿namespace OpenSpartan.Grunt.Models.HaloInfinite
{
    [IsAutomaticallySerializable]
    public class RewardContainer
    {
        public InventoryAmount[] InventoryRewards { get; set; }
        public CurrencyAmount[] CurrencyRewards { get; set; }
    }
}
