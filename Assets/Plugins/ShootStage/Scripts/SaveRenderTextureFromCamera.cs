#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

#pragma warning disable 0649
namespace ShootStage.Scripts
{
    [RequireComponent(typeof(Camera))]
    public class SaveRenderTextureFromCamera : MonoBehaviour
    {
        [SerializeField] private RenderTexture _rt;

        public void Save(string fileName = null)
        {
            string path = AssetDatabase.GetAssetPath(_rt) + ".png";
            if (fileName != null)
            {
                string dir = Path.GetDirectoryName(path);
                string ext = Path.GetExtension(path);
                path = Path.Combine(dir, fileName + ext);
            }

            var cam = GetComponent<Camera>();
            TextureSpriteUtils.SavePng(cam, _rt, path);

            AssetDatabase.Refresh();

            TextureImporter tImporter = AssetImporter.GetAtPath(path) as TextureImporter;
            tImporter.textureType = TextureImporterType.Sprite;
            tImporter.SaveAndReimport();
        }
    }
}
#endif