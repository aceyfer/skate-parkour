# Level Progression and Platform Safety Fix Plan

## Project Overview
- **Goal:** Fix teleportation logic for Level 1B and 1C, enforce sequential unlocking (1B -> 1C), and add permanent safety walls to the end of every sub-level.

## Game Mechanics
### Progression Flow
- **Sequential Unlocking**: 
    - `Level1B_Root` is revealed after Level 1A completion.
    - `Level1CRoot` is revealed only after Level 1B completion.
    - `Level1DRoot` is revealed only after Level 1C completion.
- **Teleportation**:
    - Final platforms (Road_1A_9, 1B_9, 1C_13, 1D_9) will have a `PlatformPortal` trigger.
    - Touching the platform *after* collecting the Goal Fragment will teleport the player to the next sub-level's start point on the Hub.

### Platform Safety
- **Walls**: Every final platform will have three invisible walls (Left, Right, Back).
- **Logic**: These walls will be managed by `GoalSafety.cs` and will activate as soon as the player lands on the platform.
- **Orientation**:
    - **1A (South)**: Back wall at -Z, side walls at +/-X.
    - **1B (West)**: Back wall at -X, side walls at +/-Z.
    - **1C (East)**: Back wall at +X, side walls at +/-Z.
    - **1D (North)**: Back wall at +Z, side walls at +/-X.

## Implementation Steps

### 1. Update `LevelManager.cs` logic
- **Description**: Ensure `CompleteSubLevel` correctly sets the next sub-level and activates the appropriate course root.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Configure Final Platforms
- **Description**: 
    - Add `PlatformPortal` and trigger colliders to `Road_1B_9` (currently missing).
    - Re-create `SafetyWalls` for all 4 goal platforms with consistent sizing (10m high).
- **Assigned role**: developer
- **Dependencies**: None

### 3. Setup `GoalSafety` Components
- **Description**: Attach `GoalSafety.cs` to all goal platforms and link the `safetyWalls` reference.
- **Assigned role**: developer
- **Dependencies**: Step 2

## Verification & Testing
- **1B -> 1C Sequence**: Complete Level 1B and verify Level 1C platforms appear.
- **Teleport Test**: Verify 1B portal teleports to 1C start on the hub.
- **Wall Test**: Land on any goal platform and verify you cannot fall off the sides or back.
