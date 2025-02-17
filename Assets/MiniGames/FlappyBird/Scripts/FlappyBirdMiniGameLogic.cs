using Data;
using Game;
using Infrastructure;
using UI;
using UnityEngine;
using YG;

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
            YG2.MetricaSend("mini_game_started");
            PopupManager.Instance.ShowTextPopup(new TextPopup.Data {
                time = _time,
                type = TextPopup.Type.MiddleBig,
                text = _startTimerText.GetText(),
            });
            _player.onPlayerDead += OnGameEnd;
        }

        private void Init() {
            _isInitialized = true;
        }

        private void Update() {
            if (!_isInitialized && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) {
                Init();
                PopupManager.Instance.CloseCurrentPopup(immediately: false);
            }
            if (!_isInitialized) {
                return;
            }
            _player.UpdateLogic();
            _obstacleSpawner.UpdateLogic();
        }

        private void OnGameEnd() {
            PopupManager.Instance.ShowTextPopup(new TextPopup.Data() {
                text = string.Format(_endGameText.GetText(), _player.Wallet.Value),
                type = TextPopup.Type.Upper,
                onPopupShowedCallback = () => {
                    Account.Instance.HandleMiniGameEnd(_player.Wallet.Value);
                    ScenesSwitchManager.Instance.LoadMenuScene();
                }
            });
        }
    }
}

