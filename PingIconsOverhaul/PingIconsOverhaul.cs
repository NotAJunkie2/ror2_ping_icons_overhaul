using BepInEx;
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
        public const string PluginVersion = "1.0.1";
        private const string bundleName = "pingiconsoverhaul";
        // Class variables
        private static AssetBundle bundle;
        private static readonly Dictionary<string, TexData> INTERACTABLES = new Dictionary<string, TexData>
        {
            // 3D Printers
            { "CleansingPool", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCleanse.ShrineCleanse_prefab, texName = "texCleansingPoolIcon"} },
            { "CleansingPoolSandy", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCleanse_ShrineCleanseSandy.Variant_prefab, texName = "texCleansingPoolIcon"} },
            { "CleansingPoolSnowy", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_ShrineCleanse_ShrineCleanseSnowy.Variant_prefab, texName = "texCleansingPoolIcon"} },
            { "3DPrinter", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Duplicator.Duplicator_prefab, texName = "texDuplicatorIcon" } },
            { "3DPrinterLarge", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_DuplicatorLarge.DuplicatorLarge_prefab, texName = "texDuplicatorIcon" } },
            { "3DPrinterMilitary", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_DuplicatorMilitary.DuplicatorMilitary_prefab, texName = "texDuplicatorIcon" } },
            { "3DPrinterWild", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_DuplicatorWild.DuplicatorWild_prefab, texName = "texDuplicatorIcon" } },

            // Barrels
            { "Barrel", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Barrel1.Barrel1_prefab, texName = "texBarrelIcon" } },
            { "VoidStalk", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VoidCoinBarrel.VoidCoinBarrel_prefab, texName = "texVoidStalkIcon" } },

            // // Charging zones

            // // Chests & Equipment
            { "AdaptiveChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CasinoChest.CasinoChest_prefab, texName = "texAdaptiveChestIcon" } },
            { "SmallChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Chest1.Chest1_prefab, texName = "texSmallChestIcon" } },
            { "SmallCatChestDamage", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CategoryChest.CategoryChestDamage_prefab, texName = "texSmallCatChestDamIcon" } },
            { "SmallCatChestHealing", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CategoryChest.CategoryChestHealing_prefab, texName = "texSmallCatChestHealIcon" } },
            { "SmallCatChestUtility", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_CategoryChest.CategoryChestUtility_prefab, texName = "texSmallCatChestUtilIcon" } },
            { "ChestCloacked", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Chest1StealthedVariant.Chest1StealthedVariant_prefab, texName = "texCloackedChestIcon" } },
            { "EquipmentBarrel", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_EquipmentBarrel.EquipmentBarrel_prefab, texName = "texEquipmentBarrelIcon" } },
            // Large
            { "LargeChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Chest2.Chest2_prefab, texName = "texLargeChestIcon" } },
            { "LargeCatChestDamage", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_CategoryChest2_CategoryChest2Damage.Variant_prefab, texName = "texLargeCatChestDamIcon" } },
            { "LargeCatChestHealing", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_CategoryChest2_CategoryChest2Healing.Variant_prefab, texName = "texLargeCatChestHealIcon" } },
            { "LargeCatChestUtility", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_CategoryChest2_CategoryChest2Utility.Variant_prefab, texName = "texLargeCatChestUtilIcon" } },
            // // Multishops
            { "TripleShopEquipment", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_TripleShopEquipment.TripleShopEquipment_prefab, texName = "texTripleShopEquipmentIcon"} },
            { "MultiShopEquipment", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_MultiShopEquipmentTerminal.MultiShopEquipmentTerminal_prefab, texName = "texTripleshopEquipmentIcon"}},
            { "MultiShop", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_MultiShopTerminal.MultiShopTerminal_prefab, texName = "texTripleShopIcon" } },
            { "LargeMultiShop", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_MultiShopLargeTerminal.MultiShopLargeTerminal_prefab, texName = "texTripleShopIcon" } },
            { "TripleShop", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_TripleShop.TripleShop_prefab, texName = "texTripleShopIcon" } },
            { "LargeTripleShop", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_TripleShopLarge.TripleShopLarge_prefab, texName = "texTripleShopIcon" } },
            // // Special
            { "LegendaryChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_GoldChest.GoldChest_prefab, texName = "texLegendaryChestIcon" } },
            { "LunarPod", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_LunarChest.LunarChest_prefab, texName = "texLunarPodIcon" } },
            { "RustyLockbox", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_TreasureCache.Lockbox_prefab, texName = "texRustyLockboxIcon"} },
            { "ScavSack", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Scav.ScavBackpack_prefab, texName = "texScavBackpackIcon"} },
            { "MultishopDelivery", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_FreeChestTerminalShippingDrone.FreeChestTerminalShippingDrone_prefab, texName = "texCrashedDeliveryIcon"} },
            { "EncrustedCache", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_TreasureCacheVoid.LockboxVoid_prefab, texName = "texEncrustedCacheIcon"} },
            { "VoidCradle", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VoidChest.VoidChest_prefab, texName = "texVoidCradleIcon"} },
            { "VoidPotentialChest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC1_VoidTriple.VoidTriple_prefab, texName = "texVoidPotentialIcon"} },
            { "AurelioniteFragment", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_DLC2.FragmentPotentialPickup_prefab, texName = "texAurelioniteFragmentIcon"} },

            // // Drones

            // // Environment Specific
            { "ArtifactPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Common.SetpiecePickup_prefab, texName = "texArtifactPickupIcon"} },


            // // Pickups
            { "CommandEssence", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Command.CommandCube_prefab, texName = "texCommandEssenceIcon" } },
            { "GenericPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Common.GenericPickup_prefab, texName = "texGenericPickupIcon" } },
            { "LogPickup", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_logbook.logbookPPLocal_prefab, texName = "texLogbookEntryIcon" } },
            { "FuelArrayQuest", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_QuestVolatileBattery.QuestVolatileBatteryWorldPickup_prefab, texName = "texFuelArrayQuestIcon" } },

            // // Portals

            // // Scrapper
            { "Scrapper", new TexData { addressable = RoR2BepInExPack.GameAssetPaths.RoR2_Base_Scrapper.Scrapper_prefab, texName = "texScrapperIcon" } },

            // Shrines

            // Skill Related
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
        }

        private void SetIconsForInteractables()
        {
            foreach (var keyValuePair in INTERACTABLES)
            {
                TexData texData = keyValuePair.Value;

                // Load interactable prefab from the addressable system, skip if it fails
                GameObject interactable = LoadInteractable(texData, keyValuePair.Key);
                if (interactable == null) continue;

                // Load icon from the asset bundle, skip if it fails
                Sprite icon = LoadIcon(texData);
                if (interactable == null) continue;

                // Override ping icon for the interactable
                AddPingIconOverride(interactable, icon);
            }
        }

        private static GameObject LoadInteractable(TexData texData, string key)
        {
            try
            {
                GameObject interactable = Addressables.LoadAssetAsync<GameObject>(texData.addressable).WaitForCompletion();
                if (!interactable)
                {
                    Log.Error($"Failed to load interactable {texData.addressable} for {key}");
                    return null;
                }
                return interactable;
            }
            catch (System.Exception ex)
            {
                Log.Error($"Exception while loading interactable {texData.addressable} for {key}: {ex.Message}");
                return null;
            }
        }

        private static Sprite LoadIcon(TexData texData)
        {
            try
            {
                if (bundle == null)
                {
                    Log.Error("Asset bundle is not loaded.");
                    return null;
                }

                Sprite icon = bundle.LoadAsset<Sprite>(texData.texName);
                if (icon == null)
                {
                    Log.Error($"Failed to load icon {texData.texName} from bundle for {texData.addressable}");
                    return null;
                }
                return icon;
            }
            catch (System.Exception ex)
            {
                Log.Error($"Exception while loading icon {texData.texName} from bundle for {texData.addressable}: {ex.Message}");
                return null;
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
    }
}
