using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ClosablePopup : AbstractPopup {

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private AudioSource _popupAudio;

        protected virtual void Awake() {
            _closeButton.onClick.AddListener(() => HidePopup());
        }

        public override void ShowPopup() {
            base.ShowPopup();
            _popupAudio.Play();
        }

        public override void HidePopup(bool immediately = false) {
            base.HidePopup(immediately);
            _popupAudio.Play();
        }
    }
}