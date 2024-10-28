using UnityEngine;

namespace Game {

    public class MineAttackLogic : ZoneAttackLogic {

        [SerializeField]
        private LayerMask _raycastLayerMask;

        public override bool CanSeeTarget(Collider targetCollider) {
            var directionVector = targetCollider.bounds.center - gameObject.transform.position;
            return Physics.Raycast(gameObject.transform.position, directionVector, out var hitInfo, DamageDistance, _raycastLayerMask)
                && hitInfo.collider != null
                && hitInfo.collider == targetCollider;
        }
    }
}
