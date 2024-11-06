using UnityEngine;

namespace Game {

    public class EnemyDistanceVisionLogic : AbstractEnemyVisionLogic {

        [SerializeField]
        private float _distance;
        public override float Distance => _distance;

        protected override bool CanSeeTargetInternal(PlayerController enemyTarget) {
            return Vector3.Distance(enemyTarget.transform.position, gameObject.transform.position) <= _distance;
        }
    }
}
