using UnityEngine;

namespace Game {

    public class DefaultEnemyVisualLogic : AbstractVisualLogic<Enemy> {

        [SerializeField]
        private Animator _animator;
        public Animator Animator => _animator;

        [SerializeField]
        private string _attackTriggerName = "Attack";
        [SerializeField]
        private string _stopAttackTriggerName = "StopAttack";
        [SerializeField]
        private string _moveTriggerName = "Move";
        [SerializeField]
        private string _stopTriggerName = "Stop";

        protected override void PlayAttackVisual() {
            base.PlayAttackVisual();
            if(string.IsNullOrEmpty(_attackTriggerName)) {
                return;
            }
            _animator.SetTrigger(_attackTriggerName);
        }

        protected override void PlayStopAttackVisual() {
            base.PlayStopAttackVisual();
            if (string.IsNullOrEmpty(_stopAttackTriggerName)) {
                return;
            }
            _animator.SetTrigger(_stopAttackTriggerName);
        }

        protected override void PlayMoveVisual() {
            base.PlayMoveVisual();
            if (string.IsNullOrEmpty(_moveTriggerName)) {
                return;
            }
            _animator.SetTrigger(_moveTriggerName);
        }

        protected override void PlayStopVisual() {
            base.PlayStopVisual();
            if (string.IsNullOrEmpty(_stopTriggerName)) {
                return;
            }
            _animator.SetTrigger(_stopTriggerName);
        }
    }
}

