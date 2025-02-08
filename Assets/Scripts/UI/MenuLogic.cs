using Data;
using Game;
using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace UI {

    public class MenuLogic : MonoBehaviour {

        [SerializeField]
        private Button _playButton;

        [SerializeField]
        private Button _audioSettingsButton;

        [SerializeField]
        private Button _bookButton;

        [SerializeField]
        private LocalizationText _winPopupText;
        [SerializeField]
        private LocalizationText _losePopupText;

        [SerializeField]
        private PlanetsVisualSetter _planetsVisualSetter;

        [SerializeField]
        private MenuInfoSetter _menuInfoSetter;

        [SerializeField]
        private RequiredAbilityInfo _requiredAbilityInfo;

        private void Start() {
            if(!Account.Instance.GameStarted) {
                YG2.GameReadyAPI();
            }
            YG2.GameplayStop();
            _playButton.onClick.AddListener(LoadGameScreen);
            _audioSettingsButton.onClick.AddListener(ShowAudioSettings);
            _bookButton.onClick.AddListener(ShowBookPopup);
            _planetsVisualSetter.UpdatePlanet();
            ChangeStage();
            if (YG2.envir.isMobile) {
                QualitySettings.SetQualityLevel(1);
                Application.targetFrameRate = 30;
            } else {
                QualitySettings.SetQualityLevel(0);
            }
            //_requiredAbilityInfo.Init();
        }

        private void ShowBookPopup() {
            PopupManager.Instance.ShowBookPopup();
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
                TryToShowNewEnemyPopup();
                return;
            }
            var targetMoneyPercent = Account.Instance.CurrentStageMoney * 100 / Account.Instance.RequiredMoney;
            if (Account.Instance.TryToSwitchStage()) {
                PopupManager.Instance.ShowClosableTextPopup(new ClosableTextPopup.Data {
                    text = string.Format(_winPopupText.GetText(), targetMoneyPercent, Account.Instance.RequiredMoney),
                    onPopupClosed = TryToShowNewEnemyPopup,
                });
                _planetsVisualSetter.UpdatePlanet();
            } else {
                PopupManager.Instance.ShowClosableTextPopup(new ClosableTextPopup.Data {
                    text = string.Format(_losePopupText.GetText(), targetMoneyPercent, Account.Instance.RequiredMoney),
                    onPopupClosed = TryToShowNewEnemyPopup,
                });
            }
        }

        private void TryToShowNewEnemyPopup() {
            if (!Account.Instance.newEnemyOpened) {
                return;
            }
            Account.Instance.newEnemyOpened = false;
            PopupManager.Instance.ShowBookPopup(Account.Instance.newEnemyType);
        }

        private void LoadGameScreen() {
            if (YG2.isTimerAdvCompleted && !YG2.nowAdsShow) {
                YG2.InterstitialAdvShow();
            }
            
            Account.Instance.StartGame();
        }

        void Update() {
            _menuInfoSetter.UpdateInfo();
        }
    }
}

