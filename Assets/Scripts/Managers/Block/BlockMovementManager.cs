using System;
using Service;
using State;
using State.Block;
using UnityEngine;

namespace Managers.Block
{
    public class BlockMovementManager: AbstractMovementManager
    {
        public BlockState initialState;
        public EntityCollisionService collisionService;

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
    }
}