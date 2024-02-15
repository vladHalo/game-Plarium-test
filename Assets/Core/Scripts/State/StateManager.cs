using Core.Scripts.Interfaces;
using Core.Scripts.State.Models;
using Core.Scripts.State.States;
using UnityEngine;

namespace Core.Scripts.State
{
    public class StateManager : MonoBehaviour
    {
        public Transform pointBirdsFinish;
        public OneState OneState;
        public TwoState TwoState;

        [SerializeField] private OneModel _oneModel;
        [SerializeField] private TwoModel _twoModel;

        private IState _currentState;

        private void Start()
        {
            OneState = new OneState(this, _oneModel);
            TwoState = new TwoState(this, _twoModel);
            _currentState = OneState;
        }

        private void Update()
        {
            _currentState.UpdateState();
        }

        public void SetState(IState newState)
        {
            _currentState = newState;
            _currentState.EnterState();
        }
    }
}