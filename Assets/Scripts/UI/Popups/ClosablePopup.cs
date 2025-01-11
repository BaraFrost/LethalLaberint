using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ClosablePopup : AbstractPopup {

        [SerializeField]
        private Button _closeButton;

        protected virtual void Awake() {
            _closeButton.onClick.AddListener(() => HidePopup());
        }
    }
}