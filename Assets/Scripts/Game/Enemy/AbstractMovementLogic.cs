using UnityEngine;

namespace Game {

    public abstract class AbstractMovementLogic : AbstractEnemyLogic<Enemy> {

        public abstract bool IsMoving { get; }
        public abstract void MoveToPosition(Vector3 position);
        public abstract void Stop();
        public abstract bool PositionReached(Vector3 position);
        public abstract bool PositionAvailable(Vector3 position);
        public abstract void Rotate(Vector3 lookAtPosition);
    }
}
