using System;
using Lean.Pool;
using UnityEngine;

[Serializable]
public class Factory
{
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _parent;

    public T Create<T>(Vector3 position)
    {
        return LeanPool.Spawn(_prefab, position, _prefab.transform.rotation, _parent).GetComponent<T>();
    }

    public T Create<T>(Vector3 position, Quaternion quaternion)
    {
        return LeanPool.Spawn(_prefab, position, quaternion, _parent).GetComponent<T>();
    }
}