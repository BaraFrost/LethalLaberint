using UnityEngine;

namespace Game {

    public abstract class AbstractMovementLogic : AbstractEnemyLogic<Enemy> {

        [SerializeField]
        private float _walkSpeed;

        [SerializeField]
        private float _runSpeed;

        public abstract bool IsMoving { get; }
        protected abstract void MoveToPosition(Vector3 position, float speed);
        public void WalkToPosition(Vector3 position) {
            MoveToPosition(position, _walkSpeed);
        }
        public void RunToPosition(Vector3 position) {
            MoveToPosition(position, _runSpeed);
        }
        public abstract void Stop();
        public abstract bool PositionReached(Vector3 position);
        public abstract bool PositionAvailable(Vector3 position);
        public abstract void Rotate(Vector3 lookAtPosition);
    }
}
