using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class PatrollingNode : EnemyNode<Enemy> {

        private Vector3 _targetPosition;

        public PatrollingNode(Enemy enemy) : base(enemy) {
            UpdateTargetCell();
        }

        private void UpdateTargetCell() {
            _targetPosition = _enemy.Data.CellContainer.GetRandomCellPosition();
        }

        public override NodeState Evaluate() {
            if(_enemy.MovementLogic.PositionReached(_targetPosition)) {
                UpdateTargetCell();
            }
            _enemy.MovementLogic.MoveToPosition(_targetPosition);
            return NodeState.Running;
        }
    }
}
