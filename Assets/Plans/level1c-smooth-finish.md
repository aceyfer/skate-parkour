# Level 1C "Smooth Finish" Adjustment Plan

## Project Overview
- **Goal:** Ease the difficulty of the final three platforms in Level 1C ("The Vertical Ascendant").
- **Target Platforms:** `Road_1C_7`, `Road_1C_8`, `Road_1C_9`.
- **User Feedback:** Last 3 platforms are way too hard to land.

## 1. Design Adjustments
### Physics of Placement (Standard vs. Adjusted)
| Feature | Previous | Proposed |
|---|---|---|
| **Height Step** | +/- 2.0m | **+/- 1.0m** (More forgiving for momentum) |
| **Gap Distance** | 15.0m | **11.0m** (Reduced for easier clearance) |
| **Platform Width** | 10.0m | **15.0m** (Wider target area) |
| **Platform Length** | 12.0m | **16.0m** (Longer landing pad) |

### Adjusted Layout for 1C (Final Stretch)
| Platform | X Position | Y Position | Z Position | Reason |
|---|---|---|---|---|
| **Road_1C_6** | 176.5 | 1.5 | 0.5 | *Existing baseline* |
| **Road_1C_7** | 201.5 | **2.5** | 0.5 | +1m climb instead of +2m |
| **Road_1C_8** | 226.5 | **3.5** | 0.5 | Final Peak, reduced climb |
| **Road_1C_9** | 251.5 | **2.5** | 0.5 | GOAL: Reduced drop to ease landing |

## Implementation Steps

### 1. Update Platform 1C_7 through 1C_9
- **Description:** Adjust positions and scales for the last three platforms in the `Level1C_Root`.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Update Goal Fragment Position
- **Description:** Snap `GoalCoin_1C` and the portal trigger to the new `Road_1C_9` position.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 3. Unlock Level 1C for Testing
- **Description:** Set `LevelManager` current sub-level to "1C" and activate root for immediate user testing.
- **Assigned role:** developer
- **Dependencies:** None

## Verification & Testing
- **Visual Check:** Platforms 7-9 should appear significantly larger and closer together.
- **Manual Test:** Confirm the height climb feels rhythmic and attainable with standard "Ollie" charge.
