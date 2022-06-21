﻿namespace Grunt.Models.HaloInfinite
{
    public class DevicePresetOverrides
    {
        public int Version { get; set; }
        public NvidiaPreset Nvidia { get; set; }
        public AMDPreset AMD { get; set; }
        public IntelPreset Intel { get; set; }
    }
}
