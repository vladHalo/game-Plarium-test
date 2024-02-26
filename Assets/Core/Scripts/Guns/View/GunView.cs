using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.Guns.View
{
    public class GunView : MonoBehaviour
    {
        [SerializeField] private Image _progress;
        private float _maxValue;

        public void Init(float maxValue)
        {
            _maxValue = maxValue;
        }

        public void SetProgress(float value)
        {
            _progress.fillAmount = 1 - value / _maxValue;
        }
    }
}