# Fix Spawn and Level 1D Accessibility Plan

## Project Overview
- **Goal:** Fix the player spawning into a fall/lava at the start and make Level 1D ("The Nexus") playable for the user.
- **Target Sublevels:** 
  - **Spawn:** Reset hub starting point.
  - **Level 1D:** Adjust platform spacing, sizing, and height steps.

## 1. Reset Spawn & Level State
- **Problem:** The player was left at the start of Level 1C from a previous test. If 1C is inactive or gated, the player falls.
- **Fix:** 
  - Move Player to the center of the `StartingZone` Hub: `(0.5, 2.0, 0.5)`.
  - Reset `LevelManager` state to `currentSubLevel = "1A"`.
  - Ensure `Level1A_Root` is active and other roots are inactive (progression gating).

## 2. Level 1D "The Nexus" Accessibility Tuning
The current 15m gaps and 2m climbs are reported as "almost impossible."
### Physics Adjustments:
| Property | Old Value | New Value | Reason |
|---|---|---|---|
| **Step Distance** | 27.0m | **24.0m** | Reduced gap from 15m to 12m. |
| **Height Step** | +/- 2.0m | **+/- 1.0m** | Maintain vertical rhythm while reducing momentum loss. |
| **Platform Width** | 10.0m | **16.0m** | Massive landing area for side-to-side zig-zags. |
| **Platform Length** | 12.0m | **16.0m** | Forgiving landing window for under/overshooting. |

### Adjusted Layout for 1D (North):
| Platform | Z Position | Y Position | X Offset | Feature |
|---|---|---|---|---|
| Road_1D_1 | 35.5 | 1.5 | 0.5 | Launch point (reduced gap from hub) |
| Road_1D_2 | 59.5 | 2.5 | 5.5 | Zig-Right + Easy Climb |
| Road_1D_3 | 83.5 | 3.5 | -4.5 | Zig-Left + Easy Climb |
| Road_1D_4 | 107.5 | 4.5 | 5.5 | Zig-Right + Easy Climb |
| Road_1D_5 | 131.5 | 5.5 | 0.5 | **Peak Landing** |
| Road_1D_6 | 155.5 | 1.5 | 0.5 | **The Big Drop** (Ruins Below) |
| Road_1D_7 | 179.5 | 2.5 | -4.5 | Recovery Climb |
| Road_1D_8 | 203.5 | 3.5 | 5.5 | Final Zig |
| Road_1D_9 | 227.5 | 2.5 | 0.5 | **GOAL** (Portal + Safety) |

## Implementation Steps

### 1. Fix Starting State
- **Description:** Reset player position and Manager state.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Tuned Level 1D Platforms
- **Description:** Update positions and scales for all 9 platforms in `Level1D_Root`.
- **Assigned role:** developer
- **Dependencies:** None

### 3. Relocate 1D Goal Assets
- **Description:** Move `GoalCoin_1D` and the portal trigger to the new `Road_1D_9` position.
- **Assigned role:** developer
- **Dependencies:** Step 2

### 4. Play Mode Verification
- **Description:** Activate `play-mode-test` to verify the player is standing on the hub at start and can land on the first platform of 1A.
- **Assigned role:** developer
- **Dependencies:** Step 1

## Verification & Testing
- **Visual Check:** Platforms in 1D should look significantly larger and closer.
- **Runtime Check:** Player starts safely in the hub.
