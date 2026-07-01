# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Realistic speed skateboarding parkour with a linear, speedrun-focused track.
- Goal: Lower skater speed for realism and straighten the entire course into a linear speedrun path (Level 1 up, Level 2 down).

# Game Mechanics
## Core Gameplay Loop
The player pushes to gain momentum and clears a sequence of rising and falling platforms in a straight line. Focus is on speed management and jump timing.

## Controls and Input Methods
- **WASD**: Movement/Pushing.
- **Space**: Chargeable Ollie.

# Key Asset & Context
- **SkateParkourController.cs**: Player physics.
- **Level1_LineRider**: Upward linear path.
- **Level2_Downwards**: Downward linear path.

# Implementation Steps
1. **Lower Skater Speed**:
   - Update `SkateParkourController.cs` and scene instance:
     - `maxSpeed`: 13.0f (Realistic cruising speed)
     - `pushForce`: 8.0f (Manageable acceleration)
     - Keep `jumpHeight`: 3.5f and `gravity`: -18.0f for stable flight.

2. **Straighten Level 1 (Ascent)**:
   - Align all platforms in `Level1_LineRider` to **Z = 0**.
   - Sequence them along the **X axis** starting from the player's current position.
   - Space them with horizontal gaps of ~10m-12m.
   - Height increments of 1.5m - 2.5m per platform.

3. **Straighten Level 2 (Descent)**:
   - Align all platforms in `Level2_Downwards` to **Z = 0**.
   - Sequence them along the **X axis** following Level 1's Goal.
   - Space them with horizontal gaps of ~12m-15m (since descent allows for more distance).
   - Height drops of 4m - 6m per platform.

4. **Update Environment**:
   - Re-center `DiamondCoins` on the new platform positions.
   - Re-bake the `NavMesh` so the Alien AI can navigate the new straight path.

# Verification & Testing
1. **Linear Path Check**: Ensure all platforms are perfectly aligned on the Z axis.
2. **Jump Test**: Verify that the 10m-15m gaps are clearable at the new 13m/s max speed.
3. **Alien Test**: Ensure the AI can skate the straight path without getting stuck.
