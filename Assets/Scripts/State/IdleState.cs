using Managers;

namespace State
{
    public class IdleState: IState
    {
        public IState Update(AbstractStateManager stateManager)
        {
            return null;
        }
    }
}