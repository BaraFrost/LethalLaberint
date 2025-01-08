using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class ClosablePopup : AbstractPopup {

        public struct Data {
            public string text;
        }

        [SerializeField]
        private Button _closeButton;

        [SerializeField]
        private TextMeshProUGUI _textLabel;

        private void Awake() {
            _closeButton.onClick.AddListener(() => HidePopup());
        }

        public void SetData(Data data) {
            _textLabel.text = data.text;
        }
    }
}