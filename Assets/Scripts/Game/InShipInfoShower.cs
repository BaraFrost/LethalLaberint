using UnityEngine;

namespace Game {

    public class InShipInfoShower : MonoBehaviour {

        [SerializeField]
        private CanvasGroup _group;

        [SerializeField]
        private float _fadeTime;

        private Coroutine _fadeCoroutine;

        private StartLabyrinthCells _startLabyrinthCells;
        private PlayerController _playerController;
        public bool IsActive { get; private set; }
        private bool _isInited;
        private float _startAlpha;
        private float _elapsedTime;

        public void Init(SpawnedLabyrinthCellsContainer cellsContainer, PlayerController playerController) {
            _startLabyrinthCells = cellsContainer.StartCells;
            _playerController = playerController;
            _isInited = true;
        }

        private void Update() {
            if (!_isInited) {
                return;
            }
            if (_startLabyrinthCells.ShipLogic.PositionInsideShip(_playerController.transform.position)) {
                ShowInfo();
            } else {
                HideInfo();
            }
        }

        public virtual void ShowInfo() {
            if (!IsActive) {
                IsActive = true;
                _startAlpha = _group.alpha;
                _elapsedTime = 0;
            }
            Fade(true);
        }

        public virtual void HideInfo() {
            if (IsActive) {
                _startAlpha = _group.alpha;
                IsActive = false;
                _elapsedTime = 0;
                return;
            }
            Fade(false);
        }

        private void Fade(bool enable) {
            var targetAlpha = enable ? 1 : 0;

            if (_elapsedTime < _fadeTime) {
                _elapsedTime += Time.deltaTime;
                var t = _elapsedTime / _fadeTime;
                _group.alpha = Mathf.Lerp(_startAlpha, targetAlpha, t);
            } else {
                _group.alpha = targetAlpha;
            }
        }
    }
}

