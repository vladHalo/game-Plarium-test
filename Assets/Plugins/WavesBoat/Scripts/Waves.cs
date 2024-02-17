using System;
using UnityEngine;

namespace Core.Scripts.WavesBoat
{
    public class Waves : MonoBehaviour
    {
        [SerializeField] private int _dimension = 10;
        [SerializeField] private float _UVScale = 2f;
        [SerializeField] private Octave[] _octaves;

        private MeshFilter _meshFilter;
        private Mesh _mesh;

        void Start()
        {
            _mesh = new Mesh();
            _mesh.name = gameObject.name;

            _mesh.vertices = GenerateVerts();
            _mesh.triangles = GenerateTries();
            _mesh.uv = GenerateUVs();
            _mesh.RecalculateNormals();
            _mesh.RecalculateBounds();

            _meshFilter = gameObject.AddComponent<MeshFilter>();
            _meshFilter.mesh = _mesh;
        }

        public float GetHeight(Vector3 position)
        {
            var scale = new Vector3(1 / transform.lossyScale.x, 0, 1 / transform.lossyScale.z);
            var localPos = Vector3.Scale((position - transform.position), scale);

            var p1 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Floor(localPos.z));
            var p2 = new Vector3(Mathf.Floor(localPos.x), 0, Mathf.Ceil(localPos.z));
            var p3 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Floor(localPos.z));
            var p4 = new Vector3(Mathf.Ceil(localPos.x), 0, Mathf.Ceil(localPos.z));

            p1.x = Mathf.Clamp(p1.x, 0, _dimension);
            p1.z = Mathf.Clamp(p1.z, 0, _dimension);
            p2.x = Mathf.Clamp(p2.x, 0, _dimension);
            p2.z = Mathf.Clamp(p2.z, 0, _dimension);
            p3.x = Mathf.Clamp(p3.x, 0, _dimension);
            p3.z = Mathf.Clamp(p3.z, 0, _dimension);
            p4.x = Mathf.Clamp(p4.x, 0, _dimension);
            p4.z = Mathf.Clamp(p4.z, 0, _dimension);

            var max = Mathf.Max(Vector3.Distance(p1, localPos), Vector3.Distance(p2, localPos),
                Vector3.Distance(p3, localPos), Vector3.Distance(p4, localPos) + Mathf.Epsilon);
            var dist = (max - Vector3.Distance(p1, localPos))
                       + (max - Vector3.Distance(p2, localPos))
                       + (max - Vector3.Distance(p3, localPos))
                       + (max - Vector3.Distance(p4, localPos) + Mathf.Epsilon);
            var height = _mesh.vertices[index(p1.x, p1.z)].y * (max - Vector3.Distance(p1, localPos))
                         + _mesh.vertices[index(p2.x, p2.z)].y * (max - Vector3.Distance(p2, localPos))
                         + _mesh.vertices[index(p3.x, p3.z)].y * (max - Vector3.Distance(p3, localPos))
                         + _mesh.vertices[index(p4.x, p4.z)].y * (max - Vector3.Distance(p4, localPos));

            return height * transform.lossyScale.y / dist;
        }

        private Vector3[] GenerateVerts()
        {
            var verts = new Vector3[(_dimension + 1) * (_dimension + 1)];

            for (int x = 0; x <= _dimension; x++)
            for (int z = 0; z <= _dimension; z++)
                verts[index(x, z)] = new Vector3(x, 0, z);

            return verts;
        }

        private int[] GenerateTries()
        {
            var tries = new int[_mesh.vertices.Length * 6];

            for (int x = 0; x < _dimension; x++)
            {
                for (int z = 0; z < _dimension; z++)
                {
                    tries[index(x, z) * 6 + 0] = index(x, z);
                    tries[index(x, z) * 6 + 1] = index(x + 1, z + 1);
                    tries[index(x, z) * 6 + 2] = index(x + 1, z);
                    tries[index(x, z) * 6 + 3] = index(x, z);
                    tries[index(x, z) * 6 + 4] = index(x, z + 1);
                    tries[index(x, z) * 6 + 5] = index(x + 1, z + 1);
                }
            }

            return tries;
        }

        private Vector2[] GenerateUVs()
        {
            var uvs = new Vector2[_mesh.vertices.Length];

            for (int x = 0; x <= _dimension; x++)
            {
                for (int z = 0; z <= _dimension; z++)
                {
                    var vec = new Vector2((x / _UVScale) % 2, (z / _UVScale) % 2);
                    uvs[index(x, z)] = new Vector2(vec.x <= 1 ? vec.x : 2 - vec.x, vec.y <= 1 ? vec.y : 2 - vec.y);
                }
            }

            return uvs;
        }

        private int index(int x, int z)
        {
            return x * (_dimension + 1) + z;
        }

        private int index(float x, float z)
        {
            return index((int)x, (int)z);
        }

        void Update()
        {
            var verts = _mesh.vertices;
            for (int x = 0; x <= _dimension; x++)
            {
                for (int z = 0; z <= _dimension; z++)
                {
                    var y = 0f;
                    for (int o = 0; o < _octaves.Length; o++)
                    {
                        if (_octaves[o].alternate)
                        {
                            var perl = Mathf.PerlinNoise((x * _octaves[o].scale.x) / _dimension,
                                (z * _octaves[o].scale.y) / _dimension) * Mathf.PI * 2f;
                            y += Mathf.Cos(perl + _octaves[o].speed.magnitude * Time.time) * _octaves[o].height;
                        }
                        else
                        {
                            var perl = Mathf.PerlinNoise(
                                (x * _octaves[o].scale.x + Time.time * _octaves[o].speed.x) / _dimension,
                                (z * _octaves[o].scale.y + Time.time * _octaves[o].speed.y) / _dimension) - 0.5f;
                            y += perl * _octaves[o].height;
                        }
                    }

                    verts[index(x, z)] = new Vector3(x, y, z);
                }
            }

            _mesh.vertices = verts;
            _mesh.RecalculateNormals();
        }

        [Serializable]
        public struct Octave
        {
            public Vector2 speed;
            public Vector2 scale;
            public float height;
            public bool alternate;
        }
    }
}