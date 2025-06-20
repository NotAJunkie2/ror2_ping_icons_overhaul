using BepInEx;
using R2API.Utils;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        public const string PluginVersion = "1.3.1";
        private const string bundleName = "pingiconsoverhaul";
        // Class variables
        private static AssetBundle bundle;
        private static readonly Dictionary<string, TexData> INTERACTABLES = new Dictionary<string, TexData>
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
            { "Teleporter", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Teleporters.TeleporterChargingPositionIndicator_prefab, texName = "texTeleporterIcon" } },
            // FIX with RoR2/Base/Teleporters/TeleporterChargingPositionIndicator.prefab
            // { "LunarTeleporter Variant", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Teleporters.TeleporterChargingPositionIndicator_prefab, texName = "texMoonTeleporterIcon" } }, // FIX
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
            // Intiialize the logger
            Log.Init(Logger);
        }

        public void Start()
        {
            Log.Info($"Initializing {PluginName} v{PluginVersion} by {PluginAuthor}");

            // Load the asset bundle from the plugin's directory
            bundle = AssetBundle.LoadFromFile(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(Info.Location), bundleName));

            // Set icons for interactables
            SetIconsForInteractables();

            Stage.onStageStartGlobal += (stage) =>
            {
                Log.Info("Stage started: " + stage.name);
                OverrideIconsForPurchasablesOnStageStart();
                OverrideIconsForPickupsOnStageStart();
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

        private void SetIconsForInteractables()
        {
            foreach (var keyValuePair in INTERACTABLES)
            {
                TexData texData = keyValuePair.Value;

                // Load interactable prefab from the addressable system, skip if it fails
                GameObject interactable = LoadInteractable(texData.addressable);
                if (interactable == null) continue;

                // Load icon from the asset bundle, skip if it fails
                Sprite icon = LoadIcon(texData.addressable, texData.texName);
                if (interactable == null) continue;

                // Add IDisplayName provider to the interactable
                AddGenericDisplayNameProvider(interactable, keyValuePair.Key);
                // Override ping icon for the interactable
                AddPingIconOverride(interactable, icon);
            }
        }

        private static GameObject LoadInteractable(string addressable)
        {
            try
            {
                GameObject interactable = Addressables.LoadAssetAsync<GameObject>(addressable).WaitForCompletion();
                if (!interactable)
                {
                    Log.Error($"Failed to load interactable \"{addressable}\"");
                    return null;
                }
                return interactable;
            }
            catch (System.Exception ex)
            {
                Log.Error($"Exception while loading interactable {addressable}: {ex.Message}");
                return null;
            }
        }

        private static Sprite LoadIcon(string addressable, string texName)
        {
            try
            {
                if (bundle == null)
                {
                    Log.Error("Asset bundle is not loaded.");
                    return null;
                }

                Sprite icon = bundle.LoadAsset<Sprite>(texName);
                if (icon == null)
                {
                    Log.Error($"Failed to load icon {texName} from bundle for {addressable}");
                    return null;
                }
                return icon;
            }
            catch (System.Exception ex)
            {
                Log.Error($"Exception while loading icon {texName} from bundle for {addressable}: {ex.Message}");
                return null;
            }
        }

        private static void AddGenericDisplayNameProvider(GameObject interactable, string displayName)
        {
            interactable.TryGetComponent(out IDisplayNameProvider displayNameProvider);
            if (displayNameProvider == null)
            {
                Log.Info($"Adding GenericDisplayNameProvider to {interactable.name}, display name: {displayName}");
                displayNameProvider = interactable.AddComponent<GenericDisplayNameProvider>();
                ((GenericDisplayNameProvider)displayNameProvider).SetDisplayToken(displayName);
            }
        }

        private void AddPingIconOverride(GameObject interactable, Sprite sprite)
        {
            try
            {
                interactable.TryGetComponent<PingInfoProvider>(out PingInfoProvider pingProvider);

                if (pingProvider == null)
                {
                    pingProvider = interactable.AddComponent<PingInfoProvider>();
                }

                pingProvider.pingIconOverride = sprite;
                Log.Info($"Successfully added ping icon override for {interactable.name} with icon {sprite.name}");
            }
            catch (System.Exception ex)
            {
                Log.Error($"Failed to add ping icon override for {interactable.name}: {ex.Message}");
            }
        }

        private void OverrideIconsForPurchasablesOnStageStart()
        {
            InstanceTracker.GetInstancesList<PurchaseInteraction>().ForEachTry(purchaseInteraction =>
            {
                string name = purchaseInteraction.name.Split('(')[0].Trim();
                if (INTERACTABLES.TryGetValue(name, out TexData texData))
                {
                    purchaseInteraction.TryGetComponent<PingInfoProvider>(out PingInfoProvider pingInfoProvider);
                    if (pingInfoProvider == null)
                    {
                        pingInfoProvider = purchaseInteraction.gameObject.AddComponent<PingInfoProvider>();
                        pingInfoProvider.pingIconOverride = LoadIcon(texData.addressable, texData.texName);
                    }
                    else
                    {
                        if (pingInfoProvider.pingIconOverride.name != texData.texName)
                        {
                            Log.Info($"Overriding ping icon for {name} with {texData.texName}");
                            pingInfoProvider.pingIconOverride = LoadIcon(texData.addressable, texData.texName);
                        }
                    }
                }
                else
                {
                    Log.Warning($"No icon found for {name}");
                }
            });
        }

        private void OverrideIconsForPickupsOnStageStart()
        {
            InstanceTracker.GetInstancesList<GenericPickupController>().ForEachTry(pickupInteraction =>
            {
                string name = pickupInteraction.name.Split('(')[0].Trim();
                if (INTERACTABLES.TryGetValue(name, out TexData texData))
                {
                    pickupInteraction.TryGetComponent<PingInfoProvider>(out PingInfoProvider pingInfoProvider);
                    if (pingInfoProvider == null)
                    {
                        pingInfoProvider = pickupInteraction.gameObject.AddComponent<PingInfoProvider>();
                        pingInfoProvider.pingIconOverride = LoadIcon(texData.addressable, texData.texName);
                    }
                    else
                    {
                        if (pingInfoProvider.pingIconOverride.name != texData.texName)
                        {
                            Log.Info($"Overriding ping icon for {name} with {texData.texName}");
                            pingInfoProvider.pingIconOverride = LoadIcon(texData.addressable, texData.texName);
                        }
                    }
                }
                else
                {
                    Log.Warning($"No icon found for {name}");
                }
            });
        }
    }
}
