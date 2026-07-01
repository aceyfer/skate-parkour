# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: 3D skateboarding game with physics-based movement.
- Stance: Goofy (Right foot forward).
- Visibility: Fix player model being invisible due to small scale and incorrect setup.

# Game Mechanics
## Core Gameplay Loop
The player skates through stylized environments, building momentum and performing jumps (Ollies).

# UI
N/A (Gameplay focus)

# Key Asset & Context
- `[Player]`: Root capsule object.
- `Visuals`: Parent of skater and board meshes.
- `Skater_Mesh`: Character model.
- `Skateboard_Mesh`: Skateboard model.
- `SkateParkourController`: Main movement script.
- `Player_Mat` & `Skateboard_Mat`: Textures materials.

# Implementation Steps
1. **Fix Visibility (Scale & Rendering)**:
   - Scale `Skater_Mesh` and `Skateboard_Mesh` to `(60, 60, 60)` to match the capsule's height (original meshes are tiny).
   - Ensure `MeshFilter` on both objects uses `Mesh_0` from their respective FBX files.
   - Ensure `MeshRenderer` on both objects is enabled and has `Player_Mat` (for skater) and `Skateboard_Mat` (for board) assigned.
   - Assigned role: developer.

2. **Set Goofy Stance & Foot Placement**:
   - Set `SkateParkourController.stance` to `Goofy` on the `[Player]` object.
   - Rotate `Skater_Mesh` local rotation to `(0, -90, 0)` so it faces sideways on the board.
   - Position `RightFootTarget` (child of board) at `(0.4, 0.05, 0)` (forward position on the board).
   - Position `LeftFootTarget` (child of board) at `(-0.4, 0.05, 0)` (back position on the board).
   - *Note*: Since the board's length is along the local X axis (extents 0.01), and it's scaled by 60, the board is ~1.2m long. +/- 0.4m is a good placement for feet.
   - Assigned role: developer.

3. **Wire Controller References**:
   - Ensure `SkateParkourController` has `visualsContainer` assigned to `Visuals`.
   - Ensure `SkateParkourController` has `skateboardMesh` assigned to `Skateboard_Mesh`.
   - Assigned role: developer.

4. **Verify Animations**:
   - Ensure `Animator` on `Skater_Mesh` is active.
   - Assigned role: developer.

# Verification & Testing
1. **Visual Check**: In the Scene view, ensure the skater and board are clearly visible and the skater is standing sideways with the right foot forward.
2. **Play Mode Test**: Verify the player is visible in the Game view and follows the camera correctly.
3. **Control Test**: Verify the player moves forward and jumps when triggered.
