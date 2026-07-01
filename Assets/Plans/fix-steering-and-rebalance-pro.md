# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Precision platforming and momentum-based skateboarding.
- Goal: Fix "impossible" turning while moving/pushing and rebalance physics from "Steroids" to "Pro Skater" levels.

# Game Mechanics
## Steering Fix
The current steering logic suffers from a "circular fight" where the momentum is rotated, but then immediately pulled back to the old board direction by the `grip` logic before the board can update its orientation.

**New Steering Logic**:
1. Rotate the **Transform** based on steering input.
2. Rotate the **horizontalVelocity** by the same amount to keep it in sync with the board.
3. Apply `grip` to align velocity with the *new* forward direction.

## Physics Rebalancing ("Pro Skater" Edition)
Scaling back the extreme "moon" physics to something that feels fast but controllable, inspired by early-to-mid game THPS stats.

# Key Asset & Context
- **Scripts**: `SkateParkourController.cs`
- **Objects**: `Player` GameObject.

# Implementation Steps

## 1. Fix Steering Logic in SkateParkourController.cs
- **Description**: Update `HandleSkateMovement` to rotate the transform and velocity together.
- **Action**:
    - Modify the grounded steering block to rotate `transform` directly.
    - Synchronize `horizontalVelocity` rotation with the transform.
    - Ensure `grip` uses the updated forward direction.
- **Assigned role**: developer
- **Dependencies**: None

## 2. Rebalance Physics Settings
- **Description**: Apply "Pro Skater" level values to the `Player` object.
- **Values**:
    - `pushForce`: 10.0
    - `maxSpeed`: 14.0
    - `turnSpeed`: 280.0
    - `turnTorque`: 18.0
    - `grip`: 20.0
    - `jumpHeight`: 4.2
    - `gravity`: -20.0
    - `airControl`: 35.0
    - `friction`: 0.2
- **Assigned role**: developer
- **Dependencies**: None

## 3. Reduce Air Turn Speed
- **Description**: Sightly reduce the turn multiplier in the air as requested.
- **Action**: Change the `0.5f` multiplier in the air control block to `0.3f`.
- **Assigned role**: developer
- **Dependencies**: None

# Verification & Testing
- **Manual Test**: Confirm you can turn sharply while holding 'W' (Pushing).
- **Manual Test**: Verify the "moon gravity" is gone and jumping feels more weighted.
- **Manual Test**: Ensure air turning is responsive but not as "steroid-fast" as before.
