using System.IO;
using UnityEngine;
using UnityEngine.AI;

namespace Game {

    public class NavMeshMovementLogic : AbstractMovementLogic {

        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private float _stopDistance;

        private bool _canReachTarget = false;

        private NavMeshPath _navMeshPath;

        private Vector3 _currentTargetPosition;

        public override void MoveToPosition(Vector3 position) {
            _agent.isStopped = false;
            _navMeshPath = new NavMeshPath();
            _agent.CalculatePath(position, _navMeshPath);
            if (_navMeshPath.status != NavMeshPathStatus.PathComplete) {
                return;
            }
            if (_currentTargetPosition != position) {
                _agent.SetPath(_navMeshPath);
                _currentTargetPosition = position;
            }
        }

        public override bool PositionReached(Vector3 position) {
            if(_navMeshPath == null || _navMeshPath.corners.Length == 0 || _navMeshPath.status != NavMeshPathStatus.PathComplete) {
                return true;
            }
            var distance = transform.position - _navMeshPath.corners[^1];
            distance.y = 0;
            return distance.magnitude <= _stopDistance;
        }

        public override void Rotate(Vector3 lookAtPosition) { }

        public override void Stop() {
            _agent.isStopped = true;
        }
    }
}
