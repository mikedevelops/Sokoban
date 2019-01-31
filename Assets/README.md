# States
√ States must be responsible for transitioning into other states
√ States transition into other states by returning them to the "manager"
√ States should have enter and exit methods 
√ States should have input and updates delegated to them
√ States should have a history
√ States must be inferred from an enum, therefore each manager can expose a public initial state property. This
    allows initial states to be selected from the editor
    
# Level
- Flip level to match editor scriptable objects
- Add ability to add entities to tiles in editor

# Player
√ Can move
√ Cannot move, whilst moving
√ Can push other entities

# Blocks
√ Can be pushed by other blocks

# Walls
- Cannot move
- Can allow certain entities to pass through

# Player Goal
√ Cannot move
- Next level 

# Entity Goal
- Cannot move

# Instructions
√ A movement instruction should be dispatched from a service and consumed by a manager
    this should decouple movement from an input source, therefore NPC entities can be
    moved using computed instructions, whereas the player will be moved by instructions
    derived from input 

