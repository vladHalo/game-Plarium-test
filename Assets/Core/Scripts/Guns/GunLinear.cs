using UnityEngine;

namespace Core.Scripts.Guns
{
    public class GunLinear : Gun
    {
        [SerializeField] private float _speedBullet;

        protected virtual void Update()
        {
            if (TimeShoot())
            {
                _gunModel.bulletFactory.CreateBullet(_speedBullet, _gunModel);
            }
        }
    }
}