# Fix Jump and Crouch Logic Plan

## Project Overview
- **Goal:** Restore and improve the Jump (Ollie) and Crouch mechanic to ensure reliability.
- **Problem:** Jump often fails to trigger upon release, and the crouch state is too easily canceled by minor physics fluctuations.

## Implementation Steps

### 1. Refactor `HandleInput` in `HoverboardController.cs`
- **Description:** 
    - Decouple `isCrouching` state from `isHovering` during the charge phase. This prevents the charge from being canceled if the board leaves the ground for a single frame.
    - Only require `isHovering` to *start* the crouch and to *execute* the jump.
    - Ensure a minimum "tap jump" triggers even if the key is released very quickly.
- **Assigned role:** developer
- **Dependencies:** None

### 2. Improve Grounding Detection for Jumps
- **Description:** 
    - Increase the raycast distance or add a small "coyote time" buffer to the jump eligibility check.
    - This ensures the player can still jump if they are a few centimeters above the "target hover height".
- **Assigned role:** developer
- **Dependencies:** None

### 3. Restore Crouch Visual Scaling (Optional/Re-evaluate)
- **Description:** 
    - Re-evaluate if the user liked the visual scaling or just the animation.
    - If the user felt the "feature was messed with", they might be missing the visual feedback of the board/character physically lowering. 
    - I will re-add a subtle vertical squash to the `visualsContainer` during crouch to supplement the animation.
- **Assigned role:** developer
- **Dependencies:** None

## Verification & Testing
- **Tap Test:** Verify a quick tap of Space triggers a standard jump height.
- **Charge Test:** Verify holding Space for a duration and releasing results in a higher jump.
- **Bumpy Terrain Test:** Verify jumping works reliably even on the uneven surfaces of Level 1B.
- **Visual Check:** Confirm the character/board visually lowers while holding jump.
