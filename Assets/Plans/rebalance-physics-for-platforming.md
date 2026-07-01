# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Precision platforming and momentum-based skateboarding.
- Goal: Rebalance the "Tony Hawk on Steroids" physics to be more playable for obstacle-to-obstacle navigation.

# Game Mechanics
## Physics Rebalancing
The current settings are too extreme (high speed, high gravity). This plan scales them back to a "Balanced Pro" level that favors precision over raw speed.

# Key Asset & Context
- **Scripts**: `SkateParkourController.cs`
- **Objects**: `Player` GameObject.

# Implementation Steps

## 1. Adjust Physics Settings on Player
- **Description**: Lower the extreme values to more manageable levels for platforming.
- **Values**:
    - `pushForce`: 12 (Moderate acceleration)
    - `maxSpeed`: 16 (Control over speed)
    - `turnSpeed`: 350 (Sharp but not twitchy)
    - `turnTorque`: 25 (Responsive)
    - `grip`: 30 (On-rails feel with minor leeway)
    - `jumpHeight`: 5.0 (Adequate for obstacles)
    - `gravity`: -22 (Natural fall speed)
    - `airControl`: 55 (Maximum precision in flight)
    - `friction`: 0.25 (Natural deceleration)
- **Assigned role**: developer
- **Dependencies**: None

## 2. Refine "Push and Turn" Interaction
- **Description**: Ensure the player can steer effectively while the push animation/force is active.
- **Action**: Check if the `turnTorque` or `turnSpeed` are being capped during pushing in the script. (Based on `SkateParkourController.cs` read previously, it seems `HandleSkateMovement` handles them concurrently, so values should be enough).
- **Assigned role**: developer
- **Dependencies**: Step 1

# Verification & Testing
- **Manual Test**: Jump from a platform to an obstacle on the left, then immediately turn to an obstacle on the right.
- **Manual Test**: Verify the "moon gravity" and "steroids speed" are gone.
- **Manual Test**: Ensure pushing feels rewarding but doesn't launch the player uncontrollably.
