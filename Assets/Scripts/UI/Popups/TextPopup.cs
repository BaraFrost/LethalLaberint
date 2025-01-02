using System;
using TMPro;
using UnityEngine;

namespace UI {

    public class TextPopup : AbstractTemporaryPopup {

        public enum Type {
            middle,
            rightDown,
        }

        public struct Data {
            public string text;
            public Type type;
        }

        [Serializable]
        private class TextByType {
            public Type type;
            public GameObject textGroup;
            public TextMeshProUGUI text;
        }

        [SerializeField]
        private TextByType[] _texts;

        public void SetData(Data data) {
            DisableTexts();
            foreach (var textWithType in _texts) {
                if(data.type == textWithType.type) {
                    textWithType.textGroup.SetActive(true);
                    textWithType.text.text = data.text;
                }
            }
        }

        private void DisableTexts() {
            foreach (var text in _texts) {
                text.textGroup.SetActive(false);
            }
        }
    }
}
