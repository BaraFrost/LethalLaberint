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
    }
}
