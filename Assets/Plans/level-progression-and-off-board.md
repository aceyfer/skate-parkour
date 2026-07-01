# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Linear speedrun levels (L1) and technical offset levels (L2).
- Key Feature: Completion triggers portals and unlocks levels; "Off-Board" state for precise navigation and learning.

# Game Mechanics
## Core Gameplay Loop
1.  **Skate/Run**: Navigate to the end of the level.
2.  **Complete**: Collect the Goal Coin to unlock the next level and open a portal back to Start.
3.  **Off-Board**: Toggle between skating and walking/sprinting for better control.

## Controls and Input Methods
- **F (Interact)**: Toggle Skateboard (On/Off).
- **Shift (Sprint)**: Sprint while off-board.
- **W/A/S/D**: Movement.
- **Space**: Ollie (On-Board) or Jump (Off-Board).

# Key Assets & Context
- **LevelManager.cs**: Central logic for level completion and portals.
- **Portal.cs**: Teleport the player back to the Starting Zone.
- **Coin.cs**: Modified to signal goal completion.
- **SkateParkourController.cs**: Updated with State Machine (Skating/Walking) and Slope Physics.

# Implementation Steps
1.  **Update Coin.cs**:
    - Add `isGoalCoin` boolean.
    - Trigger `LevelManager.CompleteLevel()` on collection.
2.  **Create LevelManager.cs**:
    - Track current unlocked level.
    - Spawn/Enable portals at goal platforms.
    - Handle Level 3 unlocking.
3.  **Create Portal.cs**:
    - Trigger teleportation using `CharacterController.enabled = false` pattern.
4.  **Update SkateParkourController.cs**:
    - Add `isOnBoard` state.
    - Implement `HandleWalkingMovement()` with `Sprint` support.
    - Implement `UpdateVisuals()` to hide/show board or change animations.
    - Improve `HandleSkateMovement()` to account for slope angles (simple normal-based adjustment).
5.  **Adjust Level 2 Layout**:
    - Offset platforms on Z-axis (e.g., +/- 10m).
    - Rotate some platforms to act as ramps (up to 20 degrees).
    - Reposition Goal Coin on `L2_Drop_14`.

# Verification & Testing
- **Visuals**: Board disappears/appears when toggled.
- **Movement**: Walking/Sprinting feels distinct from skating.
- **Completion**: Picking up the last coin enables the portal.
- **Teleport**: Entering the portal moves the player back to the starting zone correctly.
