# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Realistic physics-based skateboarding parkour with a "Broken Dimension" theme.
- Goal: Upgrade player movement to include torque-based turning, visual spring follow, and proper animator blend trees for stances and leaning.

# Game Mechanics
## Core Gameplay Loop
The player skates through linear and technical courses, building momentum and performing timed jumps.

## Controls and Input Methods
- **WASD**: Movement and Pushing.
- **Space**: Chargeable Ollie.
- **F**: Toggle Board.

# Key Asset & Context
- **SkateParkourController.cs**: The main movement script to be upgraded.
- **HumanoidPlayerModel**: A new humanoid character to replace the current static mesh.
- **PlayerAnimator.controller**: Upgraded with a 2D Blend Tree.

# Implementation Steps
1. **Generate Humanoid Player Model**:
   - Use `Tripo P1` to generate a humanoid male character (stylized human abducted from Earth).
   - Use `Tripo Rigging` to add a humanoid skeleton.
   - Replace the current `Skater_Mesh` with this new rigged model.

2. **Upgrade SkateParkourController.cs**:
   - **Torque Turning**: Replace direct rotation with a physics-inspired angular velocity system.
   - **Spring Follow**: Implement a spring-damper system for the `visualsContainer` so it lags and snaps back when turning/moving.
   - **Lean Logic**: Calculate a `leanAngle` based on turning intensity and speed to feed into the animator.
   - **Stance Logic**: Offset the character's idle/skating stance to be sideways (skate stance) rather than facing forward.

3. **Develop Advanced Animator**:
   - Create a 2D Blend Tree with parameters: `Speed` (cruising speed) and `Lean` (turning intensity).
   - Add animations for: Skating Idle, Pushing, Cruising (leaning left/right), Crouching, and Ollie Pop.

4. **Implement Foot Placement (Simple IK)**:
   - Use `OnAnimatorIK` to align the feet with the board's surface if using a humanoid avatar.

# Verification & Testing
1. **Torque Test**: Verify that the board has rotational inertia and doesn't snap instantly.
2. **Visual Spring Test**: Check if the character model has a slight, natural delay when the board rotates.
3. **Stance/Lean Test**: Ensure the animator correctly reflects the speed and turn direction through the blend tree.
4. **IK Test**: Check that feet stay locked to the board deck.
