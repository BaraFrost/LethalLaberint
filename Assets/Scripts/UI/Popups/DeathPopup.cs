using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class DeathPopup : AbstractPopup {

        [Serializable]
        public struct Data {
            public Action continueCallback;
        }

        [SerializeField]
        private Button _continueButton;

        private Data _data;

        private void Awake() {
            _continueButton.onClick.AddListener(OnContinueButtonPressed);
        }

        public void SetData(Data data) {
            _data = data;
        }

        private void OnContinueButtonPressed() {
            _data.continueCallback?.Invoke();
            HidePopup(immediately: true);
        }
    }
}
