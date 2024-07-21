using UnityEngine;

namespace Game {

    public class EnemyMeleeAttackLogic : AbstractEnemyAttackLogic {

        [SerializeField]
        private float _attackDistance;

        public override void AttackTarget(PlayerController target) {
            target.HealthLogic.AddDamage();
        }

        public override bool CanAttackTarget(PlayerController target) {
            return Vector3.Distance(target.gameObject.transform.position, gameObject.transform.position) < _attackDistance;
        }

        public override void StopAttack() {
        }
    }
}

