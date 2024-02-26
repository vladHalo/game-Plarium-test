using System;
using UnityEngine;

namespace Core.Scripts.Guns.Models
{
    [Serializable]
    public class GunRotateModel
    {
        [SerializeField] private float _rotationSpeed;
        [SerializeField] private Transform _body;

        [HideInInspector] public Vector3 fromTo, fromToXZ;

        public void RotateGun(Vector3 target, Transform aim)
        {
            fromTo = target - aim.position;
            fromToXZ = new Vector3(fromTo.x, 0f, fromTo.z);
            Quaternion targetRotation = Quaternion.LookRotation(fromToXZ, Vector3.up);
            _body.rotation = Quaternion.Slerp(_body.rotation,
                targetRotation, Time.deltaTime * _rotationSpeed);
        }
    }
}