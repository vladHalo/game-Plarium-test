using System;
using Core.Scripts.BezierMove;
using UnityEngine;

namespace Core.Scripts.Map
{
    public class MoveItem : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private float _speed;

        private float _time;
        private float _minScale, _maxScale;
        private Transform _firstPoint, _lastPoint;

        public Action FinishMoveAction;

        private void Start()
        {
            _minScale = .1f;
            _maxScale = transform.localScale.x;
        }

        private void Update()
        {
            if (_lastPoint != null)
                BezierMove();
        }

        public void SetPointMove(Transform firstPoint, Transform lastPoint)
        {
            _firstPoint = firstPoint;
            _lastPoint = lastPoint;
            transform.position = firstPoint.position;
            _time = 0;
        }

        private void BezierMove()
        {
            transform.position = Bezier.GetPoint(
                _firstPoint.position,
                new Vector3(_firstPoint.position.x, _firstPoint.position.y + _height, _firstPoint.position.z),
                new Vector3(_lastPoint.position.x, _lastPoint.position.y + _height, _lastPoint.position.z),
                _lastPoint.position, _time);

            float scale = Mathf.Clamp(1 - _time, _minScale, _maxScale);
            transform.localScale = new Vector3(scale, scale, scale);

            _time = Mathf.Lerp(_time, 1f, _speed * Time.deltaTime);
            if (_time < 0.99f) return;

            FinishMoveAction?.Invoke();
            FinishMoveAction = null;
            _lastPoint = null;
        }
    }
}