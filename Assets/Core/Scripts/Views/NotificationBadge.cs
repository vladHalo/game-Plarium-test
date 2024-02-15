using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Core.Scripts.Views
{
    public class NotificationBadge : MonoBehaviour
    {
        [SerializeField] private Transform _object;
        [SerializeField] private TextMeshProUGUI _textMeshPro;
        [SerializeField] private float _showAfter = 1.5f;

        private void OnEnable()
        {
            _object.localScale = Vector3.zero;
        }

        private void Start()
        {
            Invoke(nameof(ScaleToOne), _showAfter);
        }

        public void ChangeValue(int index)
        {
            _object.localScale = Vector3.zero;
            ScaleToOne();
            _textMeshPro.text = $"{index}";
        }

        private void ScaleToOne() => _object.DOScale(1f, 0.3f);
    }
}