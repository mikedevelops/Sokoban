using Managers;

namespace State
{
    public interface IStateWithExit
    {
        void Exit(AbstractStateManager stateManager);
    }
}