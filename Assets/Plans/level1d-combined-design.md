# Level 1C Cleanup and Level 1D "The Combined Spire" Design Plan

## Project Overview
- **Goal:** Clean up irrelevant "ghost" platforms in the 1C lane and design Level 1D using a synthesis of 1A, 1B, and 1C's design elements.
- **Direction for 1D:** North (+Z).
- **Theme for 1D:** "The Combined Spire" — Integrating zig-zag navigation (1A), urban ruin aesthetics/props (1B), and vertical rhythmic platforming (1C).

## 1. Deletion of Irrelevant Pieces (1C Lane)
- **Target Area:** X > 30, |Z| < 15.
- **Identified Junk:**
    - Objects parented to `Level1_Beginner`.
    - Objects parented to `Level2_Downwards`.
    - Loose `tripo_node` objects that are not the currently active Goal Fragments.
- **Preserve:** All `Road_1C_X` platforms and `GoalCoin_1C`.

## 2. Level 1D: "The Combined Spire" Design
### Physics of Placement (The "Standard")
- **Platform Scale:** 12m (Length) x 10m (Width) x 0.5m (Height).
- **Horizontal Gap:** 15m (Step = 27m).
- **Vertical Step:** +/- 2m for rhythm, or larger drops (8-10m) for impact.
- **Zig-Zag Offset:** +/- 5m on the X-axis for horizontal navigation.

### Layout (9 Platforms heading North)
| Platform | Z Position | X Offset | Y Position | Feature |
|---|---|---|---|---|
| Road_1D_1 | 41.5 | 0.5 | 1.5 | Launch point (10m gap from hub) |
| Road_1D_2 | 68.5 | 5.5 | 3.5 | Zig-Right + Climb 1 |
| Road_1D_3 | 95.5 | -4.5 | 5.5 | Zig-Left + Climb 2 |
| Road_1D_4 | 122.5 | 5.5 | 7.5 | Zig-Right + Climb 3 |
| Road_1D_5 | 149.5 | 0.5 | 9.5 | **Peak Landing** |
| Road_1D_6 | 176.5 | 0.5 | 1.5 | **The Big Drop** (Ruins Below) |
| Road_1D_7 | 203.5 | -4.5 | 3.5 | Zig-Left + Recovery |
| Road_1D_8 | 230.5 | 5.5 | 5.5 | Zig-Right + Final Climb |
| Road_1D_9 | 257.5 | 0.5 | 3.5 | **GOAL** (Portal + Safety) |

### Visual Identity
- **Synthesis:** 
    - Asphalt/Ruins texture (1B).
    - Neon Magenta rails (1C).
    - Scattered urban debris/rubble and street lamps (1B).
    - Floating in the "Ascendant" vertical style (1C).

## Implementation Steps

### 1. Execute Scene Cleanup
- **Description:** Delete all non-1C objects in the +X corridor as identified.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Implement Level 1D Platforms
- **Description:** Run Command to build the 9-platform vertical zig-zag course heading North.
- **Assigned role:** developer
- **Dependencies:** None

### 3. Add Props and Atmosphere
- **Description:** Add street lamps and floating rubble to 1D to match the polished feel of 1B.
- **Assigned role:** developer
- **Dependencies:** Step 2

### 4. Setup Goal and Safety
- **Description:** Place Goal Fragment, Portal, and Safety Walls on `Road_1D_9`.
- **Assigned role:** developer
- **Dependencies:** Step 2

## Verification & Testing
- **Visual Check:** Verify +X lane is clear of old "ghost" pieces.
- **1D Flow Test:** Confirm the vertical zig-zag is clearable and feels like a combination of the previous levels.
- **Sequence Test:** Ensure 1C completion triggers 1D visibility.
