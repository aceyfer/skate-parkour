# Final Platform Fixes and Level 1C Spacing Redesign

## Project Overview
- **Goal:** Fix non-working teleports, resolve player "floating" issues on final platforms, and redesign Level 1C with better spacing.

## Game Mechanics
### Redesigned Level 1C (East)
- **Increased Gaps:** Increase horizontal gaps between platforms from 10m to 15m.
- **Platform Scale:** Maintain 12m length and 10m width for all 9 platforms.
- **Vertical Profile:** Keep the 4-step climb (+2m/step), big drop (to base), and recovery wave.
- **Positions (X):** 46.5 (P1), 73.5 (P2), 100.5 (P3), 127.5 (P4), 154.5 (P5), 181.5 (P6), 208.5 (P7), 235.5 (P8), 262.5 (P9 Goal).

### Teleport & Floating Fixes
- **Missing Portal:** Add `PlatformPortal` to `Road_1B_9`.
- **Scaling Fix:** Normalize `Road_1A_9` root scale to `(1,1,1)` and scale its child visual instead. This fixes the distorted safety walls that were causing floating/collision issues.
- **Safety Wall Buffering:** Move safety walls further out (0.75m from edge instead of 0.25m) to ensure the player's collision capsule (radius 0.4) never overlaps with them upon landing.
- **Portal Trigger Size:** Increase trigger height to 15m and width/length to cover the entire platform area plus a small margin.
- **CharacterController Check:** Ensure no non-trigger colliders exist on the root of any final platform object.

## Implementation Steps

### 1. Fix Road_1A_9 Scaling and Structure
- **Description**: 
    - Set `Road_1A_9` root scale to `(1,1,1)`.
    - Find/create a visual child and set its scale to `(10, 0.5, 12)`.
    - Ensure all colliders on the root are removed except the trigger.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Redesign Level 1C Layout
- **Description**: Reposition `Road_1C_1` through `Road_1C_9` with 15m gaps (27m center-to-center).
- **Assigned role**: developer
- **Dependencies**: None

### 3. Rebuild Safety Walls and Portals (All Levels)
- **Description**: 
    - Re-run the safety wall generation for 1A, 1B, 1C, 1D with 0.75m offset from edges.
    - Ensure `PlatformPortal` is on every final platform.
    - Set large trigger colliders for teleports.
- **Assigned role**: developer
- **Dependencies**: Steps 1, 2

### 4. Goal Fragment Repositioning
- **Description**: Move `GoalCoin_1A`, `1B`, `1C`, `1D` to their updated platform positions.
- **Assigned role**: developer
- **Dependencies**: Steps 1, 2

## Verification & Testing
- **Visual Capture**: Use `CaptureMultiAngleSceneView` to verify player placement on each final platform.
- **Teleport Test**: Verify all 4 teleports trigger correctly when the Goal Fragment is collected.
- **Floating Check**: Verify no collision pushing or floating occurs on landing.
- **1C Playability**: Verify the 15m jumps are clearable at 15m/s.
