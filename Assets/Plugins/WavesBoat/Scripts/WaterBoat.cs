using UnityEngine;

namespace Core.Scripts.WavesBoat
{
    [RequireComponent(typeof(WaterFloat))]
    public class WaterBoat : MonoBehaviour
    {
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Transform _motor;
        [SerializeField] private float _steerPower = 500f;
        [SerializeField] private float _power = 5f;
        [SerializeField] private float _maxSpeed = 10f;
        [SerializeField] private float _drag = 0.1f;

        private Rigidbody _rigidbody;
        private Quaternion _startRotation;

        private ParticleSystem _particleSystem;
        //private Camera _camera;
        //private Vector3 _camVel;


        public void Awake()
        {
            _particleSystem = GetComponentInChildren<ParticleSystem>();
            _rigidbody = GetComponent<Rigidbody>();
            _startRotation = _motor.localRotation;
            //_camera = Camera.main;
            if (_particleSystem != null)
            {
                _particleSystem.Play();
            }
        }

        public void FixedUpdate()
        {
            var forceDirection = transform.forward;
            var steer = _joystick.Horizontal;

            _rigidbody.AddForceAtPosition(steer * transform.right * _steerPower / 100f, _motor.position);

            var forward = Vector3.Scale(new Vector3(1, 0, 1), transform.forward);
            var targetVel = Vector3.zero;

            PhysicsHelper.ApplyForceToReachVelocity(_rigidbody, forward * _maxSpeed, _power);

            _motor.SetPositionAndRotation(_motor.position,
                transform.rotation * _startRotation * Quaternion.Euler(0, 30f * steer, 0));

            var movingForward = Vector3.Cross(transform.forward, _rigidbody.velocity).y < 0;

            _rigidbody.velocity =
                Quaternion.AngleAxis(
                    Vector3.SignedAngle(_rigidbody.velocity, (movingForward ? 1f : 0f) * transform.forward,
                        Vector3.up) * _drag, Vector3.up) * _rigidbody.velocity;

            //camera position
            //Camera.transform.LookAt(transform.position + transform.forward * 6f + transform.up * 2f);
            //Camera.transform.position = Vector3.SmoothDamp(Camera.transform.position, transform.position + transform.forward * -8f + transform.up * 2f, ref CamVel, 0.05f);
        }
    }
}