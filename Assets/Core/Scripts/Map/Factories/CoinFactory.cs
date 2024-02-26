using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Scripts.Map.Factories
{
    public class CoinFactory : MonoBehaviour
    {
        [SerializeField] private Factory _factory;

        [Button]
        public void Create(Vector3 position)
        {
            _factory.Create<Coin>(position);
        }
    }
}