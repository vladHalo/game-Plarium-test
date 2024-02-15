using Core.Scripts.Interfaces;
using Core.Scripts.State.Models;

namespace Core.Scripts.State.States
{
    public class TwoState : IState
    {
        private readonly StateManager _state;

        private readonly TwoModel _twoModel;

        public TwoState(StateManager state, TwoModel twoModel)
        {
            _state = state;
            _twoModel = twoModel;
        }

        public void EnterState()
        {
        }

        public void UpdateState()
        {
        }
    }
}