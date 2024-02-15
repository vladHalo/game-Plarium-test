using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Views
{
    public class LoadBar : MonoBehaviour
    {
        [SerializeField] private Slider _progress;
        [SerializeField] private Image _image;
        [SerializeField] private Gradient _gradient;
        [ShowIf("_progress")] [SerializeField] private TextMeshProUGUI _textMesh;
        [ShowIf("_textMesh")] [SerializeField] private string _suffix;

        public void AddValue(float value)
        {
            _progress.value += value;
            ChangeValue();
        }

        public void SetValue(float value)
        {
            _progress.value = value;
            ChangeValue();
        }

        private void ChangeValue()
        {
            _image.color = _gradient.Evaluate(_progress.value);
            if (_textMesh != null)
                _textMesh.text = $"{_progress.value * 100}{_suffix}";
        }
    }
}