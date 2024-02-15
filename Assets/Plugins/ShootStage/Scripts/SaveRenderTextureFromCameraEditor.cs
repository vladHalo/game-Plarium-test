using UnityEditor;
using UnityEngine;

namespace ShootStage.Scripts
{
    [CustomEditor(typeof(SaveRenderTextureFromCamera))]
    public class SaveRenderTextureFromCameraEditor : UnityEditor.Editor
    {
        private const string menuPath = "Tools/halo-package/Save render texture with name of selected object";

        private enum SquareSize
        {
            x2048,
            x4096,
        }

        [MenuItem(menuPath + " #&f")]
        private static void SaveRenderTexture()
        {
            var obj = FindObjectOfType<SaveRenderTextureFromCamera>();
            if (obj == null)
            {
                Debug.LogError($"Didn't find object with component {nameof(SaveRenderTextureFromCamera)}.");
                return;
            }

            if (Selection.activeGameObject == null)
            {
                Debug.LogError("None GameObject selected.");
                return;
            }

            obj.Save(Selection.activeGameObject.name);
        }

        private SaveRenderTextureFromCamera _target;
        private SquareSize _squareSize;

        private void OnEnable()
        {
            _target = target as SaveRenderTextureFromCamera;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("Save Render Texture as Sprite"))
            {
                _target.Save();
            }

            EditorGUILayout.HelpBox(
                $"You can call menu command \"{menuPath}\" to save as Sprite with name of selected GameObject.",
                MessageType.Info);

            EditorGUILayout.Space();

            _squareSize = (SquareSize)EditorGUILayout.EnumPopup("Size: ", _squareSize);
            if (GUILayout.Button("Apply size to Game View"))
            {
                switch (_squareSize)
                {
                    case SquareSize.x2048:
                        SetGameViewSize(2048);
                        break;
                    case SquareSize.x4096:
                        SetGameViewSize(4096);
                        break;
                }
            }
        }

        private void SetGameViewSize(int wh)
        {
            var size = new GameViewSizeHelper.GameViewSize
            {
                type = GameViewSizeHelper.GameViewSizeType.FixedResolution,
                width = wh, height = wh, baseText = $"Square {wh}"
            };

            if (!GameViewSizeHelper.Contains(GameViewSizeHelper.GetCurrentGameViewSizeGroupType(), size))
            {
                GameViewSizeHelper.AddCustomSize(GameViewSizeHelper.GetCurrentGameViewSizeGroupType(), size);
            }

            GameViewSizeHelper.ChangeGameViewSize(GameViewSizeHelper.GetCurrentGameViewSizeGroupType(), size);
        }
    }
}