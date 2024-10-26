using System.IO;
using UnityEngine;
using UnityEngine.AI;

namespace Game {

    public class NavMeshMovementLogic : AbstractMovementLogic {

        [SerializeField]
        private NavMeshAgent _agent;
        [SerializeField]
        private float _stopDistance;

        [SerializeField]
        private float _rotationSpeed = 10f;

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

        public override bool PositionAvailable(Vector3 position) {
            if (position != _currentTargetPosition) {
                _navMeshPath = new NavMeshPath();
                _agent.CalculatePath(position, _navMeshPath);
            }
            if (_navMeshPath == null || _navMeshPath.corners.Length == 0 || _navMeshPath.status != NavMeshPathStatus.PathComplete) {
                return false;
            }
            return true;
        }

        public override bool PositionReached(Vector3 position) {
            if (!PositionAvailable(position)) {
                return true;
            }
            var distance = transform.position - position;
            distance.y = 0;
            return distance.magnitude <= _stopDistance;
        }

        public override void Rotate(Vector3 lookAtPosition) {
            var direction = Vector3.RotateTowards(transform.forward, -transform.position + lookAtPosition, _rotationSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
            /*lookAtPosition.y = transform.position.y;
            _agent.transform.LookAt(lookAtPosition);*/
        }


        public override void Stop() {
            _agent.isStopped = true;
        }
    }
}
