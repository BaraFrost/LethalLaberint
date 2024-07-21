using System.Collections;
using UnityEngine;

namespace Game {

    public class TurretAttackLogic : AbstractEnemyAttackLogic {

        [SerializeField]
        private float _timeBeforeAttack;

        [SerializeField]
        private int _missProbability;

        [SerializeField]
        private ParticleSystem _shootEffect;

        private Coroutine _coroutine;

        private bool _isAttack = false;

        public override void AttackTarget(PlayerController target) {
            if (_isAttack) {
                return;
            }
            _coroutine = StartCoroutine(AttackCoroutine(target));
        }

        private IEnumerator AttackCoroutine(PlayerController target) {
            _isAttack = true;
            yield return new WaitForSeconds(_timeBeforeAttack);
            _shootEffect.Play();
            while (true) {
                yield return null;
                if(TryToAttack(target)) {
                    break;
                }
            }
            _isAttack = false;
            _shootEffect.Stop();
        }

        private bool TryToAttack(PlayerController target) {
            var shoot = Random.Range(0,_missProbability) == 0;
            if(!shoot) {
                return false;
            }
            target.HealthLogic.AddDamage();
            return true;
        }

        public override bool CanAttackTarget(PlayerController target) {
            return true;
        }

        public override void StopAttack() {
            if(_coroutine == null || !_isAttack) {
                return;
            }
            _isAttack = false;
            _shootEffect.Stop();
            StopCoroutine(_coroutine);
        }
    }
}
