using BepInEx;
using R2API;
using RoR2;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace RoR2_PingIconsOverhaul
{
    struct TexData
    {
        public string addressable;
        public string texName;
    }

    [BepInDependency(LanguageAPI.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]

    public class PingIconsOverhaul : BaseUnityPlugin
    {
        // Plugin information
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "NotAJunkie";
        public const string PluginName = "PingIconsOverhaul";
        public const string PluginVersion = "1.0.0";
        private const string bundleName = "pingiconsoverhaul";
        // Class variables
        private static AssetBundle bundle;
        private static readonly Dictionary<string, TexData> INTERACTABLES = new()
        {
            // 3D Printers
            { "CleansingPool", new TexData { addressable = "RoR2/Base/ShrineCleanse/ShrineCleanse.prefab", texName = "texCleansingPoolIcon"} },
            { "CleansingPoolSandy", new TexData { addressable = "RoR2/Base/ShrineCleanse/ShrineCleanseSandy Variant.prefab", texName = "texCleansingPoolIcon"} },
            { "CleansingPoolSnowy", new TexData { addressable = "RoR2/Base/ShrineCleanse/ShrineCleanseSnowy Variant.prefab", texName = "texCleansingPoolIcon"} },
            { "3DPrinter", new TexData { addressable = "RoR2/Base/Duplicator/Duplicator.prefab", texName = "texDuplicatorIcon" } },
            { "3DPrinterLarge", new TexData { addressable = "RoR2/Base/DuplicatorLarge/DuplicatorLarge.prefab", texName = "texDuplicatorIcon" } },
            { "3DPrinterMilitary", new TexData { addressable = "RoR2/Base/DuplicatorMilitary/DuplicatorMilitary.prefab", texName = "texDuplicatorIcon" } },
            { "3DPrinterWild", new TexData { addressable = "RoR2/Base/DuplicatorWild/DuplicatorWild.prefab", texName = "texDuplicatorIcon" } },

            // Barrels
            { "Barrel", new TexData { addressable = "RoR2/Base/Barrel1/Barrel1.prefab", texName = "texBarrelIcon" } },
            { "VoidStalk", new TexData { addressable = "RoR2/DLC1/VoidCoinBarrel/VoidCoinBarrel.prefab", texName = "texVoidStalkIcon" } }, // NOT VISIBLE ENOUGH

            // Charging zones

            // Chests & Equipment
            { "AdaptiveChest", new TexData { addressable = "RoR2/Base/CasinoChest/CasinoChest.prefab", texName = "texAdaptiveChestIcon" } },
            { "SmallChest", new TexData { addressable = "RoR2/Base/Chest1/Chest1.prefab", texName = "texSmallChestIcon" } },
            { "SmallCatChestDamage", new TexData { addressable = "RoR2/Base/CategoryChest/CategoryChestDamage.prefab", texName = "texSmallCatChestDamIcon" } },
            { "SmallCatChestHealing", new TexData { addressable = "RoR2/Base/CategoryChest/CategoryChestHealing.prefab", texName = "texSmallCatChestHealIcon" } },
            { "SmallCatChestUtility", new TexData { addressable = "RoR2/Base/CategoryChest/CategoryChestUtility.prefab", texName = "texSmallCatChestUtilIcon" } },
            { "ChestCloacked", new TexData { addressable = "RoR2/Base/Chest1StealthedVariant/Chest1StealthedVariant.prefab", texName = "texCloackedChestIcon" } },
            { "EquipmentBarrel", new TexData { addressable = "RoR2/Base/EquipmentBarrel/EquipmentBarrel.prefab", texName = "texEquipmentBarrelIcon" } },
            // Large
            { "LargeChest", new TexData { addressable = "RoR2/Base/Chest2/Chest2.prefab", texName = "texLargeChestIcon" } },
            { "LargeCatChestDamage", new TexData { addressable = "RoR2/DLC1/CategoryChest2/CategoryChest2Damage Variant.prefab", texName = "texLargeCatChestDamIcon" } },
            { "LargeCatChestHealing", new TexData { addressable = "RoR2/DLC1/CategoryChest2/CategoryChest2Healing Variant.prefab", texName = "texLargeCatChestHealIcon" } },
            { "LargeCatChestUtility", new TexData { addressable = "RoR2/DLC1/CategoryChest2/CategoryChest2Utility Variant.prefab", texName = "texLargeCatChestUtilIcon" } },
            // Multishops
            { "TripleShopEquipment", new TexData { addressable = "RoR2/Base/TripleShopEquipment/TripleShopEquipment.prefab", texName = "texTripleShopEquipmentIcon" } },
            { "MultiShopEquipment", new TexData { addressable = "RoR2/Base/MultiShopEquipmentTerminal/MultiShopEquipmentTerminal.prefab", texName = "texTripleshopEquipmentIcon"}},
            { "MultiShop", new TexData { addressable = "RoR2/Base/MultiShopTerminal/MultiShopTerminal.prefab", texName = "texTripleShopIcon" } },
            { "LargeMultiShop", new TexData { addressable = "RoR2/Base/MultiShopLargeTerminal/MultiShopLargeTerminal.prefab", texName = "texTripleShopIcon" } },
            { "TripleShop", new TexData { addressable = "RoR2/Base/TripleShop/TripleShop.prefab", texName = "texTripleShopIcon" } },
            { "LargeTripleShop", new TexData { addressable = "RoR2/Base/TripleShopLarge/TripleShopLarge.prefab", texName = "texTripleShopIcon" } },
            // Special
            { "LegendaryChest", new TexData { addressable = "RoR2/Base/GoldChest/GoldChest.prefab", texName = "texLegendaryChestIcon" } },
            { "LunarPod", new TexData { addressable = "RoR2/Base/LunarChest/LunarChest.prefab", texName = "texLunarPodIcon" } },
            { "RustyLockbox", new TexData { addressable = "RoR2/Base/TreasureCache/Lockbox.prefab", texName = "texRustyLockboxIcon"} },
            { "ScavSack", new TexData { addressable = "RoR2/Base/Scav/ScavBackpack.prefab", texName = "texScavBackpackIcon"} },
            { "MultishopDelivery", new TexData { addressable = "RoR2/DLC1/FreeChestTerminalShippingDrone/FreeChestTerminalShippingDrone.prefab", texName = "texCrashedDeliveryIcon"} },
            { "EncrustedCache", new TexData { addressable = "RoR2/DLC1/TreasureCacheVoid/LockboxVoid.prefab", texName = "texEncrustedCacheIcon"} },
            { "VoidCradle", new TexData { addressable = "RoR2/DLC1/VoidChest/VoidChest.prefab", texName = "texVoidCradleIcon"} },
            { "VoidPotentialChest", new TexData { addressable = "RoR2/DLC1/VoidTriple/VoidTriple.prefab", texName = "texVoidPotentialIcon"} },
            { "AurelioniteFragment", new TexData { addressable = "RoR2/DLC2/FragmentPotentialPickup.prefab", texName = "texAurelioniteFragmentIcon"} },

            // Drones

            // Environment Specific
            { "ArtifactPickup", new TexData { addressable = "RoR2/Base/Common/SetpiecePickup.prefab", texName = "texArtifactPickupIcon"} },

            // Pickups
            { "CommandEssence", new TexData { addressable = "RoR2/Base/Command/CommandCube.prefab", texName = "texCommandEssenceIcon" } },
            { "GenericPickup", new TexData { addressable = "RoR2/Base/Common/GenericPickup.prefab", texName = "texGenericPickupIcon" } },
            { "LogPickup", new TexData { addressable = "RoR2/Base/Common/LogPickup.prefab", texName = "texLogbookEntryIcon" } },
            { "FuelArrayQuest", new TexData { addressable = "RoR2/Base/QuestVolatileBattery/QuestVolatileBatteryWorldPickup.prefab", texName = "texFuelArrayQuestIcon" } },

            // Portals

            // Scrapper
            { "Scrapper", new TexData { addressable = "RoR2/Base/Scrapper/Scrapper.prefab", texName = "texScrapperIcon" } },

            // Shrines

            // Skill Related
        };

        public void Awake()
        {
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
                try
                {
                    GameObject interactable = LoadInteractable(texData, keyValuePair.Key);
                    if (interactable == null) continue;

                    // Load icon from the asset bundle, skip if it fails
                    Sprite icon = LoadIcon(texData);
                    if (interactable == null) continue;

                    // Override ping icon for the interactable
                    AddPingIconOverride(interactable, icon);
                }
                catch (System.Exception ex)
                {
                    Log.Error($"Error processing {keyValuePair.Key}: {ex.Message}");
                }
            }
        }

        private static GameObject LoadInteractable(TexData texData, string key)
        {
            GameObject interactable = Addressables.LoadAssetAsync<GameObject>(texData.addressable).WaitForCompletion();
            if (!interactable)
            {
                Log.Error($"Failed to load interactable {texData.addressable} for {key}");
                return null;
            }
            return interactable;
        }

        private static Sprite LoadIcon(TexData texData)
        {
            Sprite icon = bundle.LoadAsset<Sprite>(texData.texName);
            if (icon == null)
            {
                Log.Error($"Failed to load icon {texData.texName} from bundle for {texData.addressable}");
                return null;
            }
            return icon;
        }

        private void AddPingIconOverride(GameObject interactable, Sprite sprite)
        {
            PingInfoProvider pingProvider = interactable.AddComponent<PingInfoProvider>();
            pingProvider.pingIconOverride = sprite;
        }
    }
}
