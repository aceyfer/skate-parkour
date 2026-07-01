# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: A physics-based 3D skateboarding / hoverboarding experience focused on fluid movement and parkour navigation.
- Players: Single player
- Target Platform: Standalone Windows
- Screen Orientation: Landscape
- Render Pipeline: Built-in

# Game Mechanics
- Core Gameplay Loop: Navigate environments, build momentum, perform jumps (Ollies), and collect items while avoiding hazards.
- Controls: W/S for acceleration/braking, A/D for steering, Space for Ollie charge and jump.

# UI
- Minimal UI currently; focus is on gameplay physics and character visuals.

# Key Asset & Context
- **Player Model**: `Assets/Meshy_AI_Flat_shaded_ultra_low_0529153534_texture_fbx 1/Skater.fbx`
- **Current Material**: `Assets/Materials/UltraDetailed_Skater_Mat.mat` (Currently uses a misaligned AI-generated texture).
- **Corrected Material**: `Assets/Textures/Player_Mat.mat` (Uses `Player_Texture_BlueBlack.png` which is intended to fix UV mapping issues).
- **Detail Texture**: `Assets/Textures/PlayerFace_Detail.png` (Likely intended for high-resolution face details).
- **Controller Scripts**: `SkateParkourController.cs` and `HoverboardController.cs`.

# Implementation Steps

## 1. Fix Player Material Assignment
- **Description**: The player character in the scene (`Player_Hoverboard -> Visuals -> Skater_Mesh_Detailed`) is currently using `UltraDetailed_Skater_Mat` which has a misaligned texture ("face on arm"). We will switch it to use `Player_Mat` which uses the corrected `Player_Texture_BlueBlack` texture.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 2. Configure Player_Mat with Detail Face Texture
- **Description**: Update `Player_Mat` to include `PlayerFace_Detail.png` in the **Detail Albedo x2** slot of the Standard shader. This ensures the face has the intended high-resolution detail. Set the Detail tiling to (1, 1) and ensure the shader keywords for detail mapping are active.
- **Assigned role**: developer
- **Dependencies**: Step 1
- **Parallelizable**: Yes

## 3. Fix Visuals Transform Reference
- **Description**: `SkateParkourController` attempts to find a child named `Skater_Mesh`, but the scene object is named `Skater_Mesh_Detailed`. This prevents the "Lean" logic from working. Rename `Skater_Mesh_Detailed` to `Skater_Mesh` in the scene hierarchy.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 4. Update SkateParkourController for Robustness
- **Description**: Modify `SkateParkourController.cs` to use a more robust way to find the skater mesh if `Skater_Mesh` is missing, or update the reference to `Skater_Mesh_Detailed`. 
- **Assigned role**: developer
- **Dependencies**: Step 3
- **Parallelizable**: Yes

# Verification & Testing
- **Manual Check**: Inspect the player model in the Scene view to ensure the face is correctly positioned on the head and not on the arm.
- **Visual Check**: Verify that `PlayerFace_Detail` is correctly overlaying and not creating tiling artifacts.
- **Logic Check**: Test movement in Play Mode to ensure the character "Leans" into turns (if `SkateParkourController` is active).
