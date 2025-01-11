using TMPro;
using UnityEngine;

namespace UI {

    public class ClosableTextPopup : ClosablePopup {

        public struct Data {
            public string text;
        }

        [SerializeField]
        private TextMeshProUGUI _textLabel;

        public void SetData(Data data) {
            _textLabel.text = data.text;
        }
    }
}