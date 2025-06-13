## 1.0.1

- Bumped up to 1.0.1 because I messed up the manifest when uploading it to Thunderstore 💀
- Restructured Thunderstore folder hierarchy a bit.
- Hopefully repaired Thunderstore README

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
