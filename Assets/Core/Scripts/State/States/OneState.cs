using Core.Scripts.Interfaces;
using Core.Scripts.State.Models;

namespace Core.Scripts.State.States
{
    public class OneState : IState
    {
        private readonly StateManager _state;

        private readonly OneModel _oneModel;

        public OneState(StateManager state, OneModel oneModel)
        {
            _state = state;
            _oneModel = oneModel;
        }

        public void EnterState()
        {
        }

        public void UpdateState()
        {
        }
    }
}