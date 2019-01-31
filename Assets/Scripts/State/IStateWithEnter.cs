using Managers;

namespace State
{
    public interface IStateWithEnter
    {
        void Enter(AbstractStateManager stateManager);
    }
}