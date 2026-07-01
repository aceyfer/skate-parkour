# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Physics-based 3D skateboarding parkour game.
- Players: Single player
- Target Platform: PC

# Game Mechanics
## Core Gameplay Loop
The player builds momentum by pushing, performs Ollies (chargeable jumps) to navigate between platforms, and collects Diamond Coins while avoiding falling into the KillZone.

## Controls and Input Methods
- **WASD**: Movement and Pushing.
- **Space**: Hold to crouch, release to Ollie.
- **Mouse**: Orbit camera control.

# UI
- Minimalist HUD (currently none).

# Key Asset & Context
- **SkateParkourController.cs**: Manages player physics and input.
- **DiamondCoin**: Collectible coin found on platforms.
- **Level2_Downwards**: A series of descending platforms with significant gaps.

# Implementation Steps
1. **Fix Coin Visuals**:
   - The coins are currently children of platforms with non-uniform scales, causing them to stretch.
   - I will modify the `Coin.cs` script or use a command to unparent them at runtime (or move them to a global container) to maintain a consistent scale of `(1, 1, 1)`.
   
2. **Tune Physics for Course Playability**:
   - Current settings (`JumpHeight: 2.5`, `MaxSpeed: 12`, `Gravity: -20`) are too weak for 30m+ gaps.
   - I will update `SkateParkourController` to have stronger defaults: `JumpHeight: 12`, `MaxSpeed: 40`, `Gravity: -35`.
   
3. **Verify and Adjust Platforms**:
   - I will use a Play Mode test to simulate jumps across the Level 2 gaps.
   - If gaps remain unreachable with tuned physics, I will move platforms (e.g., `L2_Drop_13` to `L2_Drop_14` is 37m) closer together.

4. **Collect All Coins**:
   - Ensure the Play Mode test confirms the player can collide with and "collect" each coin on the course.

# Verification & Testing
- **Automated Play Mode Test**: Simulate a full run through Level 1 and Level 2, verifying coin collection and gap clearance.
- **Manual Verification**: Perform a manual play-through to ensure turning and jumping feel "correct".
