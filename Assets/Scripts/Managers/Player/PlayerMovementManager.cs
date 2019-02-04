using System;
using Agents;
using Instructions;
using Service;
using Sirenix.OdinInspector;
using State;
using State.Player;
using UnityEngine;

namespace Managers.Player
{
    public class PlayerMovementManager: AbstractMovementManager, IEntityWithOffset
    {
        public PlayerInputService playerInputService;
        public PlayerState initialState = PlayerState.Idle;
        public LayerMask interactionMask;
        
        private readonly Vector3 _offset = new Vector3(0, 0.33f, 0);
        
        protected override IState GetInitialState()
        {
            switch (initialState)
            {
                case PlayerState.Idle:
                    return new PlayerIdleState(this);
                default:
                    throw new Exception($"Unable to transition to \"{initialState}\"");
            }
        }

        [Button]
        public void Teleport(Vector2Int position)
        {
            Vector3 offsetPosition = new Vector3(position.x, 0, position.y) + GetOffset();
            transform.position = offsetPosition;
        }

        public Vector3 GetOffset()
        {
            return _offset;
        }

        public void SetPosition(Vector3 position)
        {
            Vector3 offsetPosition = new Vector3(position.x, 0, position.y) + _offset;
            transform.position = offsetPosition;
        }

        public override bool IsValidMove(MovementInstruction instruction)
        {           
            if (!base.IsValidMove(instruction))
                return false;

            if (WillPushEntity(instruction))
            {
                MovementInstruction pushedEntityDirection = new MovementInstruction(instruction.Direction * 2);  
                
                return base.IsValidMove(pushedEntityDirection);
            }
                
            
            return true;
        }

        private bool WillPushEntity(MovementInstruction instruction)
        {
            Vector3 instructionDirection = new Vector3(instruction.Direction.x, GetOffset().y, instruction.Direction.y);
            
            return Physics.Raycast(transform.position, instructionDirection, 1f, interactionMask);
        }
    }
}