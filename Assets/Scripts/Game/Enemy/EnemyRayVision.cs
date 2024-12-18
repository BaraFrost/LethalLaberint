using UnityEngine;

namespace Game {

    public class EnemyRayVision : AbstractEnemyVisionLogic {

        [SerializeField]
        private float _distance;
        public override float Distance => _distance;

        [SerializeField]
        private float _visionAngle;

        [SerializeField]
        private Transform _visionPosition;

        [SerializeField]
        private LayerMask _raycastLayerMask;

        [SerializeField]
        private float _sphereCastRadius;

        protected override bool CanSeeTargetInternal(PlayerController enemyTarget) {
            var directionVector = enemyTarget.Collider.bounds.center - _visionPosition.transform.position;
            var directionForAngleCalculation = new Vector3(directionVector.x, 0, directionVector.z);
            var angleBetween = Vector3.Angle(_visionPosition.transform.forward, directionForAngleCalculation);
            if (directionForAngleCalculation.magnitude > _distance
                || angleBetween > _visionAngle) {
                return false;
            }
            Debug.DrawRay(_visionPosition.transform.position, directionVector);
            if (Physics.SphereCast(_visionPosition.transform.position, _sphereCastRadius, directionVector, out var hitInfo, _distance, _raycastLayerMask) && hitInfo.collider != null
                && hitInfo.collider.gameObject == enemyTarget.gameObject) {
                return true;
            }
            return false;
        }
    }
}