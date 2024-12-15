using System.Collections;
using UnityEngine;

namespace Game {

    public class ZoneAttackLogic : AbstractEnemyAttackLogic {

        [SerializeField]
        private float _timeBeforeExplosion;

        [SerializeField]
        private float _reloadTime;

        [SerializeField]
        private float _activateDistance;

        [SerializeField]
        private bool _hasDamage = true;

        [SerializeField]
        private float _damageDistance;
        protected float DamageDistance => _damageDistance;

        [SerializeField]
        private LayerMask _overlapEnemyLayerMask;

        private Coroutine _coroutine;

        private bool _isAttacking;
        public override bool IsAttacking => _isAttacking;

        public override void AttackTarget(PlayerController target) {
            if (_isAttacking) {
                return;
            }
            _coroutine = StartCoroutine(AttackCoroutine(target));
        }

        public virtual bool CanSeeTarget(Collider targetCollider) {
            var directionVector = targetCollider.bounds.center - gameObject.transform.position;
            directionVector.y = 0;
            return directionVector.magnitude < _damageDistance;
        }

        private IEnumerator AttackCoroutine(PlayerController target) {
            _isAttacking = true;
            yield return new WaitForSeconds(_timeBeforeExplosion);
            if (_hasDamage && CanSeeTarget(target.Collider)) {
                target.HealthLogic.AddDamage();
            }
            var targets = Physics.OverlapSphere(gameObject.transform.position, _damageDistance, _overlapEnemyLayerMask);
            foreach (var enemyTarget in targets) {
                if (!_hasDamage || !enemyTarget.TryGetComponent<Enemy>(out var enemy) || enemy.HealthLogic == null || !CanSeeTarget(enemyTarget)) {
                    continue;
                }
                enemy.HealthLogic.AddDamage();
            }
            OnAttack?.Invoke();
            yield return new WaitForSeconds(_reloadTime);
            OnAttackStopped?.Invoke();
            _isAttacking = false;
        }

        public override bool CanAttackTarget(PlayerController target) {
            if (target.HealthLogic.IsDamaged) {
                return false;
            }
            return true;
        }

        private void OnDrawGizmosSelected() {
            var alpha = 0.5f;
            var color = Color.yellow;
            color.a = alpha;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, _activateDistance);
            color = Color.red;
            color.a = alpha;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, _damageDistance);
        }

        public override void StopAttack() {
            if (!_isAttacking || _coroutine != null) {
                return;
            }
            OnAttackStopped?.Invoke();
            _isAttacking = false;
        }
    }
}

