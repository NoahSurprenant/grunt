// <copyright file="IHaloInfiniteClient.cs" company="Den Delimarsky">
// Developed by Den Delimarsky.
// Den Delimarsky licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.
// The underlying API powering Grunt is managed by 343 Industries and Microsoft. This wrapper is not endorsed by 343 Industries or Microsoft.
// </copyright>

// THIS FILE IS AUTO-GENERATED. DO NOT EDIT MANUALLY.
// Generated from HaloInfiniteClient.cs
// Any changes to public methods in HaloInfiniteClient will need to be reflected here.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Surprenant.Grunt.Models;
using Surprenant.Grunt.Models.HaloInfinite;
using Surprenant.Grunt.Models.HaloInfinite.ApiIngress;
using Surprenant.Grunt.Models.HaloInfinite.Medals;

namespace Surprenant.Grunt.Core;

/// <summary>
/// Interface for client used to access the Halo Infinite API surface.
/// This interface is automatically generated from the HaloInfiniteClient class.
/// </summary>
public interface IHaloInfiniteClient
{
    public string SpartanToken { get; set; } { get; set; }
    public string Xuid { get; set; } { get; set; }
    public string ClearanceToken { get; set; } { get; set; }
    Task<HaloApiResultContainer<ApiSettingsContainer, HaloApiErrorContainer>> GetApiSettingsContainer();
    Task<HaloApiResultContainer<BotCustomizationData, HaloApiErrorContainer>> AcademyGetBotCustomization(string flightId);
    Task<HaloApiResultContainer<AcademyClientManifest, HaloApiErrorContainer>> AcademyGetContent();
    Task<HaloApiResultContainer<TestAcademyClientManifest, HaloApiErrorContainer>> AcademyGetContentTest(string clearanceId);
    Task<HaloApiResultContainer<AcademyStarDefinitions, HaloApiErrorContainer>> AcademyGetStarDefinitions();
    Task<HaloApiResultContainer<AiCore, HaloApiErrorContainer>> EconomyAiCoreCustomization(string player, string coreId);
    Task<HaloApiResultContainer<AiCoreContainer, HaloApiErrorContainer>> EconomyAiCoresCustomization(string player);
    Task<HaloApiResultContainer<PlayerCores, HaloApiErrorContainer>> EconomyAllOwnedCoresDetails(string player);
    Task<HaloApiResultContainer<ArmorCore, HaloApiErrorContainer>> EconomyArmorCoreCustomization(string player, string coreId);
    Task<HaloApiResultContainer<ArmorCoreCollection, HaloApiErrorContainer>> EconomyArmorCoresCustomization(string player);
    Task<HaloApiResultContainer<ActiveBoostsContainer, HaloApiErrorContainer>> EconomyGetActiveBoosts(string player);
    Task<HaloApiResultContainer<RewardSnapshot, HaloApiErrorContainer>> EconomyGetAwardedRewards(string player, string rewardId);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyGetBoostsStore(string player);
    Task<HaloApiResultContainer<PlayerGiveaways, HaloApiErrorContainer>> EconomyGetGiveawayRewards(string player);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyGetHCSStore(string player);
    Task<HaloApiResultContainer<PlayerInventory, HaloApiErrorContainer>> EconomyGetInventoryItems(string player);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyGetMainStore(string player);
    Task<HaloApiResultContainer<PlayerCustomizationCollection, HaloApiErrorContainer>> EconomyGetMultiplePlayersCustomization(List<string> playerIds);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyGetOperationRewardLevelsStore(string player);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyGetOperationsStore(string player);
    Task<HaloApiResultContainer<RewardTrackMetadata, HaloApiErrorContainer>> EconomyGetRewardTrack(string player, string rewardTrackType, string trackId);
    Task<HaloApiResultContainer<CurrencySnapshot, HaloApiErrorContainer>> EconomyGetVirtualCurrencyBalances(string player);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyGetXpGrantsStore(string player);
    Task<HaloApiResultContainer<GenericCore, HaloApiErrorContainer>> EconomyOwnedCoreDetails(string player, string coreId);
    Task<HaloApiResultContainer<AppearanceCustomization, HaloApiErrorContainer>> EconomyPlayerAppearanceCustomization(string player);
    Task<HaloApiResultContainer<CustomizationData, HaloApiErrorContainer>> EconomyPlayerCustomization(string player, string viewType);
    Task<HaloApiResultContainer<OperationRewardTrackSnapshot, HaloApiErrorContainer>> EconomyPlayerOperations(string player);
    Task<HaloApiResultContainer<TransactionSnapshot, HaloApiErrorContainer>> EconomyPostCurrencyTransaction(string player, string currencyId);
    Task<HaloApiResultContainer<StoreItem, HaloApiErrorContainer>> EconomyScheduledStorefrontOfferings(string player, string storeId);
    Task<HaloApiResultContainer<SpartanBody, HaloApiErrorContainer>> EconomySpartanBodyCustomization(string player);
    Task<HaloApiResultContainer<VehicleCore, HaloApiErrorContainer>> EconomyVehicleCoreCustomization(string player, string coreId);
    Task<HaloApiResultContainer<VehicleCoreCollection, HaloApiErrorContainer>> EconomyVehicleCoresCustomization(string player);
    Task<HaloApiResultContainer<WeaponCore, HaloApiErrorContainer>> EconomyWeaponCoreCustomization(string player, string coreId);
    Task<HaloApiResultContainer<WeaponCoreCollection, HaloApiErrorContainer>> EconomyWeaponCoresCustomization(string player);
    Task<HaloApiResultContainer<AchievementCollection, HaloApiErrorContainer>> GameCmsGetAchievements();
    Task<HaloApiResultContainer<AsyncComputeOverrides, HaloApiErrorContainer>> GameCmsGetAsyncComputeOverrides();
    Task<HaloApiResultContainer<Challenge, HaloApiErrorContainer>> GameCmsGetChallenge(string challengePath, string flightId);
    Task<HaloApiResultContainer<ChallengeDeckDefinition, HaloApiErrorContainer>> GameCmsGetChallengeDeck(string challengeDeckPath, string flightId);
    Task<HaloApiResultContainer<CurrencyDefinition, HaloApiErrorContainer>> GameCmsGetCurrency(string currencyPath, string flightId);
    Task<HaloApiResultContainer<ClawAccessSnapshot, HaloApiErrorContainer>> GameCmsGetClawAccess(string flightId);
    Task<HaloApiResultContainer<CPUPresetSnapshot, HaloApiErrorContainer>> GameCmsGetCpuPresets();
    Task<HaloApiResultContainer<CustomGameDefinition, HaloApiErrorContainer>> GameCmsGetCustomGameDefaults();
    Task<HaloApiResultContainer<InventoryDefinition, HaloApiErrorContainer>> GameCmsGetCustomizationCatalog(string flightId);
    Task<HaloApiResultContainer<DevicePresetOverrides, HaloApiErrorContainer>> GameCmsGetDevicePresetOverrides();
    Task<HaloApiResultContainer<RewardTrackMetadata, HaloApiErrorContainer>> GameCmsGetEvent(string eventPath, string flightId);
    Task<HaloApiResultContainer<OverrideQueryDefinition, HaloApiErrorContainer>> GameCmsGetGraphicsSpecControlOverrides();
    Task<HaloApiResultContainer<string, HaloApiErrorContainer>> GameCmsGetGraphicSpecs();
    Task<HaloApiResultContainer<byte[], HaloApiErrorContainer>> GameCmsGetImage(string filePath);
    Task<HaloApiResultContainer<byte[], HaloApiErrorContainer>> GameCmsWaypointGetImage(string filePath);
    Task<HaloApiResultContainer<InGameItem, HaloApiErrorContainer>> GameCmsGetItem(string itemPath, string flightId);
    Task<HaloApiResultContainer<LobbyHopperErrorMessageList, HaloApiErrorContainer>> GameCmsGetLobbyErrorMessages(string flightId);
    Task<HaloApiResultContainer<Metadata, HaloApiErrorContainer>> GameCmsGetMetadata(string flightId);
    Task<HaloApiResultContainer<NetworkConfiguration, HaloApiErrorContainer>> GameCmsGetNetworkConfiguration(string flightId);
    Task<HaloApiResultContainer<News, HaloApiErrorContainer>> GameCmsGetNews(string filePath);
    Task<HaloApiResultContainer<OEConfiguration, HaloApiErrorContainer>> GameCmsGetNotAllowedInTitleMessage();
    Task<HaloApiResultContainer<T, HaloApiErrorContainer>> GameCmsGetProgressionFile<T>(string filePath);
    Task<HaloApiResultContainer<DriverManifest, HaloApiErrorContainer>> GameCmsGetRecommendedDrivers();
    Task<HaloApiResultContainer<SeasonRewardTrack, HaloApiErrorContainer>> GameCmsGetSeasonRewardTrack(string seasonPath, string flightId);
    Task<HaloApiResultContainer<GuideContainer, HaloApiErrorContainer>> GameCmsGetGuideImages(string flightId);
    Task<HaloApiResultContainer<GuideContainer, HaloApiErrorContainer>> GameCmsGetGuideMultiplayer(string flightId);
    Task<HaloApiResultContainer<GuideContainer, HaloApiErrorContainer>> GameCmsGetGuideNews(string flightId);
    Task<HaloApiResultContainer<GuideContainer, HaloApiErrorContainer>> GameCmsGetGuideProgression(string flightId);
    Task<HaloApiResultContainer<GuideContainer, HaloApiErrorContainer>> GameCmsGetGuideSpecs(string flightId);
    Task<HaloApiResultContainer<GuideContainer, HaloApiErrorContainer>> GameCmsGetGuideTitleAuthorization(string flightId);
    Task<HaloApiResultContainer<FavoriteAsset, HaloApiErrorContainer>> HIUGCCheckAssetPlayerBookmark(string title, string player, string assetType, string assetId);
    Task<HaloApiResultContainer<AuthoringAssetVersion, HaloApiErrorContainer>> HIUGCCreateAssetVersionAgnostic(string title, string assetType, string assetId, AuthoringSessionSourceStarter starter);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCDeleteAllVersions(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCDeleteAsset(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCDeleteVersion(string title, string assetType, string assetId, string versionId);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCEndSession(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<FavoriteAsset, HaloApiErrorContainer>> HIUGCFavoriteAnAsset(string player, string assetType, string assetId);
    Task<HaloApiResultContainer<AuthoringAsset, HaloApiErrorContainer>> HIUGCGetAsset(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<byte[], HaloApiErrorContainer>> HIUGCGetBlob(string blobPath);
    Task<HaloApiResultContainer<AuthoringAssetVersion, HaloApiErrorContainer>> HIUGCGetLatestAssetVersionFilm(string title, string assetId);
    Task<HaloApiResultContainer<AuthoringAssetVersion, HaloApiErrorContainer>> HIUGCGetLatestAssetVersionAgnostic(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<AuthoringAssetVersion, HaloApiErrorContainer>> HIUGCGetPublishedVersion(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<AuthoringAssetVersion, HaloApiErrorContainer>> HIUGCGetSpecificAssetVersion(string title, string assetType, string assetId, string versionId);
    Task<HaloApiResultContainer<AuthoringAssetVersionContainer, HaloApiErrorContainer>> HIUGCListAllVersions(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<AuthoringAssetContainer, HaloApiErrorContainer>> HIUGCListPlayerAssets(string title, string player, int start, int count, bool includeTimes, string sort, ResultOrder order, List<string> keywords, AssetKind kind);
    Task<HaloApiResultContainer<AuthoringFavoritesContainer, HaloApiErrorContainer>> HIUGCListPlayerFavorites(string player, string assetType);
    Task<HaloApiResultContainer<AuthoringFavoritesContainer, HaloApiErrorContainer>> HIUGCListPlayerFavoritesAgnostic(string player);
    Task<HaloApiResultContainer<AuthoringAssetVersion, HaloApiErrorContainer>> HIUGCPatchAssetVersion(string title, string assetType, string assetId, string versionId, AuthoringAssetVersion patchedAsset);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCPublishAssetVersion(string assetType, string assetId, string versionId, string clearanceId);
    Task<HaloApiResultContainer<AuthoringAssetRating, HaloApiErrorContainer>> HIUGCGetAssetRatings(string player, string assetType, string assetId);
    Task<HaloApiResultContainer<AuthoringAssetRating, HaloApiErrorContainer>> HIUGCRateAnAsset(string player, string assetType, string assetId, AuthoringAssetRating rating);
    Task<HaloApiResultContainer<AssetReport, HaloApiErrorContainer>> HIUGCReportAnAsset(string player, string assetType, string assetId, AssetReport report);
    Task<HaloApiResultContainer<AuthoringAsset, HaloApiErrorContainer>> HIUGCSpawnAsset(string title, string assetType, object? asset = null, ApiContentType contentType = ApiContentType.BondCompactBinary);
    Task<HaloApiResultContainer<AssetAuthoringSession, HaloApiErrorContainer>> HIUGCStartSessionAgnostic(string title, string assetType, string assetId, bool includeContainerSas, AuthoringSessionStarter starter);
    Task<HaloApiResultContainer<AssetAuthoringSession, HaloApiErrorContainer>> HIUGCExtendSessionAgnostic(string title, string assetType, string assetId, bool includeContainerSas);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCDeleteSessionAgnostic(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCUndeleteAsset(string title, string assetType, string assetId);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCUndeleteVersion(string title, string assetType, string assetId, string versionId);
    Task<HaloApiResultContainer<bool, HaloApiErrorContainer>> HIUGCUnpublishAsset(string assetType, string assetId);
    Task<HaloApiResultContainer<EngineGameVariant, HaloApiErrorContainer>> HIUGCDiscoveryGetEngineGameVariant(string assetId, string versionId);
    Task<HaloApiResultContainer<EngineGameVariant, HaloApiErrorContainer>> HIUGCDiscoveryGetEngineGameVariantWithoutVersion(string assetId);
    Task<HaloApiResultContainer<Manifest, HaloApiErrorContainer>> HIUGCDiscoveryGetManifest(string assetId, string versionId, string clearanceId);
    Task<HaloApiResultContainer<Manifest, HaloApiErrorContainer>> HIUGCDiscoveryGetManifestByBuild(string buildNumber);
    Task<HaloApiResultContainer<Map, HaloApiErrorContainer>> HIUGCDiscoveryGetMap(string assetId, string versionId);
    Task<HaloApiResultContainer<MapModePair, HaloApiErrorContainer>> HIUGCDiscoveryGetMapModePair(string assetId, string versionId, string clearanceId);
    Task<HaloApiResultContainer<MapModePair, HaloApiErrorContainer>> HIUGCDiscoveryGetMapModePairWithoutVersion(string assetId);
    Task<HaloApiResultContainer<Map, HaloApiErrorContainer>> HIUGCDiscoveryGetMapWithoutVersion(string assetId);
    Task<HaloApiResultContainer<Playlist, HaloApiErrorContainer>> HIUGCDiscoveryGetPlaylist(string assetId, string versionId, string clearanceId);
    Task<HaloApiResultContainer<Playlist, HaloApiErrorContainer>> HIUGCDiscoveryGetPlaylistWithoutVersion(string assetId);
    Task<HaloApiResultContainer<Prefab, HaloApiErrorContainer>> HIUGCDiscoveryGetPrefab(string assetId, string versionId);
    Task<HaloApiResultContainer<Prefab, HaloApiErrorContainer>> HIUGCDiscoveryGetPrefabWithoutVersion(string assetId);
    Task<HaloApiResultContainer<Project, HaloApiErrorContainer>> HIUGCDiscoveryGetProject(string assetId, string versionId);
    Task<HaloApiResultContainer<Project, HaloApiErrorContainer>> HIUGCDiscoveryGetProjectWithoutVersion(string assetId);
    Task<HaloApiResultContainer<TagInfo, HaloApiErrorContainer>> HIUGCDiscoveryGetTagsInfo();
    Task<HaloApiResultContainer<UGCGameVariant, HaloApiErrorContainer>> HIUGCDiscoveryGetUgcGameVariant(string assetId, string versionId);
    Task<HaloApiResultContainer<UGCGameVariant, HaloApiErrorContainer>> HIUGCDiscoveryGetUgcGameVariantWithoutVersion(string assetId);
    Task<HaloApiResultContainer<SearchResultsContainer, HaloApiErrorContainer>> HIUGCDiscoverySearch(int start, int count, bool includeTimes, string sort, ResultOrder order, AssetKind assetKind);
    Task<HaloApiResultContainer<Film, HaloApiErrorContainer>> HIUGCDiscoverySpectateByMatchId(string matchId);
    Task<HaloApiResultContainer<List<Server>, HaloApiErrorContainer>> LobbyGetQosServers();
    Task<HaloApiResultContainer<LobbyPresenceContainer, HaloApiErrorContainer>> LobbyPresence(LobbyPresenceRequestContainer presenceRequest);
    Task<HaloApiResultContainer<LobbyJoinHandle, HaloApiErrorContainer>> LobbyThirdPartyJoinHandle(string lobbyId, string player, string handleAudience, string handlePlatform);
    Task<HaloApiResultContainer<FlightedFeatureFlags, HaloApiErrorContainer>> SettingGetFlightedFeatureFlags(string flightId);
    Task<HaloApiResultContainer<PlayerClearance, HaloApiErrorContainer>> SettingsGetClearance(string audience, string sandbox, string buildNumber);
    Task<HaloApiResultContainer<PlayerClearance, HaloApiErrorContainer>> SettingsGetPlayerClearance(string audience, string player, string sandbox, string buildNumber);
    Task<HaloApiResultContainer<PlayerSkillResultContainer, HaloApiErrorContainer>> SkillGetMatchPlayerResult(string matchId, List<string> playerIds);
    Task<HaloApiResultContainer<PlaylistCsrResultContainer, HaloApiErrorContainer>> SkillGetPlaylistCsr(string playlistId, List<string> playerIds);
    Task<HaloApiResultContainer<BanSummary, HaloApiErrorContainer>> StatsBanSummary(List<string> targetlist);
    Task<HaloApiResultContainer<PlayerDecks, HaloApiErrorContainer>> StatsGetChallengeDecks(string player);
    Task<HaloApiResultContainer<PlayerMatchCount, HaloApiErrorContainer>> StatsGetMatchCount(string player);
    Task<HaloApiResultContainer<MatchHistoryResponse, HaloApiErrorContainer>> StatsGetMatchHistory(string player, int start, int count, Models.HaloInfinite.MatchType type);
    Task<HaloApiResultContainer<MatchStats, HaloApiErrorContainer>> StatsGetMatchStats(string matchId);
    Task<HaloApiResultContainer<MatchStats, HaloApiErrorContainer>> StatsGetMatchStats(Guid matchId);
    Task<HaloApiResultContainer<MatchProgression, HaloApiErrorContainer>> StatsGetPlayerMatchProgression(string player, string matchId);
    Task<HaloApiResultContainer<MatchesPrivacy, HaloApiErrorContainer>?> StatsMatchPrivacy(string player);
    Task<HaloApiResultContainer<Key, HaloApiErrorContainer>> TextModerationGetSigningKey(string keyId);
    Task<HaloApiResultContainer<ModerationProofKeys, HaloApiErrorContainer>> TextModerationGetSigningKeys();
    Task<HaloApiResultContainer<List<User>, HaloApiErrorContainer>> Users(IEnumerable<string> xuids);
    Task<HaloApiResultContainer<User, HaloApiErrorContainer>> UserByGamertag(string gamertag);
    Task<HaloApiResultContainer<MedalMetadataResponse, HaloApiErrorContainer>> Medals();
}
