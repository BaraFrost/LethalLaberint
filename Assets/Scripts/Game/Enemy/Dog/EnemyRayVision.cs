using UnityEngine;

namespace Game {

    public class EnemyRayVision : AbstractEnemyVisionLogic {

        [SerializeField]
        private float _distance;

        [SerializeField]
        private float _visionAngle;

        [SerializeField]
        private Transform _visionPosition;

        [SerializeField]
        private LayerMask _raycastLayerMask;

        public override bool CanSeeTarget(PlayerController enemyTarget) {
            var directionVector = enemyTarget.transform.position - _visionPosition.transform.position;
            directionVector.y = 0;
            var angleBetween = Vector3.Angle(gameObject.transform.forward, directionVector);
            if (directionVector.magnitude > _distance
                || angleBetween > _visionAngle) {
                return false;
            }
            if (Physics.Raycast(_visionPosition.transform.position, directionVector, out var hitInfo, _distance, _raycastLayerMask) && hitInfo.collider != null
                && hitInfo.collider.TryGetComponent<PlayerController>(out var player)) {
                return true;
            }
            return false;
        }
    }
}