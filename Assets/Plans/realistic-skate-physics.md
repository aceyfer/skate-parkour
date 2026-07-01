# Realistic Skateboard Physics & Animation Implementation

## Project Overview
- **Game Title**: Skate Parkour
- **High-Level Concept**: A physics-driven skateboarding experience with authentic animations, torque-based turning, and dynamic foot placement.
- **Players**: Single-player.
- **Tone**: Authentic skating feel (not standing straight, knees bent, responsive movement).

## Game Mechanics
### Core Gameplay Loop
The player navigates stylized environments using momentum. Success depends on timing pushes, carving through turns with lean, and landing tricks with proper stance.

### Physics & Animation Goals
- **Torque-Based Turning**: Instead of immediate rotation, turning applies torque, making the board feel like it has weight and inertia.
- **Spring Foot Follow**: Feet should dynamically follow the board with a "springy" feel, avoiding rigid attachment.
- **IK Foot Placement**: Feet should align with the board's surface and trucks using IK or simple raycast constraints.
- **Blend Tree Animation**: A proper animator setup for:
    - **Cruising**: Velocity-based lean and speed.
    - **Crouching/Ollie**: Smooth transition from ready stance to jump.
    - **Leaning**: Visual leaning in sync with turn torque.
- **Authentic Stance**: The skater should maintain a proper skating stance (low center of gravity, angled shoulders) rather than standing upright.

## Controls and Input Methods
- **Push (W)**: Additive force.
- **Steer (A/D)**: Applies torque for carving.
- **Brake/180 (S)**: Deceleration and pivot.
- **Ollie (Jump)**: Multi-stage jump (Crouch -> Launch -> Air -> Land).

## Key Assets & Context
- **Scripts/SkateParkourController.cs**: Update with torque and lean logic.
- **Scripts/SkateIKHandler.cs (New)**: Handles foot placement and board-alignment constraints.
- **Animations/PlayerAnimator.controller**: Update with Blend Trees (Velocity/Turn).
- **Skateboard Prefab**: Ensure trucks/wheels are accessible for IK targets.

## Implementation Steps
### Step 1: Torque and Lean Physics
- Update `HandleSkateMovement` to use torque-based rotation.
- Implement a `turnAmount` variable that smooths input and drives both rotation and animation parameters.
- **Dependencies**: None.

### Step 2: Advanced Animator Setup
- Modify `PlayerAnimator.controller` to include a 2D Blend Tree (Speed vs. Lean).
- Add states for `Pushing`, `Crouching`, and `Ollie`.
- Ensure the "Ready" stance is the base state (bent knees).
- **Dependencies**: Step 1.

### Step 3: Foot Placement & IK
- Create `SkateIKHandler.cs`.
- Use `OnAnimatorIK` to position feet relative to "Truck" points on the board.
- Implement a "Spring" follow logic so feet don't look glued to the mesh but have some micro-jitter/lag for realism.
- **Dependencies**: Step 2.

### Step 4: Stance & Visual Polish
- Adjust the default orientation of the skater model to match `Regular`/`Goofy` stances authentically.
- Add a tilt to the `visualsContainer` based on `turnAmount`.
- **Dependencies**: Step 3.

## Verification & Testing
- **Carving Test**: Check if the board rotates smoothly with torque and the animator leans correctly.
- **Stance Test**: Ensure the skater looks like a skater (low/ready) and not a mannequin.
- **IK Test**: Verify feet stay on the board during turns and jumps.
- **Animation Test**: Smooth transitions between cruising and pushing.
