using System;
using Instructions;
using Managers;
using Managers.Block;
using Managers.Player;
using State.Block;
using State.Player;
using UnityEngine;

namespace State.Entity
{
    public class EntityMovingState: IState
    {
        public MovementInstruction Instruction;
        
        private readonly Vector3 _targetPosition;
        private Vector3 _velocity;

        public EntityMovingState(
            AbstractStateManager stateManager,
            MovementInstruction movementInstruction
        ) {
            Instruction = movementInstruction;
            _targetPosition = new Vector3(
                movementInstruction.Direction.x,
                0,
                movementInstruction.Direction.y
            ) + stateManager.transform.position;
        }
        
        public IState Update(AbstractStateManager stateManager)
        {
            SetEntityPosition(stateManager, GetNewPosition(stateManager));
            
            if (HasArrived(_targetPosition, stateManager))
            {
                SetEntityPosition(stateManager, _targetPosition);
                
                if (stateManager is PlayerMovementManager)
                    return new PlayerIdleState((PlayerMovementManager) stateManager);

                if (stateManager is BlockMovementManager)
                    return new BlockIdleState((BlockMovementManager) stateManager);
                
                return new IdleState();
            }

            return null;
        }

        private Vector3 GetNewPosition(AbstractStateManager stateManager)
        {
            IEntityWithSpeed entityWithSpeed = stateManager as IEntityWithSpeed;
            
            if (entityWithSpeed == null)
                throw new Exception("Entity that moves must implement IEntityWithSpeed");

            return Vector3.SmoothDamp(stateManager.transform.position, _targetPosition, ref _velocity,
                Time.deltaTime * entityWithSpeed.GetSpeed());
        }

        private void SetEntityPosition(AbstractStateManager stateManager, Vector3 position)
        {
            stateManager.transform.position = position;
        }

        private bool HasArrived(Vector3 target, AbstractStateManager stateManager)
        {
            float snap = 0;
            IEntityWithSnap entityWithSnap = stateManager as IEntityWithSnap;

            if (entityWithSnap != null)
                snap = entityWithSnap.GetSnapDistance();
            
            return Vector3.Distance(target, stateManager.transform.position) <= snap;
        }
    }
}