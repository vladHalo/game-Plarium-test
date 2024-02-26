using Core.Scripts.Bots.Player;
using Core.Scripts.Guns.Models;
using UnityEngine;

namespace Core.Scripts.Guns
{
    public class GunLinearRotate : GunLinear
    {
        [SerializeField] private GunRotateModel _gunRotateModel;
        [SerializeField] private Transform _target;
        private bool _canShot;

        private void SetTarget(Transform target)
        {
            _target = target;
        }

        protected override void Update()
        {
            if (_target != null)
            {
                _gunRotateModel.RotateGun(_target.position, _gunModel.aim);
            }

            if (_canShot)
            {
                base.Update();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _canShot = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                _canShot = false;
            }
        }
    }
}