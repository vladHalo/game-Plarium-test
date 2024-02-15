using System.Collections;
using System.Collections.Generic;
using Core.Scripts.Views.Models;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UI;
using Sirenix.Utilities;

namespace Core.Scripts.Views
{
    public class CanvasChangerManager : MonoBehaviour
    {
        [SerializeField] private float _delay = .2f, _durationFade = .3f;
        [SerializeField] private List<CanvasModel> _canvasModels;

        private WaitForSeconds _waitForSeconds;
        private CanvasGroup _lastCanvas;

        private void Start()
        {
            _waitForSeconds = new WaitForSeconds(_delay);

            foreach (var i in _canvasModels)
            {
                if (i.canvasGroup == null) continue;

                i.canvasGroup.alpha = 0;
                i.canvasGroup.gameObject.SetActive(false);
            }

            _canvasModels[0].canvasGroup.alpha = 1;
            _canvasModels[0].canvasGroup.gameObject.SetActive(true);
            _lastCanvas = _canvasModels[0].canvasGroup;

            _canvasModels.ForEach((item, index) =>
            {
                if (item.button != null)
                    item.button.onClick.AddListener(() => ShowHideCanvas(index));
            });
        }

        private void OnDestroy()
        {
            _canvasModels.ForEach(item =>
            {
                if (item.button != null)
                    item.button.onClick.RemoveAllListeners();
            });
        }

        public void ShowHideCanvas(int index)
        {
            HideCanvas();
            if (_canvasModels[index].canvasGroup != null)
            {
                ShowCanvas(_canvasModels[index].canvasGroup);
                _lastCanvas = _canvasModels[index].canvasGroup;
            }
        }

        public void AddListener(Button button, CanvasGroup canvasGroup)
        {
            _canvasModels.Add(new CanvasModel(button, canvasGroup));
            button.onClick.AddListener(() => ShowHideCanvas(_canvasModels.Count - 1));
        }

        public void AddListener(Button button, CanvasGroup canvasGroup, UnityAction action)
        {
            _canvasModels.Add(new CanvasModel(button, canvasGroup));
            button.onClick.AddListener(action);
        }

        private void ShowCanvas(CanvasGroup canvas) =>
            StartCoroutine(ProcessCanvasGroup(canvas, 0f, 1f, true));

        private void HideCanvas() =>
            StartCoroutine(ProcessCanvasGroup(_lastCanvas, 1f, 0f, false));

        private IEnumerator ProcessCanvasGroup(CanvasGroup canvasGroup, float initAlpha, float alpha, bool enable)
        {
            canvasGroup.alpha = initAlpha;
            if (enable) canvasGroup.gameObject.SetActive(true);
            yield return _waitForSeconds;
            canvasGroup.DOFade(alpha, _durationFade);
            yield return _waitForSeconds;
            canvasGroup.gameObject.SetActive(enable);
        }
    }
}