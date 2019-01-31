using Instructions;
using Managers;
using Managers.Block;
using Managers.Player;
using State.Entity;
using UnityEngine;

namespace State.Block
{
    public class BlockIdleState: IState, IStateWithEnter, IStateWithExit
    {
        private readonly BlockMovementManager _movementManager;

        public BlockIdleState(BlockMovementManager movementManager)
        {
            _movementManager = movementManager;
        }
        
        public IState Update(AbstractStateManager stateManager)
        {
            return null;
        }

        public void Enter(AbstractStateManager stateManager)
        {
            _movementManager.collisionService.OnCollision += HandleCollision;
        }

        private void HandleCollision(GameObject hit)
        {
            PlayerMovementManager playerMovementManager = hit.GetComponent<PlayerMovementManager>();

            if (playerMovementManager == null)
                return;

            IState state = playerMovementManager.GetState();
            EntityMovingState entityMovingState = state as EntityMovingState;

            if (entityMovingState == null)
                return;
            
            MovementInstruction instruction = new MovementInstruction(entityMovingState.Instruction.Direction);
            _movementManager.SetState(new EntityMovingState(_movementManager, instruction));
        }

        public void Exit(AbstractStateManager stateManager)
        {
            _movementManager.collisionService.OnCollision -= HandleCollision;
        }
    }
}