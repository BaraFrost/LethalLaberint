using Data;
using Game;
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace UI {

    public class HintManager : MonoBehaviour {

        [Serializable]
        private struct HintByEnemyType {
            public Enemy.EnemyType enemyType;
            public LocalizationText hint;
        }

        [SerializeField]
        private LocalizationText _startHintText;

        [SerializeField]
        private float _startHintTime;

        [SerializeField]
        private List<LocalizationText> _deathHintsHintText;

        [SerializeField]
        private List<HintByEnemyType> _hintByEnemyTypes;

        [SerializeField]
        private float _deathHintTime;

        private PlayerController _playerController;

        public void Init(PlayerController playerController) {
            playerController.HealthLogic.onDamaged += ShowDeathHint;
            _playerController = playerController;
            ShowStartHint();
        }

        public void ShowStartHint() {
            if (_startHintText == null || TutorialLogic.Instance != null) {
                return;
            }
            var text = string.Format(_startHintText.GetText());
            if (PopupManager.Instance != null) {
                PopupManager.Instance.ShowTextPopup(new TextPopup.Data {
                    text = text,
                    type = TextPopup.Type.Upper,
                    time = _startHintTime,
                }
                );
            }
        }

        public void ShowDeathHint(Enemy.EnemyType enemyType) {
            if(_playerController.HealthLogic.IsDead || _deathHintsHintText.Count == 0) {
                return;
            }
            var text = GetDeathHintText(enemyType);
            if (PopupManager.Instance != null) {
                PopupManager.Instance.ShowTextPopup(new TextPopup.Data {
                    text = text.GetText(),
                    type = TextPopup.Type.Middle,
                    time = _deathHintTime,
                }
                );
            }
        }

        private LocalizationText GetDeathHintText(Enemy.EnemyType enemyType) {
            var hintsByEnemyType = _hintByEnemyTypes.Where(hint => hint.enemyType == enemyType).ToArray();
            if(hintsByEnemyType.Length > 0) {
                var randomHint = hintsByEnemyType[UnityEngine.Random.Range(0, hintsByEnemyType.Length)];
                _hintByEnemyTypes.Remove(randomHint);
                return randomHint.hint;
            }
            var randomText = _deathHintsHintText[UnityEngine.Random.Range(0, _deathHintsHintText.Count)];
            _deathHintsHintText.Remove(randomText);
            return randomText;
        }
    }
}