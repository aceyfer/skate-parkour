# Level 1B Texturing Plan

## Project Overview
- **Goal:** Generate and apply tileable textures to bring the "Frozen Street" of Level 1B to life.
- **Render Pipeline:** Built-in (Standard Shader).

## Asset Generation List (Tileable Textures)
1. **Cracked Frozen Asphalt** — main road/sidewalk surfaces (`FrozenStreet.mat`).
2. **Weathered Building Facade** — building blocks + distant silhouettes (windows, concrete).
3. **Rusted/Frosted Metal** — lamp posts, hydrants, gate pillars, rubble.

## Implementation Steps

### 1. Generate Tileable Textures
- **Description:** Use `realistic-textures-3-0` (SupportsTileable) to generate 3 seamless 1024x1024 textures.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Create / Update Materials
- **Description:** 
    - Assign the asphalt texture to `FrozenStreet.mat`.
    - Create `Building_Facade.mat` and `Metal_Frost.mat`.
    - Set proper tiling and slight blue/frost tint.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 3. Apply Materials to Objects
- **Description:** 
    - Sidewalks/MainRoad → FrozenStreet (textured).
    - Buildings + DistantBuildings → Building_Facade.
    - LampPosts, Hydrants, Pillars, Beam, Rubble → Metal_Frost.
    - Preserve Neon_Cyan emissive on rails.
- **Assigned role:** developer
- **Dependencies:** Step 2

## Verification & Testing
- **Visual Check:** Confirm textures tile without obvious seams.
- **Tiling Check:** Ensure road texture scale matches platform size (not stretched).
- **Theme Check:** Ensure overall frosted/urban-ruin tone is consistent.
