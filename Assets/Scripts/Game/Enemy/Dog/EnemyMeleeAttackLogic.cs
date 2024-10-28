using System.Collections;
using UnityEngine;

namespace Game {

    public class EnemyMeleeAttackLogic : AbstractEnemyAttackLogic {

        [SerializeField]
        private float _attackDistance;

        [SerializeField]
        private float _timeBeforeDamage;

        private bool _isAttacking;
        public override bool IsAttacking => _isAttacking;

        public override void AttackTarget(PlayerController target) {
            if (_isAttacking) {
                return;
            }
            OnAttack?.Invoke();
            StartCoroutine(DoDamageCoroutine(target));
        }

        private IEnumerator DoDamageCoroutine(PlayerController target) {
            _isAttacking = true;
            yield return new WaitForSeconds(_timeBeforeDamage);
            target.HealthLogic.AddDamage();
            _isAttacking = false;
        }

        public override bool CanAttackTarget(PlayerController target) {
            return Vector3.Distance(target.gameObject.transform.position, gameObject.transform.position) < _attackDistance;
        }

        void OnDrawGizmosSelected() {
            var alpha = 0.5f;
            var color = Color.yellow;
            color.a = alpha;
            Gizmos.color = color;
            Gizmos.DrawSphere(transform.position, _attackDistance);
        }

        public override void StopAttack() {
        }
    }
}

