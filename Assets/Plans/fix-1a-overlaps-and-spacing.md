# Project Overview
- Game Title: Skate Parkour
- Issue: Level platforms (A-D) overlap the starting zone and lack proper difficulty progression.
- Goal: Fix overlaps and establish a clear progression from Beginner (1A) to Advanced (1D).

# Key Asset & Context
- `StartingZone`: 50x50 area centered at `(0.5, 0.35, 0.5)`. Extends to +/- 24.5 on X and Z.
- Level Roots: `Level1A_Root` (-Z), `Level1B_Root` (-X), `Level1C_Root` (+X), `Level1D_Root` (+Z).

# Implementation Steps

## 1. Fix Level 1A (Beginner)
- **Position**: Start at Z = -45.0. 
- **Spacing**: 18.0u center-to-center (10u platform + 8u gap).
- **Layout**: Straight line (X = 0.5).
- **Goal**: GoalCoin_1A at Z end.

## 2. Fix Level 1B (Novice)
- **Position**: Start at X = -45.0.
- **Spacing**: 22.0u center-to-center (12u gap).
- **Layout**: Straight line (Z = 0.5).

## 3. Fix Level 1C (Intermediate)
- **Position**: Start at X = 45.0.
- **Spacing**: 25.0u center-to-center (15u gap).
- **Layout**: Gentle curves (X axis).

## 4. Fix Level 1D (Advanced)
- **Position**: Start at Z = 45.0.
- **Spacing**: 28.0u center-to-center (18u gap).
- **Layout**: High-speed zig-zags (Z axis).

## 5. Cleanup & NavMesh
- Re-bake NavMeshSurface.
- Ensure no "orphaned" platforms remain inside the StartingZone footprint.

# Verification
- Use Capture2DScene to verify clearance.
- Verify Alien AI tests 1A correctly.
