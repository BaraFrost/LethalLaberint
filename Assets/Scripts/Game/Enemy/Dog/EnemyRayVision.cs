using UnityEngine;

namespace Game {

    public class EnemyRayVision : AbstractEnemyVisionLogic {

        [SerializeField]
        private float _distance;

        public override bool CanSeeTarget(PlayerController enemyTarget) {
            var distanceVector = gameObject.transform.position - enemyTarget.transform.position;
            distanceVector.y = 0;
            return distanceVector.magnitude <= _distance;
        }
    }
}