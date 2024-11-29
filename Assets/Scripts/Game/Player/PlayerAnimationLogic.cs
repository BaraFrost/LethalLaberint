using UnityEngine;

namespace Game {

    [RequireComponent(typeof(Animator))]
    public class PlayerAnimationLogic : AbstractPlayerLogic {

        private Animator _animator;

        public override void Init(PlayerController player) {
            base.Init(player);
            _animator = GetComponent<Animator>();
            player.PlayerMoveLogic.onStartMove += StartMoveAnimation;
            player.PlayerMoveLogic.onStopMove += StopMoveAnimation;
        }

        private void StartMoveAnimation() {
            _animator.SetTrigger("Move");
            _animator.ResetTrigger("Stop");
        }

        private void StopMoveAnimation() {
            _animator.ResetTrigger("Move");
            _animator.SetTrigger("Stop");
        }
    }
}
