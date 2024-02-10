﻿// <copyright file="InventoryDefinition.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

using System.Collections.Generic;

namespace Surprenant.Grunt.Models.HaloInfinite;

[IsAutomaticallySerializable]
public class InventoryDefinition
{
    public PlayerItem[] Items { get; set; }
    public int ArmorChestAttachmentsOwnableCount { get; set; }
    public int ArmorCoatingsOwnableCount { get; set; }
    public int ArmorGlovesOwnableCount { get; set; }
    public int ArmorHelmetAttachmentsOwnableCount { get; set; }
    public int ArmorHelmetsOwnableCount { get; set; }
    public int ArmorKneePadsOwnableCount { get; set; }
    public int ArmorThemesOwnableCount { get; set; }
    public int ArmorLeftShoulderPadsOwnableCount { get; set; }
    public int ArmorRightShoulderPadsOwnableCount { get; set; }
    public int ArmorHipAttachmentsOwnableCount { get; set; }
    public int ArmorWristAttachmentsOwnableCount { get; set; }
    public int ArmorVisorsOwnableCount { get; set; }
    public int ArmorEmblemsOwnableCount { get; set; }
    public int ArmorFxsOwnableCount { get; set; }
    public int ArmorMythicFxsOwnableCount { get; set; }
    public int ArmorCoresOwnableCount { get; set; }
    public int SpartanActionPosesOwnableCount { get; set; }
    public int SpartanStancesOwnableCount { get; set; }
    public int SpartanBackdropImagesOwnableCount { get; set; }
    public int SpartanEmblemsOwnableCount { get; set; }
    public int WeaponCharmsOwnableCount { get; set; }
    public int WeaponAmmoCounterColorsOwnableCount { get; set; }
    public int WeaponCoatingsOwnableCount { get; set; }
    public int WeaponDeathFxsOwnableCount { get; set; }
    public int WeaponEmblemsOwnableCount { get; set; }
    public int WeaponThemesOwnableCount { get; set; }
    public int WeaponStatTrackersOwnableCount { get; set; }
    public int WeaponCoresOwnableCount { get; set; }
    public int WeaponAlternateGeometryRegionsOwnableCount { get; set; }
    public int AiColorsOwnableCount { get; set; }
    public int AiModelsOwnableCount { get; set; }
    public int AiThemesOwnableCount { get; set; }
    public int AiCoresOwnableCount { get; set; }
    public int VehicleAlternateGeometryRegionsOwnableCount { get; set; }
    public int VehicleCharmsOwnableCount { get; set; }
    public int VehicleCoatingsOwnableCount { get; set; }
    public int VehicleEmblemsOwnableCount { get; set; }
    public int VehicleFxsOwnableCount { get; set; }
    public int VehicleHornsOwnableCount { get; set; }
    public int VehicleThemesOwnableCount { get; set; }
    public int VehicleCoresOwnableCount { get; set; }
    public List<PlayerItem> Cores { get; set; }
    public int SpartanVoicesOwnableCount { get; set; }
    public List<PlayerItem> EmblemCoatings { get; set; }
    public List<PlayerItem> Consumables { get; set; }
}
