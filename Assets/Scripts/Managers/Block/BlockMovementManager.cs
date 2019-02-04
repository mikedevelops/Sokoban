using System;
using Agents;
using Service;
using State;
using State.Block;
using UnityEngine;

namespace Managers.Block
{
    public class BlockMovementManager: AbstractMovementManager, IEntityWithOffset
    {
        public BlockState initialState;
        public EntityCollisionService collisionService;
        
        private readonly Vector3 _offset = new Vector3(0, 0.4f, 0);

        protected override IState GetInitialState()
        {
            switch (initialState)
            {
                case BlockState.Idle:
                    return new BlockIdleState(this);
                default:
                    throw new Exception($"Unable to transition to \"{initialState}\"");
            }
        }

        public Vector3 GetOffset()
        {
            return _offset;
        }

        public void SetPosition(Vector3 position)
        {
            Vector3 offsetPosition = new Vector3(position.x, 0f, position.z) + GetOffset();
            transform.position = offsetPosition;
        }
    }
}