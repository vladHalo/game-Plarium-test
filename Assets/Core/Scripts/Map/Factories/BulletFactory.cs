using Core.Scripts.Guns.Models;
using Lean.Pool;
using UnityEngine;

namespace Core.Scripts.Map.Factories
{
    public class BulletFactory : MonoBehaviour
    {
        [SerializeField] private Factory _factory;

        public void CreateBullet(float speed, GunModel gunModel, bool isGravity = false)
        {
            var bullet = _factory.Create<Bullet>(gunModel.aim.position, gunModel.aim.rotation);
            LeanPool.Despawn(bullet, 25);
            bullet.rigidbody.velocity = gunModel.aim.forward * speed;
            bullet.rigidbody.useGravity = isGravity;
            bullet.transform.localScale = gunModel.sizeBullet;
            bullet.damage = gunModel.damage;
        }
    }
}