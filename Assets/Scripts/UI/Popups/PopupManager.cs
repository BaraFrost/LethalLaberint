using Infrastructure;
using System;
using UnityEngine;

namespace UI {

    public class PopupManager : SingletonCrossScene<PopupManager> {

        [SerializeField]
        private TextPopup _textPopupPrefab;
        private TextPopup _textPopup;

        [SerializeField]
        private DeathPopup _deathPopupPrefab;
        private DeathPopup _deathPopup;

        [SerializeField]
        private FadePopup _fadePopupPrefab;
        private FadePopup _fadePopup;

        [SerializeField]
        private WinPopup _winPopupPrefab;
        private WinPopup _winPopup;

        [SerializeField]
        private TimerPopup _timerPopupPrefab;
        private TimerPopup _timerPopup;

        private AbstractPopup _currentPopup;

        private void ShowPopup(AbstractPopup popup) {
            if (_currentPopup != null && _currentPopup.IsActive) {
                _currentPopup.HidePopup(immediately: true);
            }
            _currentPopup = popup;
            popup.ShowPopup();
        }

        public void ShowTextPopup(TextPopup.Data data) {
            if (_textPopup == null) {
                _textPopup = Instantiate(_textPopupPrefab, gameObject.transform);
            }
            _textPopup.SetData(data);
            ShowPopup(_textPopup);
        }

        public void ShowDeathPopup(DeathPopup.Data data) {
            if (_deathPopup == null) {
                _deathPopup = Instantiate(_deathPopupPrefab, gameObject.transform);
            }
            _deathPopup.SetData(data);
            ShowPopup(_deathPopup);
        }

        public void ShowFadePopup(Action onShowedCallback) {
            if (_fadePopup == null) {
                _fadePopup = Instantiate(_fadePopupPrefab, gameObject.transform);
            }
            _fadePopup.SetData(onShowedCallback);
            ShowPopup(_fadePopup);
        }

        public void ShowWinPopup(Action continueCallback, int money) {
            if (_winPopup == null) {
                _winPopup = Instantiate(_winPopupPrefab, gameObject.transform);
            }
            _winPopup.SetData(continueCallback, money);
            ShowPopup(_winPopup);
        }

        public void ShowTimerPopup(TimerPopup.Data data) {
            if (_timerPopup == null) {
                _timerPopup = Instantiate(_timerPopupPrefab, gameObject.transform);
            }
            _timerPopup.SetData(data);
            ShowPopup(_timerPopup);
        }
    }
}
