using Instructions;
using Managers;
using Managers.Block;
using State.Entity;
using UnityEngine;

namespace State.Block
{
    public class SheepIdleState: IState, IStateWithEnter, IStateWithExit
    {
        private readonly SheepMovementManager _movementManager;

        public SheepIdleState(SheepMovementManager movementManager)
        {
            _movementManager = movementManager;
        }
        
        public IState Update(AbstractStateManager stateManager)
        {
            return null;
        }
        
        public void Enter(AbstractStateManager stateManager)
        {
            _movementManager.repelService.OnRepel += HandleRepel;
        }

        public void Exit(AbstractStateManager stateManager)
        {
            _movementManager.repelService.OnRepel -= HandleRepel;
        }

        private void HandleRepel(Vector2Int direction)
        {                                 
            MovementInstruction instruction = new MovementInstruction(direction);
            _movementManager.SetState(new EntityMovingState(_movementManager, instruction));
        }
    }
}