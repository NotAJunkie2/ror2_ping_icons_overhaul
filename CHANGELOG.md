## 1.3.2

This smaller patch focuses on structural code improvements, enhanced code modularity and some minor adjustments to facilitate ongoing mod development.

- Replaced multiple ``OverrideIconsFor...OnStageStart()`` methods with a generic templated method ``OverridePingIconsForObjects<T>()``.
- Added dedicated method ``OverridePingIconsForTeleporters()`` to handle teleporters and ``TeleporterInteraction`` separetely, as they require different management than other interactables.
- Reduced code nesting for improved readability.
- Refactored several methods to increase modularity.
- Renamed some methods and variables to maintain consistent naming conventions.

### Docs

- Introduced **Table of Contents** in the README.
- Introduced **Development Tools** section.
- Introduced **Credits & Thanks** section.
- Added collapsable menus for tables with more than 2 items to improve README readability.

### Bug Fixes

- Teleporter and Lunar Teleporter now display correct icons, thanks to the new ``OverridePingIconsForTeleporters()`` method.

## 1.3.1

- Forgot to include the DLLs in the previous release — this update makes sure everything is correctly packaged for Thunderstore so the mod actually works.
- Huge thanks to **SuDmit** from the RoR2 Modding Discord server for pointing it out!
- Patched README issue - Crashed Multishop icon was not rendering properly.

### Known Issues

- Skill issue strikes again. :(

## 1.3.0

- Added icons for **Shrines** category.
- Feedback welcome! If you have suggestions or thoughts about the shrine icons—or any other icons in the mod—feel free to share them.

## 1.2.0

- Added icons for **Skill related** category.
- Now checking for ``IDisplayNameProvider`` and adding one if needed, to ensure interactables like **Quantum Tunnel** display icons correctly (unfortunately, this doesn't fix the issue with **Log Pickup**).

## 1.1.0

- Added icons for **Charging Zones** category.

### Bug Fixes

- Legendary Chest on **Abyssal Depths** now displays correct icon.
- Artifact Pickup now displays correct icon.

### Dev Tools

- Pressing ``\`` (Backslash) now logs all tracked InstanceTracker entries to the console, to aid with debugging interactables.

### Known Issues

- **Teleporter** (normal and primordial) icon is not displayed - currently investigating correct implementation.
- **Log Pickup** is still not displaying the correct icon.

## 1.0.2

- README updated for Thunderstore, hopefully for sure.
- Moved icon initialization to ``Start`` method.

## 1.0.1

- Bumped up to 1.0.1 because I messed up the manifest when uploading it to Thunderstore 💀.
- Restructured Thunderstore folder hierarchy a bit.
- Hopefully repaired Thunderstore README.

### Known Issues

- Skill issue.

## 0.4.1

- Replaced hard-coded addressable strings with constants from ``RoR2BepInExPack.GameAssetPaths``.
- Improved `AddPingIconOverride`: now checks for an existing `PingInfoProvider` before adding a new one, and updates the sprite if already present.
- Encrusted Lockbox now correctly displays its ping icon (due to the `AddPingIconOverride` update).
- Logger is now properly initialized in `Awake()`, ensuring messages display correctly in the debug console.
- First release on Thunderstore!

### Known Issues

- Artifact pickups do not display their ping icons yet.

## 0.4.0

- Added icons for **Chests** category.

## 0.3.0

- Added icons for **3D Printers** and **Scrapper** categories.

## 0.2.0

- Added icons for **Pickups** category.
- Added **Artifact Pickup** icon.

## 0.1.0

- Added icons for **Barrel** category.
