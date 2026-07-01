# Cleanup Redundant Starting Zones Plan

## Project Overview
- **Goal:** Remove unnecessary duplicate starting platforms and consolidate into one central Hub (`StartingZone`).
- **Context:** Individual start platforms (Start_1B, 1C, 1D) were created but are redundant as they should branch from the main hub.

## Implementation Steps

### 1. Delete Redundant Start Platforms
- **Description:** Remove `Start_1B`, `Start_1C`, `Start_1D`, and `Start_Level2` from the scene.
- **Assigned role:** developer

### 2. Consolidate Hub Destinations in `LevelManager.cs`
- **Description:** Update the `level1B_Start`, `level1C_Start`, etc., positions to be locations on the main `StartingZone` platform.
- **Specific Positions (relative to Hub center 0.5, 0.35, 0.5):**
    - **1A (South):** `(0.5, 2.0, -20.0)` facing South.
    - **1B (West):** `(-20.0, 2.0, 0.5)` facing West.
    - **1C (East):** `(21.0, 2.0, 0.5)` facing East.
    - **1D (North):** `(0.5, 2.0, 21.0)` facing North.
- **Assigned role:** developer

### 3. Adjust First Platform Gaps
- **Description:** Ensure the first platform of each level (e.g., `Road_1A_1`, `Road_1B_1`) is a jumpable distance from the edge of the expanded hub if necessary. 
- **Current Stats:** Hub edge is ~25 units from center. `Road_1A_1` is at Z=-40.5. Gap = 15.5.
- **Adjustment:** Move `Road_1A_1` and `Road_1B_1` closer to the Hub (e.g., 10m gap from edge).
- **Assigned role:** developer

## Verification & Testing
- **Hierarchy Check:** Verify only one `StartingZone` exists (excluding the Level 2 separate area if intended to be far away).
- **Teleport Test:** Verify teleporting to Level 1B puts the player on the hub facing West.
- **Jump Test:** Verify the first jump of each level is clearable from the hub edge.
