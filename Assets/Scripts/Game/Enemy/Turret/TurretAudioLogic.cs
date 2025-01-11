using UnityEngine;

namespace Game {

    public class TurretAudioLogic : AbstractEnemyLogic<TurretEnemy> {

        [SerializeField]
        private AudioSource _aimSound;

        [SerializeField]
        private AudioSource _moveSound;

        [SerializeField]
        private AudioSource _shootSound;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if (_aimSound != null) {
                Enemy.TurretAttackLogic.onTargetFound += PlayAimSound;
                Enemy.TurretAttackLogic.onTargetLost += PlayStopShootSound;
            }
            if (_moveSound != null) {
                Enemy.TurretRotationLogic.onRotation += PlayMoveSound;
                Enemy.TurretRotationLogic.onStopRotation += StopMoveSound;
            }
            if (_shootSound != null) {
                Enemy.TurretAttackLogic.onShoot += PlayShootSound;
            }
        }

        private void PlayShootSound() {
            if (_shootSound.isPlaying) {
                return;
            }
            _shootSound.Play();
        }

        private void StopMoveSound() {
            if (!_moveSound.isPlaying) {
                return;
            }
            _moveSound.Stop();
        }

        private void PlayMoveSound() {
            if (_moveSound.isPlaying) {
                return;
            }
            _moveSound.Play();
        }

        private void PlayStopShootSound() {
            if (_shootSound.isPlaying) {
                _shootSound.Stop();
            }
            PlayAimSound();
        }

        private void PlayAimSound() {
            _aimSound.Play();
        }
    }
}