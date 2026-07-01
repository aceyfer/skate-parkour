# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Physics-based 3D skateboarding parkour with sub-level progression.
- Players: Single player.
- Target Platform: PC / Standalone.
- Render Pipeline: Built-in.

# Game Mechanics
## Core Gameplay Loop
The player must navigate platforms on a skateboard/hoverboard, building momentum and performing jumps. To progress through "Level 1", the player must collect a "Goal Coin" in each sub-level (1A, 1B, 1C, 1D). Collecting a Goal Coin unlocks the next sub-level's geometry and allows the player to use a "Platform Portal" to teleport to the next area.

## Controls and Input Methods
- WASD for movement (Pushing/Braking).
- Space for Jump (Ollie).
- Mouse for camera orbit.

# UI
- Fragment Counter: Displays collected fragments (Goal Coins).
- Choice Popup: Displays messages upon sub-level completion.

# Key Asset & Context
- `LevelManager.cs`: Controls the flow between sub-levels.
- `Coin.cs`: Goal coins that trigger sub-level completion.
- `PlatformPortal.cs`: Triggers that teleport the player between zones.
- `SkateParkourController.cs`: Main player physics controller.

# Implementation Steps
## Phase 1: Scene Cleanup & Verification
1. **Remove Duplicate Root**: Delete the less complete `Level1D_Root` (the one with only 9 children) to avoid confusion and potential logic conflicts.
   - Assigned role: developer
   - Dependencies: None
2. **Verify LevelManager References**: Ensure `level1BRoot`, `level1CRoot`, and `level1DRoot` are correctly assigned to the surviving root objects.
   - Assigned role: explorer
   - Dependencies: Step 1
3. **Investigate Portals List**: Determine if the `portals` list in `LevelManager` (currently containing nulls) is required for Level 1 progression. If it's intended for a level-select hub that isn't built yet, document it.
   - Assigned role: explorer
   - Dependencies: None

## Phase 2: Play Mode Testing
4. **Create Play Mode Test**: Implement `Assets/Editor/Level1TestRunner.cs` using the Bootstrap pattern to automate the verification of the entire Level 1 flow.
   - **Test Case 1A**: Teleport player to 1A Goal Coin -> Collect -> Verify `Level1B_Root` is active.
   - **Test Case 1A Portal**: Teleport player to 1A Portal -> Verify player position matches `level1B_Start`.
   - **Test Case 1B**: Teleport player to 1B Goal Coin -> Collect -> Verify `Level1C_Root` is active.
   - **Test Case 1B Portal**: Teleport player to 1B Portal -> Verify player position matches `level1C_Start`.
   - **Test Case 1C**: Teleport player to 1C Goal Coin -> Collect -> Verify `Level1D_Root` is active.
   - **Test Case 1C Portal**: Teleport player to 1C Portal -> Verify player position matches `level1D_Start`.
   - **Test Case 1D**: Teleport player to 1D Goal Coin -> Collect -> Verify sub-level is "DONE" or "Level 2".
   - **Test Case 1D Portal**: Teleport player to 1D Portal -> Verify player position matches `level2_Start`.
   - Assigned role: developer
   - Dependencies: Phase 1
5. **Execute Test**: Run the automated test and capture results.
   - Assigned role: developer
   - Dependencies: Step 4

# Verification & Testing
- **Automated Results**: The Play Mode test must report all sub-levels and portals as functional.
- **Manual Check**: Briefly enter Play Mode and verify that collecting the first coin activates the next platform visuals.
