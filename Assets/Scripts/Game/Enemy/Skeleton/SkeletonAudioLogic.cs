using UnityEngine;

namespace Game {

    public class SkeletonAudioLogic : AbstractEnemyAudioLogic<SkeletonEnemy> {

        [SerializeField]
        private AudioSource _onHideAudio;

        [SerializeField]
        private AudioSource _standUpAudio;

        [SerializeField]
        private AudioSource _hideSound;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if (Enemy.SkeletonHideLogic) {
                Enemy.SkeletonHideLogic.onHide += PlayOnHideSound;
                Enemy.SkeletonHideLogic.onStandUp += PlayStandUpSound;
            }
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            if(_hideSound == null) {
                return;
            }
            if (Enemy.SkeletonHideLogic.IsHided && !_hideSound.isPlaying) {
                _hideSound.Play();
            } else if (!Enemy.SkeletonHideLogic.IsHided && _hideSound.isPlaying) {
                _hideSound.Stop();
            }
        }

        private void PlayOnHideSound() {
            if(_onHideAudio == null || _onHideAudio.isPlaying) {
                return;
            }
            _onHideAudio.Play();
        }

        private void PlayStandUpSound() {
            if (_standUpAudio == null || _standUpAudio.isPlaying) {
                return;
            }
            _standUpAudio.Play();
        }
    }
}

