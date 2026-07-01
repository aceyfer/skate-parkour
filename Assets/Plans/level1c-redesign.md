# Level 1C Redesign and Spacing Fix Plan

## Project Overview
- **Goal:** Redesign Level 1C (East) to resolve playability issues caused by cramped platform spacing.
- **Direction:** East (+X).
- **Core Mechanic:** Vertical platforming (4-step ascent, big drop, goal approach).

## Game Mechanics
### Platform Spacing & Sizing
- **Length:** Increase platform length to 12m (along X) to match Level 1B standard.
- **Width:** Increase width to 10m (along Z).
- **Horizontal Gap:** Set a consistent 10m gap between platform edges.
- **Vertical Rise:** 2m rise per step for the ascent.

### Layout (9 Platforms)
- **P1:** Base launch (X=41.5, Y=1.5). Gap from hub: 10m.
- **P2-P5:** The Climb. Each rises 2m and moves 22m horizontally (12m length + 10m gap).
- **P6:** The Big Drop. Drops from 9.5m back to 1.5m (base level).
- **P7-P8:** The Recovery. Gradual rise (+2m per step).
- **P9:** The Goal. Landing on base level (+3.5m) with Dimensional Fragment and Safety Walls.

## Implementation Steps

### 1. Rebuild Level 1C Platforms
- **Description:** Run a command to reposition and rescale all platforms in `Level1C_Root`. 
- **Assigned role:** developer
- **Dependencies:** None

### 2. Update Goal & Safety Setup
- **Description:** Reposition the Goal Fragment (GoalCoin_1C), Goal Gate, and Safety Walls to the new position of `Road_1C_9`. Ensure Safety Walls are correctly sized for the 12x10 platform.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 3. Cleanup Redundant Platforms
- **Description:** If 13 platforms were previously created, remove the extra 4 (10-13) as we are consolidating to a 9-platform course for consistency and pacing.
- **Assigned role:** developer
- **Dependencies:** Step 1

## Verification & Testing
- **Jump Check:** Verify that at 15m/s, the 10m horizontal / 2m vertical jump is clearable.
- **Visual Check:** Confirm platforms are no longer cramped and provide ample landing space.
- **Progression Check:** Verify the portal on `Road_1C_9` teleports to Level 1D start.
