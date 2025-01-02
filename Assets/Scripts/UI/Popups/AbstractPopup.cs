using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace UI {

    public class AbstractPopup : MonoBehaviour {

        [SerializeField]
        private CanvasGroup _group;

        [SerializeField]
        private float _fadeTime;

        private Coroutine _fadeCoroutine;

        public bool IsActive { get; private set; }

        public virtual void ShowPopup() {
            if(IsActive) {
                return;
            }
            IsActive = true;
            gameObject.SetActive(true);
            _group.alpha = 0;
            SetActive(true);
        }

        public virtual void HidePopup(bool immediately = false) {
            if (!IsActive) {
                return;
            }
            if(immediately) {
                gameObject.SetActive(false);
                _group.alpha = 0;
                return;
            }
            _group.alpha = 1;
            SetActive(false);
        }

        private void SetActive(bool enable) {
            if (_fadeCoroutine != null) {
                StopCoroutine(_fadeCoroutine);
            }
            _fadeCoroutine = StartCoroutine(FadeCoroutine(enable));
        }

        private IEnumerator FadeCoroutine(bool enable) {
            var startAlpha = _group.alpha;
            var targetAlpha = enable ? 1 : 0;
            var elapsedTime = 0f;

            while (elapsedTime < _fadeTime) {
                elapsedTime += Time.deltaTime;
                var t = elapsedTime / _fadeTime;
                _group.alpha = Mathf.Lerp(startAlpha, targetAlpha, t);
                yield return null;
            }

            _group.alpha = targetAlpha;
            gameObject.SetActive(enable);
            IsActive = enable;
        }
    }
}

