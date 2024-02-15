using DG.Tweening;
using UnityEngine;

namespace Core.Scripts.Views
{
    public class FadeItem : MonoBehaviour
    {
        [SerializeField] private float _startAnimationAfter = 0.3f;
        [SerializeField] private float _animationDuration = 0.3f;
        [SerializeField] private CanvasGroup _canvasGroup;

        private void OnEnable()
        {
            _canvasGroup.alpha = 0;
            Invoke(nameof(FadeInAfter), _startAnimationAfter);
        }

        private void OnDisable()
        {
            _canvasGroup.alpha = 0;
        }

        private void FadeInAfter() => _canvasGroup.DOFade(1f, _animationDuration);
    }
}