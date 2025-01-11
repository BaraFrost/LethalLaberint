using Data;
using Infrastructure;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class MenuLogic : MonoBehaviour {

        [SerializeField]
        private TextMeshProUGUI _stageText;
        [SerializeField]
        private TextMeshProUGUI _dayText;
        [SerializeField]
        private TextMeshProUGUI _moneyText;
        [SerializeField]
        private TextMeshProUGUI _quotaText;
        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _audioSettingsButton;

        [SerializeField]
        private LocalizationText _winPopupText;
        [SerializeField]
        private LocalizationText _losePopupText;

        private void Start() {
            _playButton.onClick.AddListener(LoadGameScreen);
            _audioSettingsButton.onClick.AddListener(ShowAudioSettings);
            ChangeStage();
        }

        private void ShowAudioSettings() {
            PopupManager.Instance.ShowAudioSettingsPopup();
        }

        public void ChangeStage() {
            if (!Account.Instance.GameStarted) {
                return;
            }
            if (Account.Instance.TotalDays - Account.Instance.CurrentDay > 0) {
                Account.Instance.TryToSwitchStage();
                return;
            }
            var targetMoneyPercent = Account.Instance.CurrentStageMoney * 100 / Account.Instance.RequiredMoney;
            if (Account.Instance.TryToSwitchStage()) {
                PopupManager.Instance.ShowClosableTextPopup(new ClosableTextPopup.Data {
                    text = string.Format(_winPopupText.GetText(), targetMoneyPercent, Account.Instance.RequiredMoney, Account.Instance.TotalDays),
                });
            } else {
                PopupManager.Instance.ShowClosableTextPopup(new ClosableTextPopup.Data {
                    text = string.Format(_losePopupText.GetText(), targetMoneyPercent, Account.Instance.RequiredMoney, Account.Instance.TotalDays),
                });
            }
        }

        private void LoadGameScreen() {
            Account.Instance.StartGame();
        }

        void Update() {
            _stageText.text = $"”ровень:{Account.Instance.CurrentStage}";
            _dayText.text = $"ƒень:{Account.Instance.CurrentDay} из {Account.Instance.TotalDays}";
            _moneyText.text = $"{Account.Instance.TotalMoney}$";
            _quotaText.text = $" вота {Account.Instance.CurrentStageMoney}/{Account.Instance.RequiredMoney}$";
        }
    }
}

