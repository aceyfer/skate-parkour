# Seamless Texturing and Dimensional Environment Detail Plan

## Project Overview
- **Goal:** Fix the "Everything is still gray" issue by generating and applying high-quality seamless textures to all roads and debris.
- **Style:** Gritty Realistic urban decay with dimensional fracture elements.

## 1. Texture Generation (Seamless)
I will generate 4 primary seamless textures to replace the current gray or missing textures.
- **Model:** `gemini-3.1-flash-texture` (SupportsTileable)

| Level | Texture Name | Prompt |
|---|---|---|
| **Hub / 1D** | `Road_Asphalt_Seamless` | "Seamless gritty dark asphalt road texture with cracks and oil stains, realistic 4k." |
| **Level 1A** | `Sidewalk_Concrete_Seamless` | "Seamless weathered urban concrete sidewalk texture, gritty realistic, subtle moss in cracks." |
| **Level 1B** | `Road_Frozen_Seamless` | "Seamless cracked asphalt road covered in a thin layer of blue ice and frost, gritty realistic." |
| **Level 1C** | `Rift_Plate_Seamless` | "Seamless sci-fi metal floor plating with glowing neon circuit patterns, dark metallic, futuristic." |

## 2. Material Configuration
I will update the existing materials to use these new textures and ensure they are assigned to the platforms.

## 3. Dimensional Fracture Detail (Props & Debris)
The user wants more "buildings debris" and floating objects.
- I will generate **Building Debris Fragments** (FBX) with realistic textures.
- I will scatter **Floating Cars** and **Parking Meters** more aggressively in Level 1D (The Nexus).

## Implementation Steps

### 1. Asset Generation: Textures
- **Description:** Generate the 4 seamless textures using AI.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Asset Generation: Building Debris
- **Description:** Generate a "Building Facade Chunk" to add to Level 1B and 1D.
- **Assigned role:** developer
- **Dependencies:** None

### 3. Apply Textures and Fix "Gray" Materials
- **Description:** Update `Concrete_Sidewalk`, `FrozenStreet`, `RiftPlatform`, and `T_FrozenAsphalt` with new textures.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 4. Scene Reconstruction & Detail Pass
- **Description:** Iterate through the scene, ensure every platform uses the updated materials, and add floating cars/debris.
- **Assigned role:** developer
- **Dependencies:** Step 2 & 3

## Verification & Testing
- **Visual Check:** No gray platforms should be visible. Textures should repeat seamlessly.
- **Detail Check:** Floating cars and debris should be visible in the voids.
