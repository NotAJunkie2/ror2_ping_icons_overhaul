using BepInEx;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

// StreamingAssets\Language\en for translations
namespace PingIconsOverhaul
{
    struct TexData
    {
        public string addressable;
        public string texName;
    }

    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class PingIconsOverhaul : BaseUnityPlugin
    {
        // Plugin information
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "NotAJunkie";
        public const string PluginName = "PingIconsOverhaul";
        public const string PluginVersion = "1.3.2";
        private const string bundleName = "pingiconsoverhaul";
        // Class variables
        private static AssetBundle? bundle;
        private static readonly Dictionary<string, TexData> INTERACTABLES = new()
        {
            // 3D Printers
            { "ShrineCleanse", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCleanse.ShrineCleanse_prefab, texName = "texCleansingPoolIcon"} },
            { "ShrineCleanseSandy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCleanse_ShrineCleanseSandy.Variant_prefab, texName = "texCleansingPoolIcon"} },
            { "ShrineCleanseSnowy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCleanse_ShrineCleanseSnowy.Variant_prefab, texName = "texCleansingPoolIcon"} },
            { "Duplicator", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Duplicator.Duplicator_prefab, texName = "texDuplicatorIcon" } },
            { "DuplicatorLarge", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_DuplicatorLarge.DuplicatorLarge_prefab, texName = "texDuplicatorIcon" } },
            { "DuplicatorMilitary", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_DuplicatorMilitary.DuplicatorMilitary_prefab, texName = "texDuplicatorIcon" } },
            { "DuplicatorWild", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_DuplicatorWild.DuplicatorWild_prefab, texName = "texDuplicatorIcon" } },

            // Barrels
            { "Barrel1", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Barrel1.Barrel1_prefab, texName = "texBarrelIcon" } },
            { "VoidCoinBarrel", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VoidCoinBarrel.VoidCoinBarrel_prefab, texName = "texVoidStalkIcon" } },

            // Charging zones
            // FIX with RoR2/Base/Teleporters/TeleporterChargingPositionIndicator.prefab
            { "Teleporter1", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Teleporters.Teleporter1_prefab, texName = "texTeleporterIcon" } },
            { "LunarTeleporter Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Teleporters_LunarTeleporter.Variant_prefab, texName = "texMoonTeleporterIcon" } },
            { "LunarTeleporterProngs", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Teleporters.LunarTeleporterProngs_prefab, texName = "texMoonTeleporterIcon" } },

            { "MoonBatteryBlood", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_moon2.MoonBatteryBlood_prefab, texName = "texPillarBloodIcon" } },
            { "MoonBatteryDesign", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_moon2.MoonBatteryDesign_prefab, texName = "texPillarDesignIcon" } },
            { "MoonBatteryMass", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_moon2.MoonBatteryMass_prefab, texName = "texPillarMassIcon" } },
            { "MoonBatterySoul", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_moon2.MoonBatterySoul_prefab, texName = "texPillarSoulIcon" } },
            { "DeepVoidPortalBattery", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_DeepVoidPortalBattery.DeepVoidPortalBattery_prefab, texName = "texVoidSignalIcon" } },
            { "NullSafeZone", new TexData { addressable = "", texName = "texCellVentIcon"} },
            { "InfiniteTowerSafeWard", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_GameModes_InfiniteTowerRun_InfiniteTowerAssets.InfiniteTowerSafeWard_prefab, texName = "texVoidFocusIcon"} },

            // Chests & Equipment
            { "CasinoChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CasinoChest.CasinoChest_prefab, texName = "texAdaptiveChestIcon" } },
            { "Chest1", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Chest1.Chest1_prefab, texName = "texSmallChestIcon" } },
            { "CategoryChestDamage", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CategoryChest.CategoryChestDamage_prefab, texName = "texSmallCatChestDamIcon" } },
            { "CategoryChestHealing", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CategoryChest.CategoryChestHealing_prefab, texName = "texSmallCatChestHealIcon" } },
            { "CategoryChestUtility", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CategoryChest.CategoryChestUtility_prefab, texName = "texSmallCatChestUtilIcon" } },
            { "Chest1StealthedVariant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Chest1StealthedVariant.Chest1StealthedVariant_prefab, texName = "texCloackedChestIcon" } },
            { "EquipmentBarrel", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_EquipmentBarrel.EquipmentBarrel_prefab, texName = "texEquipmentBarrelIcon" } },
            // Large
            { "Chest2", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Chest2.Chest2_prefab, texName = "texLargeChestIcon" } },
            { "CategoryChest2Damage Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_CategoryChest2_CategoryChest2Damage.Variant_prefab, texName = "texLargeCatChestDamIcon" } },
            { "CategoryChest2Healing Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_CategoryChest2_CategoryChest2Healing.Variant_prefab, texName = "texLargeCatChestHealIcon" } },
            { "CategoryChest2Utility Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_CategoryChest2_CategoryChest2Utility.Variant_prefab, texName = "texLargeCatChestUtilIcon" } },
            // Multishops
            { "MultiShopEquipment", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_MultiShopEquipmentTerminal.MultiShopEquipmentTerminal_prefab, texName = "texTripleshopEquipmentIcon"}},
            { "MultiShopTerminal", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_MultiShopTerminal.MultiShopTerminal_prefab, texName = "texTripleShopIcon" } },
            { "MultiShopLargeTerminal", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_MultiShopLargeTerminal.MultiShopLargeTerminal_prefab, texName = "texTripleShopIcon" } },
            // Special
            { "GoldChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_GoldChest.GoldChest_prefab, texName = "texLegendaryChestIcon" } },
            { "LunarChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_LunarChest.LunarChest_prefab, texName = "texLunarPodIcon" } },
            { "Lockbox", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_TreasureCache.Lockbox_prefab, texName = "texRustyLockboxIcon"} },
            { "ScavBackpack", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Scav.ScavBackpack_prefab, texName = "texScavBackpackIcon"} },
            { "FreeChestTerminalShippingDrone", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_FreeChestTerminalShippingDrone.FreeChestTerminalShippingDrone_prefab, texName = "texCrashedDeliveryIcon"} },
            { "LockboxVoid", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_TreasureCacheVoid.LockboxVoid_prefab, texName = "texEncrustedCacheIcon"} },
            { "VoidChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VoidChest.VoidChest_prefab, texName = "texVoidCradleIcon"} },
            { "VoidTriple", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VoidTriple.VoidTriple_prefab, texName = "texVoidPotentialIcon"} },
            { "FragmentPotentialPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC2.FragmentPotentialPickup_prefab, texName = "texAurelioniteFragmentIcon"} },

            // Drones

            // Environment Specific
            { "SetpiecePickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Common.SetpiecePickup_prefab, texName = "texArtifactPickupIcon"} },

            // Pickups
            { "CommandCube", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Command.CommandCube_prefab, texName = "texCommandEssenceIcon" } },
            { "GenericPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Common.GenericPickup_prefab, texName = "texGenericPickupIcon" } },
            { "LogPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Common.LogPickup_prefab, texName = "texLogbookEntryIcon" } },
            { "QuestVolatileBatteryWorldPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_QuestVolatileBattery.QuestVolatileBatteryWorldPickup_prefab, texName = "texFuelArrayQuestIcon" } },

            // Portals

            // Scrapper
            { "Scrapper", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Scrapper.Scrapper_prefab, texName = "texScrapperIcon" } },

            // Shrines
            { "ShrineGoldshoresAccess", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineGoldshoresAccess.ShrineGoldshoresAccess_prefab, texName = "texShrineGoldIcon" } },

            { "ShrineBlood", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineBlood.ShrineBlood_prefab, texName = "texShrineBloodIcon" } },
            { "ShrineBloodSandy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineBlood_ShrineBloodSandy.Variant_prefab, texName = "texShrineBloodIcon" } },
            { "ShrineBloodSnowy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineBlood_ShrineBloodSnowy.Variant_prefab, texName = "texShrineBloodIcon" } },

            { "ShrineChance", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineChance.ShrineChance_prefab, texName = "texShrineChanceIcon" } },
            { "ShrineChanceSandy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineChance_ShrineChanceSandy.Variant_prefab, texName = "texShrineChanceIcon" } },
            { "ShrineChanceSnowy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineChance_ShrineChanceSnowy.Variant_prefab, texName = "texShrineChanceIcon" } },

            { "ShrineCombat", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCombat.ShrineCombat_prefab, texName = "texShrineCombatIcon" } },
            { "ShrineCombatSandy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCombat_ShrineCombatSandy.Variant_prefab, texName = "texShrineCombatIcon" } },
            { "ShrineCombatSnowy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCombat_ShrineCombatSnowy.Variant_prefab, texName = "texShrineCombatIcon" } },

            { "ShrineRestack", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineRestack.ShrineRestack_prefab, texName = "texShrineOrderIcon" } },
            { "ShrineRestackSandy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineRestack_ShrineRestackSandy.Variant_prefab, texName = "texShrineOrderIcon" } },
            { "ShrineRestackSnowy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineRestack_ShrineRestackSnowy.Variant_prefab, texName = "texShrineOrderIcon" } },

            { "ShrineBoss", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineBoss.ShrineBoss_prefab, texName = "texShrineMountainIcon" } },
            { "ShrineBossSandy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineBoss_ShrineBossSandy.Variant_prefab, texName = "texShrineMountainIcon" } },
            { "ShrineBossSnowy Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineBoss_ShrineBossSnowy.Variant_prefab, texName = "texShrineMountainIcon" } },

            { "ShrineHealing", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineHealing.ShrineHealing_prefab, texName = "texShrineWoodsIcon" } },

            // DLC 2
            { "ShrineColossusAccess", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC2.ShrineColossusAccess_prefab, texName = "texShrineShapingIcon" } },
            { "ShrineRebirth", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC2.ShrineRebirth_prefab, texName = "texShrineRebirthIcon" } },
            { "ShrineHalcyonite", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC2.ShrineHalcyonite_prefab, texName = "texShrineHalcyonIcon" } },

            // Skill Related
            { "CaptainSupplyDrop, EquipmentRestock", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Captain_CaptainSupplyDrop.EquipmentRestock_prefab, texName = "texResupplyIcon" } },
            { "VendingMachine", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VendingMachine.VendingMachine_prefab, texName = "texVendingMachineIcon" } },
            { "ZiplineVehicle", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Gateway.ZiplineVehicle_prefab, texName = "texTunnelIcon" } }
        };

        public void Awake()
        {
            Log.Init(Logger); // Intiialize the logger
        }

        public void Start()
        {
            Log.Info($"Initializing {PluginName} v{PluginVersion} by {PluginAuthor}");

            // Load the asset bundle from the plugin's directory
            bundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Info.Location), bundleName));

            SetPingIconsForInteractables(); // Set icons for interactables

            // Override icons for interactables on stage start (necessary for such objects as Legendary Chests in Abyssal Depths, Moon Pillars, etc...)
            Stage.onStageStartGlobal += (stage) =>
            {
                Log.Info("Stage started: " + stage.name);
                OverridePingIconsForObjects<PurchaseInteraction>();
                OverridePingIconsForObjects<GenericPickupController>();
                OverridePingIconsForTeleporters();
            };
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Backslash))
            {
                InstanceTracker.instancesLists.ForEachTry(kvp =>
                {
                    Log.Info($"Key: {kvp.Key}, Value: {kvp.Value}");
                    foreach (var instance in kvp.Value)
                    {
                        Log.Info($"  Instance: {instance.name} of type {instance.GetType()}");
                    }
                });
            }
        }

        private void SetPingIconsForInteractables()
        {
            foreach (var keyValuePair in INTERACTABLES)
            {
                if (string.IsNullOrEmpty(keyValuePair.Value.addressable))
                {
                    Log.Warning($"Skipping {keyValuePair.Key} due to missing addressable.");
                    continue;
                }

                TexData td = keyValuePair.Value;

                // Load interactable prefab from the addressable system, skip if it fails
                if (!LoadInteractable(td.addressable, out GameObject interactable)) continue;

                // Load icon from the asset bundle, skip if it fails
                if (!LoadPingIcon(td.addressable, td.texName, out Sprite? pingIcon)) continue;

                // Add IDisplayName provider to the interactable
                if (!keyValuePair.Key.Contains("Prongs")) // Ignore prongs as they default to "Primordial Teleporter"
                {
                    AddGenericDisplayNameProvider(interactable, keyValuePair.Key);
                }

                // Override ping icon for the interactable
                AddPingIconOverride(interactable, pingIcon!);
            }
        }

        private static bool LoadInteractable(string addressable, out GameObject interactable)
        {
            interactable = Addressables.LoadAssetAsync<GameObject>(addressable).WaitForCompletion();
            if (interactable == null)
            {
                Log.Error($"Failed to load interactable \"{addressable}\"");
            }
            return interactable != null;
        }

        private static bool LoadPingIcon(string addressable, string texName, out Sprite? pingIcon)
        {
            if (bundle == null)
            {
                Log.Error("Failed to load Asset bundle.");
                pingIcon = null;
                return false;
            }

            pingIcon = bundle.LoadAsset<Sprite>(texName);
            if (pingIcon == null)
            {
                Log.Error($"Failed to load ping icon \"{texName}\" from bundle for \"{addressable}\"");
            }

            return pingIcon != null;
        }

        private static void AddGenericDisplayNameProvider(GameObject interactable, string displayName)
        {
            if (interactable.TryGetComponent(out IDisplayNameProvider _)) return;

            Log.Info($"Adding GenericDisplayNameProvider to {interactable.name}, display name: {displayName}");
            var displayNameProvider = interactable.AddComponent<GenericDisplayNameProvider>();
            displayNameProvider.SetDisplayToken(displayName);
        }

        private void AddPingIconOverride(GameObject interactable, Sprite sprite)
        {
            if (!interactable.TryGetComponent(out PingInfoProvider pingProvider))
            {
                pingProvider = interactable.AddComponent<PingInfoProvider>();
            }

            pingProvider.pingIconOverride = sprite;
            Log.Info($"Successfully added ping icon override for {interactable.name} with {sprite.name}");
        }

        private static void OverridePingIconsForObjects<T>() where T : MonoBehaviour
        {
            InstanceTracker.GetInstancesList<T>().ForEachTry(interactable =>
            {
                string name = interactable.name.Split('(')[0].Trim();
                if (!INTERACTABLES.TryGetValue(name, out TexData texData))
                {
                    Log.Warning($"No ping icon is available for {name} in asset dictionary");
                    return;
                }

                if (!interactable.TryGetComponent(out PingInfoProvider pingInfoProvider))
                {
                    pingInfoProvider = interactable.gameObject.AddComponent<PingInfoProvider>();
                }

                if (pingInfoProvider.pingIconOverride.name != texData.texName)
                {
                    Log.Info($"Overriding ping icon for {name} with {texData.texName}");
                    if (!LoadPingIcon(texData.addressable, texData.texName, out Sprite? pingIcon)) return;

                    pingInfoProvider.pingIconOverride = pingIcon;
                }
            });
        }

        private static void OverridePingIconsForTeleporters()
        {
            var teleporters = InstanceTracker.GetInstancesList<TeleporterInteraction>();
            if (teleporters.Count == 0)
            {
                Log.Warning("No TeleporterInteraction instances found.");
                return;
            }

            Log.Info($"Found {teleporters.Count} TeleporterInteraction instances.");
            teleporters.ForEachTry(teleporter =>
            {
                if (teleporter == null)
                {
                    Log.Warning("Found a null TeleporterInteraction instance.");
                    return;
                }

                ReplaceTeleporterPingIcon(teleporter);
            });
        }

        private static void ReplaceTeleporterPingIcon(TeleporterInteraction teleporter)
        {
            string name = teleporter.name.Split('(')[0].Trim();

            if (!INTERACTABLES.TryGetValue(name, out _))
            {
                Log.Warning($"No ping icon found for teleporter: {name}");
                return;
            }

            Log.Info($"Replacing icon for teleporter: {name}");
            PositionIndicator posIndicator = teleporter.teleporterPositionIndicator;
            if (posIndicator?.alwaysVisibleObject.TryGetComponent(out SpriteRenderer sr) != true)
            {
                Log.Warning("No SpriteRenderer found on alwaysVisibleObject.");
                return;
            }

            if (!teleporter.TryGetComponent(out PingInfoProvider pingInfo))
            {
                Log.Warning("No PingInfoProvider found on the teleporter.");
                return;
            }

            sr.sprite = pingInfo.pingIconOverride;
        }
    }
}
