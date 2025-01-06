using UnityEngine;

namespace Game {

    public class SkeletonVisualLogic : AbstractVisualLogic<SkeletonEnemy> {

        [SerializeField]
        private string _hideTriggerName = "Hide";
        [SerializeField]
        private string _standUpTriggerName = "StandUp";

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

        public void Hide() {
            if (string.IsNullOrEmpty(_hideTriggerName)) {
                return;
            }
            Animator.ResetTrigger(_standUpTriggerName);
            Animator.SetTrigger(_hideTriggerName);
        }

        public void StandUp() {
            if (string.IsNullOrEmpty(_standUpTriggerName)) {
                return;
            }
            Animator.ResetTrigger(_hideTriggerName);
            Animator.SetTrigger(_standUpTriggerName);
        }


        protected override void PlayAttackVisual() {
            if(Enemy.SkeletonHideLogic.IsHided || Enemy.SkeletonHideLogic.IsStanding()) {
                return;
            }
            base.PlayAttackVisual();
            if (string.IsNullOrEmpty(_attackTriggerName)) {
                return;
            }
            _animator.SetTrigger(_attackTriggerName);
        }

        protected override void PlayStopAttackVisual() {
            if (Enemy.SkeletonHideLogic.IsHided || Enemy.SkeletonHideLogic.IsStanding()) {
                return;
            }
            base.PlayStopAttackVisual();
            if (string.IsNullOrEmpty(_stopAttackTriggerName)) {
                return;
            }
            _animator.SetTrigger(_stopAttackTriggerName);
        }

        protected override void PlayMoveVisual() {
            if (Enemy.SkeletonHideLogic.IsHided || Enemy.SkeletonHideLogic.IsStanding()) {
                return;
            }
            base.PlayMoveVisual();
            if (string.IsNullOrEmpty(_moveTriggerName)) {
                return;
            }
            _animator.SetTrigger(_moveTriggerName);
        }

        protected override void PlayStopVisual() {
            if (Enemy.SkeletonHideLogic.IsHided || Enemy.SkeletonHideLogic.IsStanding()) {
                return;
            }
            base.PlayStopVisual();
            if (string.IsNullOrEmpty(_stopTriggerName)) {
                return;
            }
            _animator.SetTrigger(_stopTriggerName);
        }
    }
}

