# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Realistic physics-based skateboarding parkour.
- Goal: Revert to realistic skater physics and adjust the environment to match, ensuring playability without superhuman speed or gravity.

# Game Mechanics
## Core Gameplay Loop
The player builds momentum through pushing and navigates a sequence of platforms using jumps (Ollies). The difficulty comes from timing and precision rather than excessive speed.

## Controls and Input Methods
- **WASD**: Standard movement and pushing.
- **Space (Hold/Release)**: Chargeable Ollie.

# Key Asset & Context
- **SkateParkourController.cs**: The physics engine of the game.
- **Level1_LineRider**: Upward and horizontal platforming sequence.
- **Level2_Downwards**: Descending platform sequence.

# Implementation Steps
1. **Define and Apply Stable Realistic Physics**:
   - Update `SkateParkourController.cs` with the following "Stable Realistic" defaults:
     - `maxSpeed`: 18.0f
     - `pushForce`: 10.0f
     - `jumpHeight`: 3.5f
     - `gravity`: -18.0f
     - `turnSpeed`: 250.0f
     - `grip`: 10.0f
   - These values provide a "snappy" but believable skateboarding feel.

2. **Adjust Level 1 (LineRider) Platforms**:
   - Many platforms currently require jumps of 4m-6m vertically. These will be lowered to a maximum vertical step of 2.5m - 3.0m.
   - Adjust `HighDeck`, `SkyBridge`, `Peak`, `Approach`, and `Goal` heights and horizontal positions to maintain a challenging but possible flow.

3. **Adjust Level 2 (Downwards) Platforms**:
   - Level 2 is a descent. The primary adjustment will be horizontal spacing.
   - Gaps will be tuned to roughly 15m-18m horizontally to ensure they are clearable at medium-to-high speeds without requiring frame-perfect execution.

4. **Verify Alignment**:
   - Ensure `DiamondCoins` are still correctly positioned on the adjusted platforms.
   - Ensure platforms are rotated correctly to suggest the intended path.

# Verification & Testing
1. **Manual Playthrough**: Ensure each jump in Level 1 and Level 2 can be completed using a fully charged Ollie at reasonable speed.
2. **Feel Test**: Verify that the skater doesn't feel floaty or overly heavy, and that turning is responsive but not jittery.
