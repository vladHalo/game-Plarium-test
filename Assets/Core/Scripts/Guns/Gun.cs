using Core.Scripts.Guns.Models;
using Core.Scripts.Guns.View;
using UnityEngine;

namespace Core.Scripts.Guns
{
    public abstract class Gun : MonoBehaviour
    {
        [SerializeField] protected GunModel _gunModel;
        [SerializeField] private GunView _gunView;

        private float _timer;

        protected virtual void Start()
        {
            if (_gunView != null)
                _gunView.Init(_gunModel.shootDelay);
            ResetTime();
        }

        protected bool TimeShoot()
        {
            _timer -= Time.deltaTime;
            if (_gunView != null)
                _gunView.SetProgress(_timer);

            if (_timer > 0) return false;
            ResetTime();
            return true;
        }

        private void ResetTime() => _timer = _gunModel.shootDelay;
    }
}