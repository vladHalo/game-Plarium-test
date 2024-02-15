using System;
using UnityEngine;

namespace Core.Scripts.BezierMove
{
    public class MoveItem : MonoBehaviour
    {
        [SerializeField] private float _height;
        [SerializeField] private float _speed;

        private Vector3 _offset;
        private float _time;
        private float _minScale, _maxScale;
        private Transform _firstPoint, _lastPoint;

        public Action FinishMoveAction;

        private void Start()
        {
            _minScale = transform.localScale.x / 2;
            _maxScale = transform.localScale.x;
        }

        private void FixedUpdate()
        {
            BezierMove();
        }
        
        public void SetPointMove(Transform firstPoint, Transform lastPoint)
        {
            _firstPoint = firstPoint;
            _lastPoint = lastPoint;
            transform.position = firstPoint.position;
            _time = 0;
            _offset = Vector3.zero;
        }

        private void BezierMove()
        {
            transform.position = Bezier.GetPoint(
                _firstPoint.position,
                new Vector3(_firstPoint.position.x, _firstPoint.position.y + _height, _firstPoint.position.z),
                new Vector3(_lastPoint.position.x, _lastPoint.position.y + _height, _lastPoint.position.z),
                _lastPoint.position + _offset, _time);

            float scale = Mathf.Clamp(_time, _minScale, _maxScale);
            transform.localScale = new Vector3(scale, scale, scale);
            ResetTransform();
        }

        private void ResetTransform()
        {
            _time = Mathf.Lerp(_time, 1f, _speed * Time.deltaTime);
            if (_time < 0.99f) return;
            transform.localScale = new Vector3(_maxScale, _maxScale, _maxScale);
            transform.position = _lastPoint.position + _offset;
            FinishMoveAction?.Invoke();
            FinishMoveAction = null;
        }
    }
}