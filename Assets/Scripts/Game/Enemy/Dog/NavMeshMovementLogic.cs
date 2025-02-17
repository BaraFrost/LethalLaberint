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

        private Vector3 _currentTargetPosition;
        private NavMeshPath _cachedPath;

        public override bool IsMoving => !_agent.isStopped;

        public override void Init(Enemy enemy) {
            base.Init(enemy);
            _agent.isStopped = true;
        }

        private bool TryCalculatePath(Vector3 position, out NavMeshPath path) {
            path = new NavMeshPath();
            _agent.CalculatePath(position, path);
            return path.status == NavMeshPathStatus.PathComplete;
        }

        protected override void MoveToPosition(Vector3 position, float speed) {
            if ((position == _currentTargetPosition) || !PositionAvailable(position)) {
                return;
            }

            _agent.speed = speed;

            if (TryCalculatePath(position, out var path)) {
                _agent.isStopped = false;
                _agent.SetPath(path);
                _currentTargetPosition = position;
            }
        }

        public override bool PositionAvailable(Vector3 position) {
            if (!TryCalculatePath(position, out var path)
                || path.corners.Length == 0
                || Enemy.EntitiesContainer.cellsContainer.StartCells.ShipLogic.PositionInsideShip(position)) {
                return false;
            }

            return true;
        }

        public override bool PositionReached(Vector3 position) {
            var distance = transform.position - position;
            distance.y = 0;
            return distance.magnitude <= _stopDistance;
        }

        public override void Rotate(Vector3 lookAtPosition) {
            Vector3 direction = (lookAtPosition - transform.position).normalized;
            direction.y = 0;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);
        }

        public override void Stop() {
            _agent.isStopped = true;
            _currentTargetPosition = transform.position;
        }
    }
}
