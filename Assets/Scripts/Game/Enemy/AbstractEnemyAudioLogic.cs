using UnityEngine;

namespace Game {

    public class AbstractEnemyAudioLogic<T> : AbstractEnemyLogic<T> where T : Enemy {

        [SerializeField]
        private AudioSource _moveSound;
        [SerializeField]
        private AudioSource _attackSound;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if (Enemy.AttackLogic != null && _attackSound != null) {
                Enemy.AttackLogic.OnAttack += PlayAttackSound;
            }
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            PlayMoveSound();
        }

        private void PlayMoveSound() {
            if (_moveSound == null || Enemy.MovementLogic == null) {
                return;
            }
            if (Enemy.MovementLogic.IsMoving) {
                if (!_moveSound.isPlaying) {
                    _moveSound.Play();
                }
                return;
            }
            if (_moveSound.isPlaying) {
                _moveSound.Stop();
            }
            return;
        }

        private void PlayAttackSound() {
            if (_attackSound.isPlaying) {
                return;
            }
            _attackSound.Play();
            return;
        }
    }
}

