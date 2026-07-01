# Level 1C: "Ascendant Rift" Implementation Plan

## Project Overview
- **Game Title:** Skate Parkour
- **Goal:** Design a creative, vertical Level 1C (East / +X) that introduces up-and-down platforming — a staircase climb, a big satisfying drop, then a rollercoaster wave to the goal.
- **Direction:** East (+X), player faces `Quaternion.Euler(0, 90, 0)` on teleport.
- **Theme:** A vertical dimensional rift rising into the sky. Warm **amber/magenta** identity to contrast 1A's asphalt road and 1B's cold cyan frozen street.

## Game Mechanics
### Vertical Platforming (the core ask)
Tuned to current player physics (jump velocity ≈13.4 m/s, gravity -20, maxSpeed 15):
- **Ascents:** small horizontal gaps (~2m) with **+2m rise** per step — clearable while still rising.
- **Drops:** wide gaps (~7-12m) are fine and exciting because falling grants long air time.

### Layout (13 platforms, length 5m along travel, width 8m)
| Platform | X | Y | Note |
|---|---|---|---|
| P1 | 34 | 1.5 | Base launch |
| P2 | 41 | 3.5 | Ascent 1 (+2) |
| P3 | 48 | 5.5 | Ascent 2 (+2) |
| P4 | 55 | 7.5 | Ascent 3 (+2) |
| P5 | 62 | 9.5 | Ascent 4 (+2) — **peak (4 platforms up from P1)** |
| P6 | 74 | 1.5 | **Big drop back to base level (same as P1)** |
| P7 | 81 | 3.5 | Wave up (+2) |
| P8 | 88 | 5.5 | Wave up (+2) |
| P9 | 95 | 5.5 | Flat landing pad |
| P10 | 103 | 3.5 | Wave down (-2) |
| P11 | 112 | 1.5 | Back to base (-2) |
| P12 | 121 | 2.5 | Approach (+1) |
| P13 | 130 | 3.5 | **GOAL** — Dimensional Fragment + portal + safety |

### Progression & Safety (consistent with 1A/1B)
- **Gating:** Level 1C is hidden until Level 1B is completed.
- **Goal:** Dimensional Fragment (vacuum pickup) on P13, `PlatformPortal` trigger on P13 → teleport to 1D, `GoalSafety` invisible walls + visual Goal Gate so the player can't fall off after landing.

## Key Asset & Context
- **Scripts (reused):** `PlatformPortal.cs`, `GoalSafety.cs`, `Coin.cs`, `FloatingRubble.cs`.
- **LevelManager.cs change:** add `[SerializeField] GameObject level1CRoot;` and reveal it inside `CompleteSubLevel` when `subLevel == "1B"` (mirrors the existing `level1BRoot` reveal on 1A).
- **Materials (new):** `RiftPlatform.mat` (warm amber), `Neon_Magenta.mat` (emissive). Re-use `FrozenStreet.mat`/Standard for structure where useful.
- **level1C_Start:** already `(21, 1.5, 0.5)` on the hub — keep.

## Implementation Steps

### 1. Add Level 1C Gating to LevelManager.cs
- **Description:** Add `level1CRoot` serialized field; in `CompleteSubLevel`, when `subLevel == "1B"` set `level1CRoot.SetActive(true)`.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Build Level 1C Platform Layout
- **Description:** RunCommand to create `Level1C_Root` with 13 platforms (`Road_1C_1`..`Road_1C_13`) per the table above. Apply `RiftPlatform` material + `Neon_Magenta` edge rails. Add gentle visual variety (slight rotation) without breaking flat landing surfaces.
- **Assigned role:** developer
- **Dependencies:** Step 1

### 3. Create Materials
- **Description:** Create `RiftPlatform.mat` (warm amber) and `Neon_Magenta.mat` (emissive magenta).
- **Assigned role:** developer
- **Dependencies:** None
- **Parallelizable:** Yes (with Step 1)

### 4. Goal, Portal & Safety on P13
- **Description:** Place Dimensional Fragment on `Road_1C_13` (isGoalCoin, collectSound), add `PlatformPortal` + trigger collider, add `GoalSafety` walls + Goal Gate visual.
- **Assigned role:** developer
- **Dependencies:** Step 2

### 5. Gate the Level & Wire Root
- **Description:** Assign `Level1C_Root` to `LevelManager.level1CRoot`, set it inactive by default.
- **Assigned role:** developer
- **Dependencies:** Steps 1, 2

## Verification & Testing
- **Climb Test:** Confirm the 4-step ascent (P1→P5) is clearable at speed with charged jumps.
- **Drop Test:** Confirm the P5→P6 drop lands safely back at base level.
- **Wave Test:** Confirm the up/down rollercoaster (P7→P11) flows well.
- **Goal Test:** Confirm fragment vacuum, safety walls engage, and portal teleports to 1D.
- **Gating Test:** Confirm 1C is hidden until 1B is completed.
