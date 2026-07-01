# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Physics-based 3D skateboarding/hoverboarding experience.
- Player Model: High-poly "UltraDetailed" character with specific color and skin requirements.
- Problem: The player model colors are corrupted ("mixed grey") and positioning is incorrect.

# Game Mechanics
## Core Gameplay Loop
- Navigating stylized levels with a hoverboard assistant.
- Performing momentum-based tricks and clear obstacles.

# UI
- HUD for movement and build mode.
- Build Cursor for AI assistant commands.

# Key Asset & Context
- `Player_Hoverboard`: Main player object in `_Recovery/0.unity`.
- `Skater_Mesh_Detailed`: The high-poly mesh (`Mesh_0`) used for the skater.
- `UltraDetailed_Skater_Mat`: The material currently applied to the skater.
- `Player_Texture_BlueBlack.png`: The correctly mapped texture sheet for blue/black clothing and Caucasian skin.
- `Meshy_..._texture_normal.png`: Original normal map for surface detail.

# Implementation Steps

## 1. Repair Skater Material Textures
- **Description**: Restore the correct UV-mapped textures to `UltraDetailed_Skater_Mat`.
- **Details**:
    - Set **Main Texture** to `Assets/Textures/Player_Texture_BlueBlack.png`.
    - Set **Normal Map** to `Assets/Meshy_AI_Flat_shaded_ultra_low_0529153534_texture_fbx 1/Meshy_AI_Flat_shaded_ultra_low_0529153534_texture_normal.png`.
    - Set **Metallic Map** to `Assets/Meshy_AI_Flat_shaded_ultra_low_0529153534_texture_fbx 1/Meshy_AI_Flat_shaded_ultra_low_0529153534_texture_metallic.png`.
    - Set **Base Color** to Pure White (1,1,1,1) to ensure the texture isn't tinted grey.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 2. Refine Material Appearance
- **Description**: Adjust physical properties to resolve the "downfall" in visual quality.
- **Details**:
    - Ensure `_Glossiness` (Smoothness) is around 0.5 for realistic skin/fabric balance.
    - Enable `_NORMALMAP` and `_METALLICGLOSSMAP` keywords.
- **Assigned role**: developer
- **Dependencies**: Step 1
- **Parallelizable**: Yes

## 3. Correct Skater Positioning and Stance
- **Description**: Align the skater model vertically and rotate them for a proper skate stance.
- **Details**:
    - Stand the skater **Upright**: Local Rotation `(90, 90, 0)` (adjusted for model coordinate space).
    - Position feet **Flush on the Board**: Local Position `(0, 0.12, 0)` (approximate based on board thickness).
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

## 4. Final Cleanup of "Predators"
- **Description**: Purge any legacy objects or scripts that interfere with the player visuals.
- **Details**:
    - Delete/Disable any overlapping `[Player]` roots or wheeled board meshes.
- **Assigned role**: developer
- **Dependencies**: None
- **Parallelizable**: Yes

# Verification & Testing
- **Visual Inspection**: Use Scene View Multi-Angle capture to verify the blue/black clothing and realistic Caucasian skin are visible and correctly mapped.
- **Positioning Audit**: Verify the skater's feet are touching the board deck and they are facing sideways as intended.
- **Console Check**: Ensure no errors regarding missing textures or materials.
