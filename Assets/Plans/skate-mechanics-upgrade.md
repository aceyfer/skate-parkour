# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Skateboard parkour game with realistic physics and mechanics.
- Players: Single player

# Game Mechanics
## Core Gameplay Loop
The player navigates platforms, collecting Diamond Coins, using advanced skating mechanics like ollies and stance-based pushes.

## Controls and Input Methods
- **Move (WASD)**: 
  - W: Push (Animation-based logic)
  - S: Brake/Revert
  - A/D: Turn (Grip-based physics)
- **Jump (Space)**: 
  - Hold: Crouch (Prepare Ollie)
  - Release: Perform Ollie (Height based on hold time)
- **Stance**: Regular/Goofy settings.

# UI
- Coin Counter (to be implemented later).

# Key Asset & Context
- **SkateParkourController.cs**: Main movement script.
- **Coin.cs**: New script for collectables.
- **DiamondCoin**: Asset to be distributed across platforms.

# Implementation Steps
1. **Update SkateParkourController**:
   - Add `Stance` enum and setting.
   - Implement "Hold to Crouch, Release to Jump" logic.
   - Improve turning physics to reduce "sliding" (interpolate velocity towards forward vector).
   - Add `isCrouching` and `pushAnimation` flags (logic only for now as no animator is present, but will be ready).
   
2. **Implement Coin System**:
   - Create `Coin.cs` script with `OnTriggerEnter` logic.
   - Ensure `DiamondCoin` has a `SphereCollider` (Trigger).
   
3. **Distribute Coins**:
   - Place a `DiamondCoin` on every platform in `Level1_LineRider` and `Level2_Downwards` except the Start zone.
   
4. **Verify Gaps & Jumps**:
   - Check all gaps.
   - Adjust platform positions if the new physics (Ollie height/speed) make them impossible.
   - Perform a Play Mode test to verify collectability.

# Verification & Testing
1. **Ollie Test**: Verify that holding space makes the character "crouch" (visualized by scaling for now) and releasing triggers the jump.
2. **Turning Test**: Verify that turning feels more "grounded" and less like drifting.
3. **Coin Test**: Collect coins on each platform.
4. **Course Test**: Ensure every platform is reachable.
