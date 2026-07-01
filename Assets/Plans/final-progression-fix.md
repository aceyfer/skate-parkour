# Final Level Progression and Goal Fix Plan

## Project Overview
- **Goal:** Fix non-working teleports for 1B and 1C by properly configuring the Goal Fragments and cleaning up duplicate objects.
- **Problem:** `GoalCoin_1B`, `1C`, and `1D` are missing the necessary `Coin` script. Duplicate `Road_1B_9` platforms are causing confusion.

## Game Mechanics
### Goal Fragments
- **Requirement**: Every sub-level must end with a `GoalCoin` that has the `Coin` script attached and `isGoalCoin` set to `true`.
- **Function**: Collecting this coin calls `LevelManager.CompleteSubLevel()`, which sets `goalMet = true` and reveals the next level's root.

### Teleportation
- **Requirement**: The final platform must have a `PlatformPortal` component and a large trigger collider.
- **Function**: Teleports the player to the Hub when `goalMet` is true.

## Implementation Steps

### 1. Script Fixes for Goal Coins
- **Description**: Attach the `Coin` script to `GoalCoin_1B`, `GoalCoin_1C`, and `GoalCoin_1D`. Set `isGoalCoin = true` and assign the collection sound.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Scene Cleanup (Level 1B)
- **Description**: Delete the duplicate `Road_1B_9` at `(-216.5, 5.5, 0.5)`. The correct one is at `(-202.5, 5.5, 0.5)`. 
- **Description**: Delete the duplicate `GoalCoin_1B` at `(-211, 7.5, 0.5)`. The correct one is at `(-202.5, 7, 0.5)`.
- **Assigned role**: developer
- **Dependencies**: None

### 3. Portal and Collider Audit
- **Description**: Ensure `Road_1B_9` and `Road_1C_9` both have the `PlatformPortal` component and a standardized 15m trigger collider.
- **Assigned role**: developer
- **Dependencies**: Step 2

### 4. Level Manager Logic Check
- **Description**: Ensure `LevelManager.currentSubLevel` correctly advances during teleportation.
- **Assigned role**: developer
- **Dependencies**: None

## Verification & Testing
- **1B Progression**: Complete 1B, collect the fragment, and verify teleportation to 1C start.
- **1C Visibility**: Verify 1C is visible immediately after 1B completion.
- **Console Check**: Monitor logs for "Goal met!" and "Teleporting..." messages.
