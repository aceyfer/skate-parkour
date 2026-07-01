# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: A physics-based skating game where players navigate a parkour-style environment.
- Players: Single player.
- Target Platform: PC (StandaloneWindows64).
- Input System: New Input System.

# Game Mechanics
## Core Gameplay Loop
- Players push (W) to gain momentum, steer (A/D) to turn, jump (Space) to ollie, and navigate obstacles.
- Death occurs when falling into a KillZone, which respawns the player.

## Controls and Input Methods
- **W / Up**: Push (add forward velocity).
- **A/D / Left/Right**: Steering.
- **S / Down**: Brake.
- **Double S / Double Down**: Revert (180-degree turn and reverse direction).
- **Space**: Ollie.

# Key Asset & Context
- `Assets/Scripts/SkateParkourController.cs`: Main movement script.
- `Assets/Scripts/KillZone.cs`: Handles respawning and teleportation.

# Implementation Steps
## 1. Modify SkateParkourController.cs
- **Turning Speed**: Change default `turnSpeed` to 400 to balance responsiveness and realism.
- **Velocity Reset**: Implement a public `ResetVelocity()` method to clear `horizontalVelocity` and `verticalVelocity`.
- **Braking and Revert Logic**:
    - Update `HandleSkateMovement` to detect double-taps on the "S" key (negative Y input).
    - Implement the "Revert" behavior: rotate the transform 180 degrees and reverse the `horizontalVelocity` vector.
    - Ensure braking remains functional for a single hold.

## 2. Visual Polish
### Level Objects (Roads)
- **Road Texture**: Generate a seamless road texture (dark asphalt with a yellow center line).
- **Road Material**: Create a material using the road texture.
- **Environment Update**: Apply the Road Material to all platform cubes in the scene.

### Player and Skateboard
- **Skater Model**: Generate or update the skater model with Caucasian skin and blue/black clothing.
- **Skateboard Model**: Generate or update the skateboard with black griptape and a blue/black deck.
- **Material Assignment**: Ensure the new visuals are correctly applied to the player prefab/object.

# Verification & Testing
- **Turning Test**: Manually steer the player to verify the turning is more responsive.
- **Respawn Test**: Intentionally fall into a KillZone and verify the player is stationary upon respawn.
- **Braking Test**: Hold "S" while moving to verify the player slows down to a stop.
- **Revert Test**: Double-tap "S" while moving to verify the character flips 180 degrees and moves in the opposite direction.
