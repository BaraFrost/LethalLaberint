using UnityEngine;

namespace Game {

    public class EnemyDistanceVisualLogic : AbstractEnemyVisionLogic {

        [SerializeField]
        private float _distance;

        protected override bool CanSeeTargetInternal(PlayerController enemyTarget) {
            return Vector3.Distance(enemyTarget.transform.position, gameObject.transform.position) <= _distance;
        }
    }
}
