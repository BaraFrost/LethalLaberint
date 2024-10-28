using UnityEngine;

namespace Game {

    public class AbstractEnemyAudioLogic<T> : AbstractEnemyLogic<T> where T : Enemy {

        [SerializeField]
        private AudioSource _moveSound;
        [SerializeField]
        private AudioSource _attackSound;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            if(Enemy.AttackLogic != null && _attackSound != null) {
                Enemy.AttackLogic.OnAttack += PlayAttackSound;
                Enemy.AttackLogic.OnAttack += PlayStopSound;
            }
        }

        public override void UpdateLogic() {
            base.UpdateLogic();
            PlayMoveSound();
        }

        private void PlayMoveSound() {
            if(_moveSound == null || Enemy.MovementLogic == null) {
                return;
            }
            if(Enemy.MovementLogic.IsMoving) {
                _moveSound.Play();
                Debug.Log("MoveSound");
                return;
            }
            _moveSound.Stop();
            return;
        }

        private void PlayAttackSound() {
            if (_attackSound.isPlaying) {
                return;
            }
            _attackSound.Play();
            Debug.Log("AttackSound");
            return;
        }

        private void PlayStopSound() {
            if (!_attackSound.isPlaying) {
                return;
            }
            _attackSound.Stop();
            Debug.Log("StopAttackSound");
            return;
        }
    }
}

