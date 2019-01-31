using System.Collections.Generic;
using System.Linq;
using Sirenix.Utilities;
using State;
using UnityEngine;

namespace Managers
{
    public abstract class AbstractStateManager: MonoBehaviour
    {        
        private List<IState> _state;
        private readonly int _stateHistory = 2;
        
        protected abstract IState GetInitialState();

        private void Start()
        {
            _state = new List<IState>(_stateHistory);
            SetState(GetInitialState());
        }

        private void Update()
        {
            IState nextState = GetState().Update(this);
            
            if (nextState != null)
                SetState(nextState);
        }

        public void SetState(IState state)
        {
            IStateWithEnter enterState = state as IStateWithEnter;

            if (_state.Count > 0)
            {
                IStateWithExit exitState = _state.Last() as IStateWithExit;
                exitState?.Exit(this);
            }

            enterState?.Enter(this);
            
            _state.Add(state);
            
            if (_state.Count > _stateHistory)
                _state.RemoveAt(0);
        }

        public IState GetState()
        {
            return _state.Last();
        }
    }
}