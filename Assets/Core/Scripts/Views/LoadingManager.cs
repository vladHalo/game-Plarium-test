using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using Sirenix.OdinInspector;

namespace Core.Scripts.Views
{
    public class LoadingManager : MonoBehaviour
    {
        public static LoadingManager Instance { get; private set; }

        [SerializeField] private float _fadeDuration = 1f;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private LoadBar _loadBar;

        private float _time;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (Instance != this)
                {
                    Destroy(Instance);
                    Instance = this;
                    DontDestroyOnLoad(this);
                }
            }
        }

        [Button]
        public void LoadScene(string level)
        {
            StartCoroutine(Fade(level));
            if (_loadBar != null)
                StartCoroutine(LoadBar());
        }

        private IEnumerator LoadBar()
        {
            _loadBar.SetValue(0);
            float time = _fadeDuration * 2.0f;
            while (time > 0)
            {
                time -= .01f;
                _loadBar.AddValue(.01f);
                yield return new WaitForSeconds(.01f);
            }

            _loadBar.SetValue(1);
        }

        private IEnumerator Fade(string level)
        {
            _canvasGroup.alpha = 0.0f;
            _canvasGroup.gameObject.SetActive(true);

            var halfDuration = _fadeDuration / 2.0f;

            _canvasGroup.DOFade(1f, halfDuration);
            yield return new WaitForSeconds(_fadeDuration);

            SceneManager.LoadScene(level);

            _canvasGroup.DOFade(0f, halfDuration);
            yield return new WaitForSeconds(_fadeDuration);
            _canvasGroup.gameObject.SetActive(false);
        }
    }
}