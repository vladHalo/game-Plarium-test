using Lean.Pool;
using UnityEngine;

namespace Core.Scripts.Map
{
    public class DespawnBulletZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bullet bullet))
            {
                LeanPool.Despawn(bullet);
            }
        }
    }
}
