using UnityEngine;
using UnityEngine.AI;

namespace Game {

    public class NavMeshMovementLogic : AbstractMovementLogic {

        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private float _stopDistance;

        public override void MoveToPosition(Vector3 position) {
            _agent.isStopped = false;
            _agent.SetDestination(position);
        }

        public override bool PositionReached(Vector3 position) {
            var distance = gameObject.transform.position - position;
            distance.y = 0;
            return distance.magnitude <= _stopDistance;
        }

        public override void Stop() {
            _agent.isStopped = true;
        }
    }
}
