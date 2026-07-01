# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Physics-based 3D skateboarding movement with parkour elements.
- Players: Single player
- Target Platform: PC (StandaloneWindows64)
- Render Pipeline: Built-in

# Game Mechanics
## Rotation & Alignment
- **Board Alignment**: The skateboard's rotation will be locked to the direction of its horizontal velocity using `Rigidbody.MoveRotation` and `Quaternion.LookRotation`.
- **Slope Handling**: The rotation will also align with the ground normal if grounded, ensuring the board stays flat on slopes while facing the movement direction.
- **Character Stance**: The character will be adjusted to face the same direction as the skateboard (0-degree base rotation).
- **Steering**: Steering input will influence the velocity direction directly.

## Pushing Mechanics
- **Contextual Pushing**: When the player is grounded, moving slow, and pressing forward, the push animation will be triggered.
- **Automatic Pushing**: While moving forward, the player will automatically perform a push every few seconds to maintain momentum.

# Key Asset & Context
- `SkateParkourController.cs`: The main script to be modified.
- `CharacterController` & `Rigidbody`: Components on the Player object.
- Animator Parameters: `Velocity`, `Push` (trigger).

# Implementation Steps
1. **Prepare Controller**:
    - Add a `Rigidbody` reference and cache it in `Awake`.
    - Add fields for `autoPushInterval` (e.g., 3.0s) and `slowPushThreshold` (e.g., 2.0m/s).
    - Add `lastAutoPushTime` to track timing.

2. **Refactor Steering & Rotation**:
    - Modify `HandleSkateMovement` to rotate the `horizontalVelocity` vector based on input instead of rotating the `transform`.
    - Replace `transform.Rotate` calls with `rb.MoveRotation(Quaternion.LookRotation(horizontalVelocity))` in the movement loop.
    - Ensure rotation only updates when `horizontalVelocity.sqrMagnitude > 0.001f`.

3. **Update Visuals**:
    - In `ApplyVisuals`, change `baseAngle` to 0 so the character faces the same way as the board.

4. **Implement Auto-Push**:
    - In `HandleSkateMovement`, add logic to check if the player is grounded and pressing forward (`input.y > 0`).
    - If `magnitude < slowPushThreshold`, trigger the "Push" animation.
    - If `Time.time > lastPushTime + autoPushInterval`, apply a push force and trigger the "Push" animation.

5. **Integrate with Existing Mechanics**:
    - Ensure the "Grip" logic doesn't conflict (since velocity and transform forward are now locked, Grip becomes a no-op or a simple magnitude maintainer).
    - Maintain slope adjustment logic.

# Verification & Testing
- **Test Alignment**: Verify the board always points exactly where it's moving, even when sliding or turning.
- **Test Stance**: Verify the character is facing forward.
- **Test Auto-Push**: Check if the character pushes automatically every few seconds while holding W.
- **Test Low Speed**: Verify the push animation triggers when starting from a standstill.
- **Test 180-Turns**: Verify the quick 180-degree turn still works (it should flip the velocity and the rotation will follow).
