# Level 1A Layout Fix & Validation Plan

## Project Overview
- **Goal:** Fix the Level 1A platform layout to be challenging but possible at full speed (10m/s).
- **Sub-level:** 1A (South direction, -Z).
- **Constraints:** Max Jump distance at 10m/s is 11.2m (max charge) or 8.9m (tap).
- **Gap Target:** 10m (empty space).

## Implementation Steps

### 1. Fix Level 1A Platform Layout
- **Description**: Reposition, rescale, and rerotate platforms `Road_1A_1` to `Road_1A_9`.
- **Properties**:
    - Scale: (10, 0.5, 12) for all.
    - Z Positions: Start at -40.5, increment by -22 (12m length + 10m gap).
    - Y Positions: Start at 1.5, increment by 0.5.
    - X Positions: Zig-zag between 0.5, 2.5, -1.5.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Consolidate Goal Coins
- **Description**: Delete extra Goal Coins and place one on `Road_1A_9`.
- **Assigned role**: developer
- **Dependencies**: Step 1

### 3. Play Mode Validation Test
- **Description**: Create an automated test to drive the player from the Hub to the end of Level 1A.
- **Verification**: Ensure the player reaches `Road_1A_9` without falling.
- **Assigned role**: developer
- **Dependencies**: Step 1, 2

## Verification & Testing
- **Visual Validation**: Check platform spacing and rotations in Scene View.
- **Play Mode Test**: The automated test script will report success if the Goal Coin is collected or the final platform is reached.
