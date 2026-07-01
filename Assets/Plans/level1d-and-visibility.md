# Level 1D Implementation & Visibility Fixes Plan

## Project Overview
- **Goal:** Resolve Level 1C visibility issues and implement Level 1D (North / +Z) with a unique obstacle course.
- **Orientation:**
    - Level 1A: South (-Z)
    - Level 1B: West (-X)
    - Level 1C: East (+X)
    - Level 1D: North (+Z)

## Game Mechanics
### Level 1D: "North Ridge"
- **Direction:** North (+Z).
- **Theme:** "Shattered Peaks" — High verticality with narrow, long platforms.
- **Difficulty:** Introduce "Wind" visual effects (floating particles) and tight gaps.
- **Layout:** 9 platforms heading North from the Hub.

### Progression Flow
- Level 1C Completion → Unlocks Level 1D.
- Level 1D Completion → Unlocks Level 2.

## Implementation Steps

### 1. Update `LevelManager.cs`
- **Description**: Add `level1DRoot` serialized field and update `CompleteSubLevel` logic to handle the 1C → 1D transition and the 1D completion to Level 2.
- **Assigned role**: developer
- **Dependencies**: None

### 2. Activate Level 1C and Build Level 1D
- **Description**: 
    - Set `Level1C_Root` to active so it's visible.
    - Create `Level1D_Root` and generate 9 platforms heading North (+Z).
    - Apply amber/magenta theme to 1D as well for now, or a new "Ice/Wind" theme.
- **Assigned role**: developer
- **Dependencies**: Step 1

### 3. Setup Level 1D Goal & Portal
- **Description**: Place Goal Fragment on `Road_1D_9` and configure `PlatformPortal` to teleport to Level 2.
- **Assigned role**: developer
- **Dependencies**: Step 2

## Verification & Testing
- **Visual Check**: Ensure all four directions (A, B, C, D) have courses branching from the Hub.
- **Gating Check**: Verify 1D only spawns after 1C fragment collection.
- **Teleport Check**: Verify 1D portal leads to the Level 2 separate area.
