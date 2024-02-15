using System.IO;
using UnityEngine;

#pragma warning disable 0649
namespace ShootStage.Scripts
{
    public static class TextureSpriteUtils
    {
        #region Convert

        public static Texture2D RenderTextureToTexture2D(Camera camera, RenderTexture rt)
        {
            // Create another render texture to apply specific params
            // that we can't apply in Inspector.
            RenderTexture extraRT = new RenderTexture(
                rt.width, rt.height, rt.depth,
                RenderTextureFormat.ARGB32,
                RenderTextureReadWrite.sRGB)
            {
                antiAliasing = rt.antiAliasing
            };

            var texture = new Texture2D(extraRT.width, extraRT.height, TextureFormat.ARGB32, false);
            camera.targetTexture = extraRT;
            camera.Render();
            RenderTexture.active = extraRT;

            texture.ReadPixels(new Rect(0, 0, extraRT.width, extraRT.height), 0, 0);
            texture.Apply();

            camera.targetTexture = rt;
            camera.Render();
            RenderTexture.active = rt;

            Object.DestroyImmediate(extraRT);

            return texture;
        }

        public static Sprite Texture2DToSprite(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(0f, 0f, texture.width, texture.height), new Vector2(0.5f, 0.5f),
                100);
        }

        #endregion

        #region Save

        public static void SavePngToPersistantDataPath(Camera camera, RenderTexture rt, string relativePath)
        {
            SavePng(camera, rt, Path.Combine(Application.persistentDataPath, relativePath));
        }

        public static void SavePng(Camera camera, RenderTexture rt, string path)
        {
            var texture = RenderTextureToTexture2D(camera, rt);
            SavePng(texture, path);
            Object.DestroyImmediate(texture);
        }

        public static void SavePngToPersistantDataPath(Texture2D texture, string relativePath)
        {
            SavePng(texture, Path.Combine(Application.persistentDataPath, relativePath));
        }

        public static void SavePng(Texture2D texture, string path)
        {
            // Apply file extension just in case.
            path = Path.ChangeExtension(path, ".png");

            var directory = Path.GetDirectoryName(path);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(path)) Directory.CreateDirectory(directory);

            File.WriteAllBytes(path, texture.EncodeToPNG());
        }

        #endregion

        #region Load

        public static Texture2D LoadTexturePngFromPersistantDataPath(string relativePath)
        {
            return LoadTexturePng(Path.Combine(Application.persistentDataPath, relativePath));
        }

        public static Texture2D LoadTexturePng(string filePath)
        {
            Texture2D tex = null;

            if (File.Exists(filePath))
            {
                var fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(2, 2);
                // Auto-resize the texture dimensions.
                tex.LoadImage(fileData);
            }

            return tex;
        }

        public static Sprite LoadSpritePngFromPersistantDataPath(string relativePath)
        {
            return LoadSpritePNG(Path.Combine(Application.persistentDataPath, relativePath));
        }

        public static Sprite LoadSpritePNG(string filePath)
        {
            Texture2D texture = LoadTexturePng(filePath);
            if (texture == null) return null;
            return Texture2DToSprite(texture);
        }

        #endregion
    }
}