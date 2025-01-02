using Data;
using Game;
using Infrastructure;
using System.Collections.Generic;
using UnityEngine;

namespace UI {

    public class HintManager : MonoBehaviour {

        [SerializeField]
        private LocalizationText _startHintText;

        [SerializeField]
        private List<LocalizationText> _deathHintsHintText;

        private PlayerController _playerController;

        public void Init(PlayerController playerController) {
            playerController.HealthLogic.onDamaged += ShowDeathHint;
            _playerController = playerController;
            ShowStartHint();
        }

        public void ShowStartHint() {
            if (_startHintText == null) {
                return;
            }
            var text = string.Format(_startHintText.GetText(), Account.Instance.RequiredMoney, Account.Instance.CurrentStageMoney, Account.Instance.TotalDays - Account.Instance.CurrentDay);
            if (PopupManager.Instance != null) {
                PopupManager.Instance.ShowTextPopup(new TextPopup.Data {
                    text = text,
                    type = TextPopup.Type.middle,
                }
                );
            }
        }

        public void ShowDeathHint() {
            if(_playerController.HealthLogic.IsDead || _deathHintsHintText.Count == 0) {
                return;
            }
            var randomText = _deathHintsHintText[Random.Range(0, _deathHintsHintText.Count)];
            _deathHintsHintText.Remove(randomText);
            if (PopupManager.Instance != null) {
                PopupManager.Instance.ShowTextPopup(new TextPopup.Data {
                    text = randomText.GetText(),
                    type = TextPopup.Type.rightDown,
                }
                );
            }
        }
    }
}