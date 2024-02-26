using Lean.Pool;
using UnityEngine;

namespace Core.Scripts.Bots
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _hp;

        public void SetDamage(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            LeanPool.Despawn(gameObject);
        }
    }
}