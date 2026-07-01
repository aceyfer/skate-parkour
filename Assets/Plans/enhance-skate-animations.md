# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Arcade-style skateboarding with momentum and trick mechanics.
- Players: Single player.
- Art Direction: Stylized.
- Inspiration: Tony Hawk's Pro Skater (THPS).

# Game Mechanics
## Core Gameplay Loop
The player skates, performs Ollies, and navigates environments using highly responsive controls.
## Animation Goals
- Remove "stiffness" from the skater and board.
- Implement dynamic leaning for the character.
- Implement procedural "carving" tilt for the board.
- Add landing impact and jumping "pop" visuals.

# Key Asset & Context
- **Scripts**:
    - `SkateParkourController.cs`: The main script to update for procedural animations.
- **Animations**:
    - `PlayerAnimator.controller`: Needs Blend Tree correction.
    - `Skate_Lean_Left 1.anim`, `Skate_Lean_Right 1.anim`: To be used in the animator.

# Implementation Steps

## 1. Fix Animator Blend Tree
- **Description**: Update `PlayerAnimator`'s `Locomotion` Blend Tree.
- **Action**: 
    - Replace the 0-length lean clips with `Skate_Lean_Left 1` and `Skate_Lean_Right 1`.
    - Set `Skate_Lean_Left 1` at `(-1, 0)`.
    - Set `Skate_Lean_Right 1` at `(1, 0)`.
    - Set `Skate_Ready` at `(0, 0)`.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 2. Enhance Procedural Board Movement
- **Description**: Modify `SkateParkourController.cs` to separate skater and board rotations.
- **Action**:
    - Update `ApplyVisuals()` to apply a "Roll" rotation to `skateboardMesh` based on `turnAmount` (simulating truck lean).
    - Apply a "Lean" rotation to `skaterMesh` (the character) that is more dramatic than the board's roll.
    - Add a "Pitch" effect to the board during Ollies (pop).
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 3. Implement Landing & Impact Visuals
- **Description**: Add code to detect landing and apply a procedural "squat."
- **Action**:
    - Track the transition from `!isGrounded` to `isGrounded`.
    - Apply a temporary vertical offset and scale reduction to the character mesh on impact.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 4. Verification & Testing
- **Description**: Test in Play Mode to verify fluid movement.
- **Manual Check**: Ensure turning left/right shows both character leaning and board carving.
- **Manual Check**: Verify the board "pops" its nose up when starting an Ollie.
- **Manual Check**: Check that landing from a high fall has a visible "compression" effect.
