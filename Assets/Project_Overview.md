# Skate Parkour Project Technical Overview

## 1. Project Description
**Skate Parkour** is a physics-based 3D skateboarding experience focused on fluid movement, momentum-based parkour, and environment navigation. The project is designed as a technical demonstration of custom character controller physics, featuring a unique "push and grip" mechanic that simulates the feel of skateboarding. Players navigate stylized environments, perform "Ollies" with a charge-up mechanic, and collect Diamond Coins while avoiding hazards.

## 2. Gameplay Flow / User Loop
The project follows a straightforward arcade-style loop:
1.  **Boot & Initialization:** The game starts in the main scene (e.g., `0.unity`), initializing the `SkateParkourController` and locking the cursor for mouse-driven orbit camera control.
2.  **Momentum Building:** The user pushes off using input (W), building horizontal velocity. Unlike standard walking controllers, the player must manage friction and "Grip" to maintain speed through turns.
3.  **Navigation & Trickery:** Players use the jump (Ollie) mechanic, which requires holding the jump button to "crouch" and releasing it to gain height based on charge time.
4.  **Hazard Recovery:** If the player falls out of bounds into a `KillZone`, they are instantly teleported back to a safe respawn position with their velocity reset.
5.  **Shutdown:** The user can unlock the cursor with Escape and exit the application.

## 3. Architecture
The project utilizes a **Component-Based Architecture** typical of Unity, with a heavy emphasis on the **Input System Package** for decoupled control logic.

*   **Centralized Input:** Uses `InputSystem_Actions.inputactions` to map physical hardware to logical actions like "Move", "Jump", and "Look".
*   **Physics Execution:** The `SkateParkourController` acts as the primary driver, processing input and manually applying movement to a `CharacterController` rather than relying on standard Unity Rigidbody physics.
*   **Decoupled Camera:** The camera logic is separated into an orbit system that tracks the player’s transform without being parented to it, preventing jitter and allowing for smooth rotation.

## 4. Game Systems & Domain Concepts

### Skateboard Movement System
A custom movement solver that simulates "skate physics" using vector math.
*   `SkateParkourController`: Manages horizontal velocity, friction, and "Grip" (the speed at which velocity aligns with the board's forward direction).
*   **Stance Logic**: Supports `Regular` and `Goofy` stances via an enum, affecting visual orientation.
*   **Braking & 180-Turns**: Includes a specific mechanic for braking (S) and a double-tap/cooldown-based logic for performing quick 180-degree rotations.
*   **Ollie Mechanic**: A state-based jump system where `isCrouching` scales the vertical impulse based on button hold duration.

`Location: Assets/Scripts`

### Orbit Camera System
A third-person camera system designed for high-speed tracking and manual orientation.
*   `SkateOrbitCamera`: Uses `LateUpdate` to calculate position based on a spherical coordinate system (Yaw/Pitch) relative to the player.
*   **Sensitivity & Clamping**: Provides exposed variables for mouse sensitivity and vertical angle limits to prevent gimbal lock.

`Location: Assets/Scripts`

### Hazard & Respawn System
A simple spatial trigger system for managing player failure states.
*   `KillZone`: Detects `CharacterController` entry and handles the specific "Disable-Teleport-Enable" workflow required to move controllers without physics interference.
*   **Velocity Reset**: Interfaces back with `SkateParkourController` to stop momentum upon respawn.

`Location: Assets/Scripts`

## 5. Scene Overview
*   **_Recovery/0.unity**: The primary active development scene. It contains the player prefab, environment geometry (likely ProBuilder-based), and the `KillZone` triggers.
*   **SampleScene.unity**: A default Unity scene used for testing or as a fallback.
*   **Scene Constraints**: The project relies on a `Main Camera` tagged object for the `SkateParkourController` to find its orientation reference.

## 6. UI System
The current implementation has minimal UI evidence in the source code, but the project is configured for **UGUI** (Unity UI).
*   **UI Structure**: The project is set up to support a `Canvas` and `EventSystem`, though the primary interaction is currently gameplay-focused via the `InputSystem`.
*   **Interaction**: No complex UI binding logic was found in the core scripts, suggesting UI is either purely decorative or yet to be implemented via a dedicated Manager.

## 7. Asset & Data Model
*   **Models & Prefabs**: Uses `.fbx` and `.glb` files for characters (`Skater.fbx`) and equipment (`Skateboard.fbx`). The `DiamondCoin.prefab` represents the primary collectible.
*   **Visual Container**: The player uses a specific `Visuals_Fix` transform as a child for stance-based rotations and "crouch" scaling without affecting the collision capsule.
*   **Input Actions**: The `InputSystem_Actions` asset serves as the single source of truth for all player controls.
*   **Materials**: Uses standard materials with Albedo/Normal/Roughness maps, likely targeting the Built-in Render Pipeline.

## 8. Notes, Caveats & Gotchas
*   **CharacterController Teleportation**: Do NOT use `transform.position` directly to move the player. You must disable the `CharacterController` component first, as shown in `KillZone.cs`, otherwise the move will be ignored or cause physics jitter.
*   **Visual Scaling**: The `SkateParkourController` applies a Y-axis scale to the `visualsContainer` to simulate a crouch. Ensure the visual child is not a parent to the camera or other logic-dependent objects.
*   **Camera Reference**: The controller caches `Camera.main`. If the scene contains multiple cameras, ensure the orbit camera is correctly tagged.
*   **Input Dependencies**: Scripts access actions via `InputSystem.actions.FindAction`. Renaming actions in the `.inputactions` asset will break script references unless updated in code.