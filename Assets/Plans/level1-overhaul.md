# Level 1 Overhaul (1A - 1D) Plan

## Project Overview
- **Goal:** Restructure Level 1 into four sub-levels (1A, 1B, 1C, 1D) branching from a central hub.
- **Directional Mapping:**
    - Level 1A: South (-Z direction)
    - Level 1B: West (-X direction)
    - Level 1C: East (+X direction)
    - Level 1D: North (+Z direction)
- **Level 1A Design:** 9 platforms requiring steering (offsets/rotations), reachable with current hoverboard stats.
- **Progression:** The 9th platform of 1A has a collectible that triggers a UI popup to teleport to 1B.

## Game Mechanics
### Progression & UI
- **Choice UI:** A screen-space canvas with a prompt (e.g., "Level 1A Complete! Teleport to Level 1B?") and "Yes/No" buttons.
- **Collectible Integration:** `Coin.cs` will trigger the UI popup instead of immediate level completion for sub-levels.
- **Teleportation:** Clicking "Yes" teleports the player to the next sub-level's starting platform.

### Level 1A Layout (South)
- **Platforms:** 9 road-style platforms starting from the Hub.
- **Challenge:** Zig-zagging paths (X offsets) and rotations to force turning.
- **Reachability:** Gaps set to ~7-8 units (max jump is ~11 at full speed).

## Key Assets & Context
- `ChoicePopup.cs`: Manages the UI display and button callbacks.
- `LevelManager.cs`: Updated to track sub-level progress.
- `Road_1A_1` through `Road_1A_9`: Renamed and repositioned platforms.
- `Level1B_Start`, `Level1C_Start`, `Level1D_Start`, `Level2_Start`: New destination transforms.

## Implementation Steps

### 1. Update Scripts
- **File:** `Assets/Scripts/LevelManager.cs`
- **Description:** Add `CompleteSubLevel` method and destination targets for 1B, 1C, 1D, and Level 2.
- **File:** `Assets/Scripts/Coin.cs`
- **Description:** Update trigger to detect `HoverboardController` and call `ShowChoicePopup` on the 9th platform coin.
- **File:** `Assets/Scripts/ChoicePopup.cs` (New)
- **Description:** Handle UI logic for teleportation choice.

### 2. Create UI Prefab
- **Description:** Create a UI Canvas with a "Yes/No" box matching the game's cyan/black theme.

### 3. Redesign Level 1A (South)
- **Description:** Create 9 road platforms heading -Z.
- **Steps:**
    1. Place `Road_1A_1` near the hub.
    2. Sequentially place 1A_2 to 1A_9 with varying X offsets and Y rotations.
    3. Ensure gaps are reachable at full speed.
    4. Move the `Goal` coin to `Road_1A_9`.

### 4. Setup Hub and Directions
- **Description:** Repurpose existing platforms to form the 1B, 1C, 1D starts.
- **Positions:**
    - 1A Start: South (-Z)
    - 1B Start: West (-X)
    - 1C Start: East (+X)
    - 1D Start: North (+Z)

### 5. Final Integration & Testing
- **Description:** Link buttons to the teleport logic and run a full speed test of Level 1A.

## Verification & Testing
- **Control Test:** Verify W acceleration and steering feel right on the new path.
- **Jump Test:** Verify all 9 gaps are jumpable at max speed.
- **UI Test:** Verify coin collection triggers popup and "Yes" teleports the player.
- **Progression Test:** Verify 1A unlocks/leads to 1B.
