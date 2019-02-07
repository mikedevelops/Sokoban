using System;
using Agents;
using Instructions;
using JetBrains.Annotations;
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

        public override bool IsValidMove(MovementInstruction instruction, int? playerLayer = null)
        {           
            if (!base.IsValidMove(instruction))
                return false;

            AbstractMovementManager entityToPush = GetEntityToPush(instruction);

            if (entityToPush == null)
                return true;

            // Determine where the pushed entity will land
            MovementInstruction pushedEntityDirection = new MovementInstruction(instruction.Direction * 2);  
            
            // Validate the position the pushed entity will land
            return base.IsValidMove(pushedEntityDirection, entityToPush.gameObject.layer);
        }

        [CanBeNull]
        private AbstractMovementManager GetEntityToPush(MovementInstruction instruction)
        {
            Vector3 instructionDirection = new Vector3(instruction.Direction.x, GetOffset().y, instruction.Direction.y);
            RaycastHit entity;
            
            Physics.Raycast(transform.position, instructionDirection, out entity, 1f, interactionMask);
            
            if (entity.collider == null)
                return null;

            AbstractMovementManager entityMovementManager = entity.collider.GetComponent<AbstractMovementManager>();

            return entityMovementManager;
        }
    }
}