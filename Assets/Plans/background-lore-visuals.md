# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Momentum-based skateboarding parkour in a dimension-fractured world.
- Players: Single player.
- Tone / Art Direction: Stylized, atmospheric, lore-driven (Earth vs Alien dimension).
- Render Pipeline: Built-in.

# Game Mechanics
## Core Gameplay Loop
The player skates through environments, building momentum and avoiding hazards. Falling into the pit resets the player.
## Controls and Input Methods
Standard skateboard controls (W to push, mouse to orbit).

# UI
N/A for this task.

# Key Assets & Context
- **Materials**:
    - `Skybox_Fractured_Mat`: A panoramic skybox material using a half-Earth/half-Alien texture.
    - `FieryPit_Mat`: An emissive material for the KillZone visual.
- **Textures**:
    - `FracturedSky_Panoramic`: Generated panoramic texture (Half Earth day sky, half black/purple alien sky with blue stars).
    - `FieryPit_Emissive`: Generated texture for the pit (Black/red glowing lava).
- **GameObjects**:
    - `FieryPit_Visual`: A large Plane positioned at Y = -19.9 to provide visual feedback for the KillZone.

# Implementation Steps
1. **Generate Fractured Sky Texture**:
   - Use `GenerateAsset` to create a panoramic texture with the half-Earth/half-Alien split.
   - Assigned role: developer.
   - Dependencies: None.

2. **Generate Fiery Pit Texture**:
   - Use `GenerateAsset` to create a glowing black/red fiery texture.
   - Assigned role: developer.
   - Dependencies: None.

3. **Create Skybox Material**:
   - Create `Skybox_Fractured_Mat` using the `Skybox/Panoramic` shader.
   - Assign the generated sky texture.
   - Apply to `RenderSettings.skybox`.
   - Assigned role: developer.
   - Dependencies: Step 1.

4. **Setup Fiery Pit Visuals**:
   - Create a `Plane` named `FieryPit_Visual`.
   - Scale it to match the `KillZone` (e.g., 1000x1000).
   - Position it at `(0, -19.9, 0)`.
   - Create `FieryPit_Mat` with `Standard` shader.
   - Set Albedo to Black and Emission to Red/Orange using the generated fiery texture.
   - Assigned role: developer.
   - Dependencies: Step 2.

5. **Lighting Adjustment**:
   - Adjust `RenderSettings.ambientMode` to `Skybox`.
   - Adjust `RenderSettings.ambientIntensity` to ensure the scene feels moody but playable.
   - Assigned role: developer.
   - Dependencies: Step 3.

# Verification & Testing
- Visual check: Ensure the skybox shows a clear split between Earth and the Alien dimension.
- Visual check: Ensure the pit looks like a "fiery pit" when looking down or falling.
- Gameplay check: Ensure the player still respawns correctly when hitting the `KillZone` (the visual should be slightly above the collider to ensure visibility before the trigger).
