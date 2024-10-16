using System.Collections;
using UnityEngine;

namespace Game {

    public class MineAttackLogic : AbstractEnemyAttackLogic {

        [SerializeField]
        private float _timeBeforeExplosion;

        [SerializeField]
        private float _activateDistance;

        [SerializeField]
        private float _damageDistance;

        [SerializeField]
        private LayerMask _raycastLayerMask;

        [SerializeField]
        private MineVisualLogic _visualLogic;

        private Coroutine _coroutine;

        private bool _isAttacking;

        public override void AttackTarget(PlayerController target) {
            _coroutine = StartCoroutine(AttackCoroutine(target));
        }

        public bool CanSeeTarget(PlayerController enemyTarget) {
            var directionVector = enemyTarget.Collider.bounds.center - gameObject.transform.position;
            return Physics.Raycast(gameObject.transform.position, directionVector, out var hitInfo, _damageDistance, _raycastLayerMask)
                && hitInfo.collider != null
                && hitInfo.collider.TryGetComponent<PlayerController>(out var player);
        }

        private IEnumerator AttackCoroutine(PlayerController target) {
            _isAttacking = true;
            yield return new WaitForSeconds(_timeBeforeExplosion);
            if (CanSeeTarget(target)) {
                target.HealthLogic.AddDamage();
            }
            _visualLogic.PlayExplosionEffect();
            Destroy(gameObject);
            _isAttacking = false;
        }

        public override bool CanAttackTarget(PlayerController target) {
            return Vector3.Distance(target.transform.position, gameObject.transform.position) <= _activateDistance;
        }

        void OnDrawGizmosSelected() {
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
            _isAttacking = false;
        }
    }
}
