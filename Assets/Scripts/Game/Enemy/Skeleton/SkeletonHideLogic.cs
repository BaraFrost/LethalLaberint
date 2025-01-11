using System;
using UnityEngine;

namespace Game {

    public class SkeletonHideLogic : AbstractEnemyLogic<SkeletonEnemy> {

        [SerializeField]
        private float _timeBeforeHide;
        private float _currentTimeBeforeHide;

        [SerializeField]
        private float _hideTime;
        private float _currentHideTime;

        [SerializeField]
        private float _standUpTime;
        private float _currentStandUpTime;

        private bool _isHided;
        public bool IsHided => _isHided;

        public Action onHide;
        public Action onStandUp;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            _currentTimeBeforeHide = _timeBeforeHide;
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            if (_currentTimeBeforeHide > 0) {
                _currentTimeBeforeHide -= Time.deltaTime;
            }
            if (_currentStandUpTime > 0) {
                _currentStandUpTime -= Time.deltaTime;
            }
            if (_currentHideTime > 0) {
                _currentHideTime -= Time.deltaTime;
            }
        }

        public void Hide() {
            if (!CanHide()) {
                return;
            }
            _isHided = true;
            onHide?.Invoke();
            Enemy.MovementLogic.Stop();
            _currentHideTime = _hideTime;
        }

        public void StandUp() {
            if(_currentHideTime > 0) {
                return;
            }
            _isHided = false;
            onStandUp?.Invoke();
            _currentStandUpTime = _standUpTime;
            _currentTimeBeforeHide = _timeBeforeHide + _standUpTime;
            Enemy.MovementLogic.Stop();
        }

        public bool CanHide() {
            return !_isHided && _currentTimeBeforeHide <= 0;
        }

        public bool IsHiding() {
            return _currentHideTime > 0;
        }

        public bool IsStanding() {
            return _currentStandUpTime > 0;
        }
    }
}