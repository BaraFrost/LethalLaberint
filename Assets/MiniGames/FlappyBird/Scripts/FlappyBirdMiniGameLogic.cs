using Data;
using Game;
using Infrastructure;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace MiniGames.FlappyBird {

    public class FlappyBirdMiniGameLogic : MonoBehaviour {

        [SerializeField]
        private LocalizationText _startTimerText;

        [SerializeField]
        private int _time;

        [SerializeField]
        private LocalizationText _endGameText;

        [SerializeField]
        private FlappyBirdPlayer _player;

        [SerializeField]
        private FlappyBirdObstacleSpawner _obstacleSpawner;

        private bool _isInitialized;

        private void Start() {
            PopupManager.Instance.ShowTimerPopup(new TimerPopup.Data {
                time = _time,
                additionalText = _startTimerText.GetText(),
                onTimerEnd = Init,
            });
            _player.onPlayerDead += OnGameEnd;
        }

        private void Init() {
            _isInitialized = true;
        }

        private void Update() {
            if (!_isInitialized) {
                return;
            }
            _player.UpdateLogic();
            _obstacleSpawner.UpdateLogic();
        }

        private void OnGameEnd() {
            PopupManager.Instance.ShowTextPopup(new TextPopup.Data() {
                text = string.Format(_endGameText.GetText(), _player.Wallet.Value),
                type = TextPopup.Type.middle,
                onPopupShowedCallback = () => {
                    Account.Instance.HandleMatchDoneEvent(new Account.MatchDoneEvent() {
                        EarnedMoney = Account.Instance.CurrentStageMoney + _player.Wallet.Value,
                    });
                    ScenesSwitchManager.Instance.LoadMenuScene();
                }
            });
        }
    }
}

