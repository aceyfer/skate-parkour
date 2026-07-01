# Level 1B Transition and Physics Fix Plan

## Project Overview
- **Goal:** Resolve teleportation failure, implement "vacuum" collectible behavior, and fix the initial "super bounce" at game start.
- **Direction:** Shift from choice-based teleportation to a seamless platform-triggered teleport with a "Level Unlocked" notification.

## Game Mechanics
### Collectibles (Vacuum Behavior)
- **Script:** `Coin.cs`
- **Logic:** When the player is within a certain distance (e.g., 5-10 units), the coin will lerp its position towards the player. This ensures the player collects the item even if they just barely land on the platform.

### Teleportation (Platform Trigger)
- **Mechanism:** The final platform (`Road_1A_9`) will act as a portal.
- **Workflow:** 
    1. Player collects the goal coin.
    2. A "Level 1B Unlocked" notification appears (Top of screen).
    3. Touching the 9th platform (the whole surface) triggers the teleport to `level1B_Start`.
- **Face Direction:** Ensure teleportation sets the player's rotation to face the level direction (West for 1B).

### Physics (Initial Bounce Fix)
- **Issue:** Player bounces into the sky immediately on Play Mode start.
- **Fix:** 
    - Initialize `verticalVelocity` to 0.
    - Add a "cooldown" or "warmup" period (e.g., 0.1s) where the hover force is ignored or heavily damped.
    - Ensure `CharacterController` is grounded properly at start.

## Implementation Steps

### 1. Update `Coin.cs`
- **Description**: Implement the vacuum effect in `Update`.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Update `LevelManager.cs`
- **Description**: 
    - Add a method to show a simple UI notification.
    - Add a method to teleport without a choice popup.
- **Assigned role**: developer
- **Dependencies**: None

### 3. Create `PlatformPortal.cs`
- **Description**: A trigger script for the 9th platform that calls `LevelManager.TeleportToNext()`.
- **Assigned role**: developer
- **Dependencies**: Step 2

### 4. Fix `HoverboardController.cs` Physics
- **Description**: Prevent the initial bounce by damping the hover spring at start-up.
- **Assigned role**: developer
- **Dependencies**: None

### 5. Scene Setup
- **Description**: 
    - Add the `PlatformPortal` component to `Road_1A_9`.
    - Ensure `Road_1A_9` has a Trigger collider covering its top.
- **Assigned role**: developer
- **Dependencies**: Step 3

## Verification & Testing
- **Bounce Test**: Start Play Mode and ensure the player stays on the ground without a random jump.
- **Vacuum Test**: Land on the edge of the 9th platform and verify the coin flies to the player.
- **Teleport Test**: Touch the 9th platform after collecting the coin and verify you teleport to Level 1B facing West.
