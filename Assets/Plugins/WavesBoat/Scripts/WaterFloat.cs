using UnityEngine;

namespace Core.Scripts.WavesBoat
{
    [RequireComponent(typeof(Rigidbody))]
    public class WaterFloat : MonoBehaviour
    {
        [SerializeField] private float _airDrag = 1;
        [SerializeField] private float _waterDrag = 10;
        [SerializeField] private bool _affectDirection = true;
        [SerializeField] private bool _attachToSurface = false;
        [SerializeField] private Transform[] _floatPoints;

        private Rigidbody _rigidbody;
        private Waves _waves;

        private float _waterLine;
        private Vector3[] _waterLinePoints;

        private Vector3 _smoothVectorRotation;
        private Vector3 _targetUp;
        private Vector3 _centerOffset;

        private Vector3 Center { get { return transform.position + _centerOffset; } }

        void Awake()
        {
            _waves = FindObjectOfType<Waves>();
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;

            _waterLinePoints = new Vector3[_floatPoints.Length];
            for (int i = 0; i < _floatPoints.Length; i++)
                _waterLinePoints[i] = _floatPoints[i].position;
            _centerOffset = PhysicsHelper.GetCenter(_waterLinePoints) - transform.position;

        }

        void FixedUpdate()
        {
            var newWaterLine = 0f;
            var pointUnderWater = false;

            for (int i = 0; i < _floatPoints.Length; i++)
            {
                _waterLinePoints[i] = _floatPoints[i].position;
                _waterLinePoints[i].y = _waves.GetHeight(_floatPoints[i].position);
                newWaterLine += _waterLinePoints[i].y / _floatPoints.Length;
                if (_waterLinePoints[i].y > _floatPoints[i].position.y)
                    pointUnderWater = true;
            }

            var waterLineDelta = newWaterLine - _waterLine;
            _waterLine = newWaterLine;

            _targetUp = PhysicsHelper.GetNormal(_waterLinePoints);

            var gravity = Physics.gravity;
            _rigidbody.drag = _airDrag;
            if (_waterLine > Center.y)
            {
                _rigidbody.drag = _waterDrag;
                if (_attachToSurface)
                {
                    _rigidbody.position = new Vector3(_rigidbody.position.x, _waterLine - _centerOffset.y, _rigidbody.position.z);
                }
                else
                {
                    gravity = _affectDirection ? _targetUp * -Physics.gravity.y : -Physics.gravity;
                    transform.Translate(Vector3.up * waterLineDelta * 0.9f);
                }
            }
            _rigidbody.AddForce(gravity * Mathf.Clamp(Mathf.Abs(_waterLine - Center.y),0,1));

            if (pointUnderWater)
            {
                _targetUp = Vector3.SmoothDamp(transform.up, _targetUp, ref _smoothVectorRotation, 0.2f);
                _rigidbody.rotation = Quaternion.FromToRotation(transform.up, _targetUp) * _rigidbody.rotation;
            }

        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            if (_floatPoints == null)
                return;

            for (int i = 0; i < _floatPoints.Length; i++)
            {
                if (_floatPoints[i] == null)
                    continue;

                if (_waves != null)
                {

                    Gizmos.color = Color.red;
                    Gizmos.DrawCube(_waterLinePoints[i], Vector3.one * 0.3f);
                }

                Gizmos.color = Color.green;
                Gizmos.DrawSphere(_floatPoints[i].position, 0.1f);

            }

            if (Application.isPlaying)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(new Vector3(Center.x, _waterLine, Center.z), Vector3.one * 1f);
                Gizmos.DrawRay(new Vector3(Center.x, _waterLine, Center.z), _targetUp * 1f);
            }
        }
    }
}