using Data;
using Infrastructure;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {

    public class WinPopup : AbstractPopup {

        [SerializeField]
        private TextMeshProUGUI _textLabel;

        [SerializeField]
        private LocalizationText _text;

        [SerializeField]
        private Button _continueButton;

        private Action _continueCallback;

        private void Awake() {
            _continueButton.onClick.AddListener(OnContinueButtonClicked);
        }

        public void SetData(Action continueCallback, int money) {
            _textLabel.text = string.Format(_text.GetText(), money, Account.Instance.RequiredMoney);
            _continueCallback = continueCallback;
        }

        private void OnContinueButtonClicked() {
            _continueCallback?.Invoke();
        }
    }
}

