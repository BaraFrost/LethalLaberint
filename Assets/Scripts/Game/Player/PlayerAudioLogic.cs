using UnityEngine;

namespace Game {

    public class PlayerAudioLogic : AbstractPlayerLogic {

        [SerializeField]
        private AudioSource _moveSound;

        public override void Init(PlayerController player) {
            base.Init(player);
            player.PlayerMoveLogic.onStartMove += StartMoveAnimation;
            player.PlayerMoveLogic.onStopMove += StopMoveAnimation;
        }

        private void StartMoveAnimation() {
            if (_moveSound == null || _moveSound.isPlaying) {
                return;
            }
            _moveSound.Play();
            Debug.Log("moveSound.Play()");
        }

        private void StopMoveAnimation() {
            if (_moveSound == null || !_moveSound.isPlaying) {
                return;
            }
            _moveSound.Stop();
            Debug.Log("moveSound.Stop()");
        }
    }
}
