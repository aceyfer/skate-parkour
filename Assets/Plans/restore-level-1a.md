# Restore Level 1A "The Beginner's Descent" Plan

## Project Overview
- **Goal:** Restore the accidentally deleted Level 1A (previously `Level1_Beginner`).
- **Direction for 1A:** South (-Z).
- **Theme for 1A:** "The Beginner's Descent" — Simple, wide platforms with gentle height changes and minor zig-zagging to introduce the "Grip" mechanic.

## 1. Level 1A: "The Beginner's Descent" Design
### Physics of Placement
- **Platform Scale:** 12m (Length) x 10m (Width) x 0.5m (Height).
- **Horizontal Gap:** 15m (Step = 27m).
- **Zig-Zag Offset:** +/- 3m on the X-axis (Beginner level).
- **Vertical Step:** +/- 1m.

### Layout (9 Platforms heading South)
| Platform | Z Position | X Offset | Y Position | Feature |
|---|---|---|---|---|
| Road_1A_1 | -41.5 | 0.5 | 1.5 | Launch point (10m gap from hub) |
| Road_1A_2 | -68.5 | 3.5 | 1.0 | Gentle Right |
| Road_1A_3 | -95.5 | -2.5 | 0.5 | Gentle Left |
| Road_1A_4 | -122.5 | 3.5 | 0.5 | Gentle Right |
| Road_1A_5 | -149.5 | 0.5 | 1.5 | Recovery straight |
| Road_1A_6 | -176.5 | 0.5 | 2.5 | Gentle Climb |
| Road_1A_7 | -203.5 | -2.5 | 1.5 | Gentle Left |
| Road_1A_8 | -230.5 | 3.5 | 2.5 | Gentle Right |
| Road_1A_9 | -257.5 | 0.5 | 1.5 | **GOAL** (Portal + Safety) |

### Visual Identity
- **Style:** Concrete Sidewalk style (introduces the urban theme).
- **Rails:** Neon Cyan (different from the Spire's Magenta).

## Implementation Steps

### 1. Create Level 1A Root and Platforms
- **Description:** Build the 9-platform course heading South.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Setup Level 1A Goal and Safety
- **Description:** Place GoalCoin_1A, Portal, and Safety Walls on `Road_1A_9`.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 3. Link Progression
- **Description:** Ensure GoalCoin_1A correctly triggers Level 1B unlock in `LevelManager`.
- **Assigned role:** developer
- **Dependencies:** Step 2

## Verification & Testing
- **Visual Check:** Verify platforms head South from the hub.
- **Progression Test:** Collecting GoalCoin_1A unlocks Level 1B.
