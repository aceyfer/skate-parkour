# Project Overview
- Game Title: Skate Parkour
- Goal: Test levels 1A through 1D for difficulty and completion possibility.
- Issue: Level 1A is reported as too difficult.

# Game Mechanics
- Movement: Physics-based hoverboard movement with acceleration, friction, and grip.
- Jump: "Ollie" jump that can be charged by holding the jump button.
- Hazards: KillZones that reset the player.

# Test Strategy
- Use `play-mode-test` to automate traversal of the first section of Level 1A.
- Measure max jump distance with current parameters.
- Evaluate gaps in all levels (1A-1D).
- Adjust Level 1A geometry or player parameters if impossible.

# Implementation Steps
1. **Provision Play Mode Test**:
   - Write a multi-frame test script to `Assets/Editor/PlayModeTestRunner.cs`.
   - The test will teleport the player to the start of Level 1A, accelerate to max speed, and perform a full-charge jump.
   - Files: `Assets/Editor/PlayModeTestRunner.cs`
   - Assigned role: developer.

2. **Execute and Poll Test**:
   - Prime SessionState and force domain reload.
   - Wait and retrieve results.
   - Assigned role: developer.

3. **Level Difficulty Audit**:
   - Calculate gap distances for all levels.
   - Propose adjustments to Level 1A (e.g., adding an intermediate platform).
   - Assigned role: developer.

4. **Apply Fixes**:
   - Adjust Level 1A geometry based on findings.
   - Re-run test to verify.
   - Assigned role: developer.

# Verification & Testing
- Automated test success: Player lands the first gap in Level 1A.
- Manual verification: Observe the Alien AI traversing the path if possible.
