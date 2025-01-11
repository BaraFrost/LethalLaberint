using System;
using System.Collections;
using UnityEngine;

namespace Game {

    public class TurretAttackLogic : AbstractEnemyAttackLogic {

        [SerializeField]
        private float _timeBeforeAttack;

        [SerializeField]
        private float _timeBeforeDoDamage;

        [SerializeField]
        private int _missProbability;

        [SerializeField]
        private ParticleSystem _shootEffect;

        [SerializeField]
        private TurretVisualLogic _turretVisualLogic;

        private Coroutine _coroutine;

        public Action onTargetFound;
        public Action onTargetLost;
        public Action onShoot;

        private bool _isAttack = false;
        public override bool IsAttacking => _isAttack;

        public override void AttackTarget(PlayerController target) {
            if (_isAttack) {
                return;
            }
            _coroutine = StartCoroutine(AttackCoroutine(target));
        }

        private IEnumerator AttackCoroutine(PlayerController target) {
            _isAttack = true;
            _turretVisualLogic.PlayPrepareShootEffects();
            onTargetFound?.Invoke();
            yield return new WaitForSeconds(_timeBeforeAttack);
            onShoot?.Invoke();
            _turretVisualLogic.PlayShootEffects();
            yield return new WaitForSeconds(_timeBeforeDoDamage);
            while (true) {
                yield return null;
                if(TryToAttack(target)) {
                    break;
                }
            }
            _isAttack = false;
            _turretVisualLogic.StopShootEffects();
        }

        private bool TryToAttack(PlayerController target) {
            var shoot = UnityEngine.Random.Range(0,_missProbability) == 0;
            OnAttack?.Invoke();
            if (!shoot) {
                return false;
            }
            target.HealthLogic.AddDamage(Enemy.Type);
            return true;
        }

        public override bool CanAttackTarget(PlayerController target) {
            return true;
        }

        public override void StopAttack() {
            if(_coroutine == null || !_isAttack) {
                return;
            }
            OnAttackStopped?.Invoke();
            _isAttack = false;
            onTargetLost?.Invoke();
            _turretVisualLogic.StopShootEffects();
            StopCoroutine(_coroutine);
        }
    }
}
