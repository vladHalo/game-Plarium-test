using System.Collections.Generic;
using Lean.Pool;
using Sirenix.OdinInspector;
using Unity.Mathematics;
using UnityEngine;

namespace Core.Scripts.Map.Factories
{
    public class BuoyFactory : MonoBehaviour
    {
        [SerializeField] private Factory _factory;

        [InfoBox("Only even numbers")] [SerializeField]
        private int _count;

        [SerializeField] private Vector2 _widthHeight;
        [SerializeField] private List<Transform> _buoys;

        private void OnValidate()
        {
            if (_count % 2 != 0)
            {
                _count = 0;
            }
        }

        public bool IsInBorder(Transform player)
        {
            if (player.position.x > _widthHeight.x || player.position.x < -_widthHeight.x)
            {
                return false;
            }

            return true;
        }

        [Button]
        private void SetBorder()
        {
            _buoys.ForEach(x => LeanPool.Despawn(x));
            _buoys.Clear();

            float maxHeight = 0;

            for (int i = 0; i < _count; i++)
            {
                var buoy = _factory.Create<Transform>(Vector3.zero);
                _buoys.Add(buoy);

                if (i % 2 == 0)
                {
                    buoy.position = new Vector3(-_widthHeight.x, 0, maxHeight);
                }
                else
                {
                    buoy.position = new Vector3(_widthHeight.x, 0, maxHeight);
                    maxHeight += _widthHeight.y;
                }
            }
        }
    }
}