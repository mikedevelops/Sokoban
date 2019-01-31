using Instructions;
using Managers;
using Managers.Player;
using State.Entity;
using UnityEngine;

namespace State.Player
{
    public class PlayerIdleState : IState, IStateWithEnter, IStateWithExit
    {
        private readonly PlayerMovementManager _playerMovementManager;
        
        public PlayerIdleState(PlayerMovementManager playerMovementManager)
        {
            _playerMovementManager = playerMovementManager;
        }

        private void HandleInput(MovementInstruction instruction)
        {           
            if (_playerMovementManager.GetState() == this && _playerMovementManager.IsValidMove(instruction))
            {
                _playerMovementManager.SetState(new EntityMovingState(_playerMovementManager, instruction));
            }
        }

        public void Enter(AbstractStateManager stateManager)
        {
            _playerMovementManager.playerInputService.OnInput += HandleInput;
            LevelManager.Instance.CheckWinCondition();
        }

        public IState Update(AbstractStateManager stateManager)
        {
            return null;
        }

        public void Exit(AbstractStateManager stateManager)
        {
            _playerMovementManager.playerInputService.OnInput -= HandleInput;
        }
    }
}