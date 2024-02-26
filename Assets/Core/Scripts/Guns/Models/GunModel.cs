using System;
using Core.Scripts.Map.Factories;
using UnityEngine;

namespace Core.Scripts.Guns.Models
{
    [Serializable]
    public class GunModel
    {
        public int damage;
        public float shootDelay;
        public Transform aim;
        public Vector3 sizeBullet;
        public BulletFactory bulletFactory;
    }
}