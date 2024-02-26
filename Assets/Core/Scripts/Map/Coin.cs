using Core.Scripts.BezierMove;
using Lean.Pool;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Scripts.Map
{
    public class Coin : MonoBehaviour
    {
        [SerializeField] private int _priceMin, _priceMax;
        [SerializeField] private float _speedRotate;
        [SerializeField] private MoveItem _moveItem;
        [SerializeField] private Collider _collider;

        private void Update()
        {
            transform.Rotate(0, _speedRotate * Time.deltaTime, 0, Space.Self);
        }

        private void OnEnable()
        {
            _collider.enabled = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Bots.Player.Player bagPlayer))
            {
                _collider.enabled = false;
                _moveItem.FinishMoveAction = () =>
                {
                    LeanPool.Despawn(gameObject);
                    bagPlayer.AddCoins(Random.Range(_priceMin, _priceMax));
                };
                _moveItem.SetPointMove(transform, bagPlayer.transform);
            }
        }
    }
}