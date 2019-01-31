using JetBrains.Annotations;
using Managers;

namespace State
{
    public interface IState
    {
        [CanBeNull] IState Update(AbstractStateManager stateManager);
    }
}