using System.Collections;
using UnityEngine;

namespace UI {

    public class AbstractTemporaryPopup : AbstractPopup {

        [SerializeField]
        private float _showTime;

        private Coroutine _hideCoroutine;

        protected override void OnPopupShowed() {
            base.OnPopupShowed();
            _hideCoroutine = StartCoroutine(HideCoroutine());
        }

        public override void HidePopup(bool immediately = false) {
            base.HidePopup();
            StopCoroutine(_hideCoroutine);
        }

        private IEnumerator HideCoroutine() {
            yield return new WaitForSeconds(_showTime);
            HidePopup();
        }
    }
}
