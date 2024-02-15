using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Core.Scripts.BezierMove;
using Lean.Pool;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scripts.Player
{
    public class BackpackPlayer : MonoBehaviour
    {
        [SerializeField] private int _capacityMax;
        [SerializeField] private float _delay;
        [SerializeField] private Transform _pointStartItems;
        [SerializeField] private MovementPlayer _movementPlayer;
        [SerializeField] private List<MoveItem> _items;

        public IEnumerator GetItems(List<MoveItem> items, Transform firstPoint)
        {
            while (_items.Count < _capacityMax)
            {
                if (items.Count != 0)
                {
                    var last = items.Last();
                    if (_items.Count < _capacityMax)
                    {
                        var lastPoint = _items.Count == 0 ? _pointStartItems : _items.Last().transform;
                        last.SetPointMove(firstPoint, lastPoint);
                        _items.Add(last);
                    }

                    items.Remove(items.Last());
                }

                if (_items.Count == 1)
                {
                    _items[0].FinishMoveAction += () => { _movementPlayer.SetLayerWeight(1, 1, 0.25f); };
                }

                yield return new WaitForSeconds(_delay);
            }
        }

        public IEnumerator SetItems(params Transform[] startPoints)
        {
            for (int i = _items.Count - 1; i >= 0; i--)
            {
            }

            return null;
        }

        private void RefreshPosition()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                var result = i == 0 ? _pointStartItems : _items[i - 1].transform;
                _items[i].SetPointMove(null, result);
            }
        }

        [Button]
        private void ClearBackpack()
        {
            _items.ForEach(x => LeanPool.Despawn(x));
            _items.Clear();
            _movementPlayer.SetLayerWeight(1, 0, 0.25f);
        }
    }
}