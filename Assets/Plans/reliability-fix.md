# Level 1B/1C Reliability and Consolidation Plan

## Project Overview
- **Goal:** Fix non-working teleports for 1B/1C, resolve 1C visibility/distance issues, and clean up duplicate objects in the scene.

## Game Mechanics
### Consolidation & Cleanup
- **Remove Duplicates**: Delete the "ghost" `Level1B_Root` (the one with 18 children) and any other duplicate level roots.
- **Normalize Scale**: Ensure all root objects have scale `(1,1,1)`.

### Level 1C (East) Spacing Fix
- **Hub Proximity**: Move the first platform of Level 1C (`Road_1C_1`) closer to the Hub. 
    - Hub edge: X=25.5.
    - P1 position: X=35.5 (Gap: 10m).
- **Sequential Jumps**: Adjust all subsequent platforms to maintain 15m gaps (27m center-to-center).
- **Updated X positions**: 35.5, 62.5, 89.5, 116.5, 143.5, 170.5, 197.5, 224.5, 251.5 (Goal).

### Teleport Reliability
- **Portal Consistency**: Ensure `PlatformPortal` is on the final platform of EVERY course.
- **Logic Sync**: Ensure `CompleteSubLevel` and `TeleportToNext` handle state updates correctly.

## Implementation Steps

### 1. Scene Consolidation
- **Description**: Delete ghost roots and ensure only one `Level1A_Root`, `1B_Root`, `1C_Root`, and `1D_Root` exist.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Redesign 1C for Reachability
- **Description**: Reposition all 1C platforms to start 10m from the Hub and have 15m gaps.
- **Assigned role**: developer
- **Dependencies**: Step 1

### 3. Bulletproof Portals and Gating
- **Description**: 
    - Update `LevelManager.cs` to ensure `currentSubLevel` is always accurate.
    - Ensure `Road_1B_9` has the correct `PlatformPortal` and `GoalSafety`.
    - Reposition all Goal Fragments.
- **Assigned role**: developer
- **Dependencies**: Step 2

## Verification & Testing
- **Hub -> 1C Visibility**: Land on Hub and verify 1C is visible (once unlocked).
- **Teleport Loop**: Play from 1A -> 1B -> 1C -> 1D and verify every portal works.
- **Distance Check**: Verify the first jump to 1C is manageable.
