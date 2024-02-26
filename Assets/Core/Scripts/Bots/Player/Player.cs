using System;
using Core.Scripts.Bots.Player.View;
using Core.Scripts.Map.Factories;
using UnityEngine;

namespace Core.Scripts.Bots.Player
{
    public class Player : Enemy
    {
        [SerializeField] private int _coins;
        [SerializeField] private PlayerView _playerView;
        [SerializeField] private BuoyFactory _buoyFactory;
        [SerializeField] private float _timeOutBorder;
        private float _timer;

        private void Start()
        {
            _timer = _timeOutBorder;
        }

        private void Update()
        {
            if (_buoyFactory.IsInBorder(transform))
            {
                _timer = _timeOutBorder;
                _playerView.ActiveBorderMenu(false);
                return;
            }

            _playerView.ActiveBorderMenu(true);
            _playerView.SetTextBorder(_timer);
            _timer -= Time.deltaTime;
            if (_timer > 0) return;
            _playerView.ActiveBorderMenu(false);
            _timer = _timeOutBorder;
            Die();
        }

        public void AddCoins(int count)
        {
            _coins += count;
        }

        protected override void Die()
        {
            gameObject.SetActive(false);
        }
    }
}