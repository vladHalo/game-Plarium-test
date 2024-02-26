using Core.Scripts.Guns.Models;
using UnityEngine;

namespace Core.Scripts.Guns
{
    public class GunBallistic : Gun
    {
        [SerializeField] private GunRotateModel _gunRotateModel;
        [SerializeField] private float _angleInDegrees;
        [SerializeField] private BoxCollider _boxCollider;
        private Vector3 _point;
        private float _gravity;

        protected override void Start()
        {
            base.Start();
            _gravity = Physics.gravity.y;
            _gunModel.aim.localEulerAngles = new Vector3(-_angleInDegrees, 0f, 0f);
            _point = GetRandomPointInBox();
        }

        private void Update()
        {
            _gunRotateModel.RotateGun(_point, _gunModel.aim);
            if (TimeShoot())
            {
                _gunModel.bulletFactory.CreateBullet(SpeedBullet(), _gunModel, true);
                _point = GetRandomPointInBox();
            }
        }

        private float SpeedBullet()
        {
            float x = _gunRotateModel.fromToXZ.magnitude;
            float y = _gunRotateModel.fromTo.y;

            float angleInRadians = _angleInDegrees * Mathf.PI / 180;

            float v2 = _gravity * x * x /
                       (2 * (y - Mathf.Tan(angleInRadians) * x) * Mathf.Pow(Mathf.Cos(angleInRadians), 2));
            return Mathf.Sqrt(Mathf.Abs(v2));
        }

        private Vector3 GetRandomPointInBox()
        {
            if (_boxCollider == null) return Vector3.zero;

            Vector3 size = _boxCollider.size * .5f;

            float randomX = Random.Range(-size.x, size.x);
            float randomZ = Random.Range(-size.z, size.z);

            Vector3 randomPointInBox =
                new Vector3(randomX, 0, randomZ) + _boxCollider.transform.position + _boxCollider.center;

            return randomPointInBox;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_point, .6f);
        }
    }
}