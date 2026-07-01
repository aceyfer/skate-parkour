# Corrective City Texture and Urban Realism Plan

## Project Overview
- **Goal:** Re-align the visual style with the "City" related textures specifically requested by the user, replacing generic placeholders with building facade and urban-specific textures.
- **Visual Target:** A gritty, realistic city fractured by dimensional rifts.

## 1. Primary City Textures Audit
I will prioritize the following textures which are core to the "City" aesthetic:
- **Building Facade:** `Assets/Textures/T_BuildingFacade_Generated Maps/MainTex.jpg`
- **Road Albedo:** `Assets/Textures/Road_Albedo_Generated Maps/MainTex.jpg`
- **Urban Concrete:** `Assets/Textures/Sidewalk_Concrete_Seamless.png` (Keep for ground/sidewalks)

## 2. Re-Texturing Strategy
### A. Building & Debris (The "City" Parts)
All debris and fractured building chunks will be unified under the **Building Facade** texture to look like actual parts of the city that have been ripped out.
- Update `BuildingDebris` prefab.
- Update `UrbanDebrisChunk` prefab.
- Apply to any "Rubble" objects.

### B. The Nexus (Level 1D)
This level will now feature the highest density of city-specific debris:
- Mix of brick walls, building facades, and wrecked city props.
- Ensure the floating cars are textured as urban vehicles (already done, but will verify).

### C. Platform "City Block" Detail
I will attempt to apply the **Building Facade** texture to the *sides* of the platforms or create "skyscrapers" in the background/void using the facade texture to make the player feel like they are skating through a fractured city skyline.

## Implementation Steps

### 1. Unified City Material Setup
- **Description:** Ensure the `T_BuildingFacade` material is correctly using the high-quality MainTex.jpg.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Debris Re-Texturing Pass
- **Description:** Re-apply the `T_BuildingFacade` material to all 3D generated debris (UrbanDebrisChunk, BuildingDebris).
- **Assigned role:** developer
- **Dependencies:** Step 1

### 3. "Fractured Skyline" Decorative Pass
- **Description:** Add large floating vertical pillars textured with `T_BuildingFacade` in the voids of Level 1B and 1D to simulate shattered skyscrapers.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 4. Hub Final Texture Sync
- **Description:** Update the Hub to use the `Road_Albedo` city texture for the floor, and add city-specific signs or markers.
- **Assigned role:** developer
- **Dependencies:** None

## Verification & Testing
- **Visual Audit:** Confirm that debris looks like actual building rubble (bricks/windows/concrete) rather than generic gray shapes.
- **Skyline Check:** Ensure the "Fractured Skyline" elements create the sense of an urban environment.
