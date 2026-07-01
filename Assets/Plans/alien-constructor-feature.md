# Project Overview
- Game Title: Skate Parkour
- High-Level Concept: Realistic physics-based skateboarding parkour with a "Broken Dimension" alien assistant who helps construct the world.
- Alien Character: Black skin, glowing blue tattoos, glowing purple eyes, wearing a borrowed black hoodie.

# Game Mechanics
## Core Gameplay Loop
The player skates through the world. The Alien AI skates alongside or to specific points to build obstacles using pieces from both worlds.

## AI Construction System
- **Skating AI**: The alien uses a modified `SkateParkourController` driven by Unity's NavMesh system.
- **Build Mode Interface**: A simple UI or shortcut to select props for the alien to build.
- **Dynamic NavMesh**: The environment updates its walkable area as new props are placed.

# Key Asset & Context
- **Alien_Model**: A generated 3D model of the alien character.
- **AlienSkateAI.cs**: Logic for the AI to navigate the world with skateboard physics.
- **ConstructionManager.cs**: Manages the toolkit and commands the AI to build.
- **SkateProps**: A collection of prefabs (ramps, rails, platforms).

# Implementation Steps
1. **Generate Alien Character**:
   - Use `Tripo P1` to generate a 3D model of the alien (Black skin, blue tattoos, purple eyes, black hoodie).
   - Use `Tripo Rigging` to add a humanoid skeleton to the model.
   
2. **Setup AI Navigation**:
   - Add a `NavMeshSurface` to the scene environment.
   - Configure agent settings for the Alien.
   
3. **Develop Alien AI Controller**:
   - Create `AlienSkateAI.cs` which inherits movement logic from a base skate controller or implements a variant that responds to `NavMeshAgent` paths.
   
4. **Create Prop Toolkit**:
   - Define a few "Broken Dimension" props using existing primitives or generated assets.
   - Implement `ConstructionManager.cs` to handle placement logic.

# Verification & Testing
1. **Visual Verification**: Check the generated alien model for style and glowing effects.
2. **Navigation Test**: Command the alien to skate to a point and verify its movement follows the terrain and uses skate-like momentum.
3. **Construction Test**: Place a prop via the manager and ensure the alien moves to the spot before it appears.
