using JetBrains.Annotations;
using Managers;

namespace State
{
    public interface IStateWithInput
    {
        [CanBeNull] IState HandleInput(AbstractStateManager stateManager);
    }
}