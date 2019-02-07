# States
- [x] States transition into other states by returning them to the "manager"
- [x] States should have enter and exit methods 
- [x] States should have input and updates delegated to them
- [x] States should have a history
- [x] States must be inferred from an enum, therefore each manager can expose a public initial state property. This
    allows initial states to be selected from the editor
    
# Level
- [ ] Restart

# Player
- [x] Can move
- [x] Cannot move, whilst moving
- [x] Can push other entities
- [ ] Input received close to the end of a movement should be registered

# Blocks
- [x] Can be pushed by the player
- [ ] Can have lazers

# Walls
- [ ] Cannot move
- [ ] Can allow certain entities to pass through

# Player Goal
- [x] Cannot move
- [ ] Inactive state
- [ ] Next level 

# Entity Goal
- [x] Cannot move

# Instructions
- [x] A movement instruction should be dispatched from a service and consumed by a manager
    this should decouple movement from an input source, therefore NPC entities can be
    moved using computed instructions, whereas the player will be moved by instructions
    derived from input 


