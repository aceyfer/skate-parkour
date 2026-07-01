# Level 1B Gating and Fragment Economy Plan

## Project Overview
- **Game Title:** Skate Parkour
- **Goal:** Gate Level 1B access, replace Level 1A coins with a "Dimensional Fragment" on the final platform, and add a HUD counter.

## Game Mechanics
### Level Gating
- Level 1B platforms (`Level1B_Root`) will be hidden initially.
- Collecting the Dimensional Fragment on `Road_1A_9` will reveal Level 1B.

### Economy (Dimensional Fragments)
- **Removal:** All standard coins in Level 1A will be removed.
- **Dimensional Fragment:** Reuses the `DiamondCoin` model but functions as the primary progression currency.
- **Placement:** Only one Fragment is placed on the 9th platform of Level 1A.

### UI HUD
- **HUD:** A new screen-space Canvas in the top-right corner.
- **Counter:** Displays "Fragments: X".

## Implementation Steps

### 1. Update Scripts
- **Description**: Update `LevelManager.cs` to handle fragment counting, UI updates, and level spawning. Update `Coin.cs` to interact with this new system.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Create HUD UI
- **Description**: Create a HUD Canvas with a TextMeshPro counter in the top-right.
- **Assigned role**: developer
- **Dependencies**: None

### 3. Scene Refactoring
- **Description**: Remove Level 1A coins, set Level 1B to inactive, and configure the goal fragment.
- **Assigned role**: developer
- **Dependencies**: Step 1

## Verification & Testing
- **Economy Check**: Confirm Level 1A has no coins except the goal fragment.
- **UI Check**: Confirm the fragment counter is visible and updates on collection.
- **Progression Check**: Confirm Level 1B becomes visible only after collecting the 1A fragment.
