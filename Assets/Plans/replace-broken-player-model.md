# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: 3D physics-based skateboarding/hoverboarding.
- Players: Single player
- Target Platform: Standalone Windows
- Screen Orientation: Landscape
- Render Pipeline: Built-in

# Game Mechanics
- Core Gameplay Loop: Skate through stylized environments, building momentum and performing tricks.
- Controls: W/S for thrust/brake, A/D for steering, Space for Ollie charge.

# UI
- Minimal gameplay-focused UI.

# Key Asset & Context
- **Broken Model**: `Assets/Meshy_AI_Flat_shaded_ultra_low_0529153534_texture_fbx 1/Skater.fbx`
- **Replacement Model**: `Assets/Models/PlayerCharacter_Assets/selected.glb` (Detailed humanoid).
- **Intended Texture**: `Assets/Textures/Player_Texture_BlueBlack.png` (Blue and black apparel).
- **Player Prefab**: `Assets/Prefabs/Player_Hoverboard.prefab`
- **Controller**: `SkateParkourController.cs`

# Implementation Steps

## 1. Configure Replacement Model Importer
- **Description**: Set the Animation Type of `Assets/Models/PlayerCharacter_Assets/selected.glb` to **Humanoid** in the Model Importer. Create an Avatar for it.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 2. Create and Configure Player Character Material
- **Description**: Create a new material `Assets/Materials/PlayerCharacter_Mat.mat`. Assign `Assets/Textures/Player_Texture_BlueBlack.png` as the Albedo (Main Texture). Set Smoothness to a reasonable value (e.g., 0.5) and ensure it uses the Standard shader.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 3. Replace Player Mesh in Prefab
- **Description**: Open `Assets/Prefabs/Player_Hoverboard.prefab`.
    - Remove or disable the old `Skater_Mesh` (the one using the broken `Skater.fbx`).
    - Instantiate `selected.glb` under the `Visuals` container.
    - Rename the new mesh object to `Skater_Mesh` to maintain script compatibility.
    - Apply the `PlayerCharacter_Mat` to the new mesh.
- **Assigned role**: developer
- **Dependencies**: Step 1, Step 2
- **Parallelizable**: No

## 4. Update Animator Reference
- **Description**: Update the `Animator` component on the `Player_Hoverboard` prefab (or the new mesh child) to use the newly created Humanoid Avatar from `selected.glb`. Ensure the `PlayerAnimator` controller is still assigned.
- **Assigned role**: developer
- **Dependencies**: Step 3
- **Parallelizable**: No

## 5. Adjust Visual Offsets and Scaling
- **Description**: The new model may have a different default rotation or scale. Adjust the `selected.glb` transform within the prefab so it aligns correctly with the hoverboard. If the model is sideways, rotate it (likely -90 or 90 on X/Y). Update `SkateParkourController.cs` if the visual "lean" logic needs different axes for the new model.
- **Assigned role**: developer
- **Dependencies**: Step 3
- **Parallelizable**: No

# Verification & Testing
- **Visual Check**: Inspect the player in the Scene view to ensure the face is on the head and the apparel is blue/black as requested.
- **Animation Check**: Play the game and verify that Idle, Push, and Ollie animations still trigger correctly on the new model.
- **Movement Check**: Verify that the character still "leans" into turns and compresses during jumps.
