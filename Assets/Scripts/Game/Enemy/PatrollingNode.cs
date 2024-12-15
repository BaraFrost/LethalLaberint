using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class PatrollingNode : EnemyNode<Enemy> {

        private Vector3 _targetPosition;

        public PatrollingNode(Enemy enemy) : base(enemy) {
            UpdateTargetCell();
        }

        private void UpdateTargetCell() {
            _targetPosition = _enemy.EntitiesContainer.cellsContainer.GetRandomCellPosition();
        }

        public override NodeState Evaluate() {
            if (_enemy.EntitiesContainer.additionalEnemyTarget != null &&
                _enemy.MovementLogic.PositionAvailable(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position) &&
                !_enemy.MovementLogic.PositionReached(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position)) {
                _enemy.MovementLogic.WalkToPosition(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position);
                return NodeState.Success;
            }
            if (_enemy.MovementLogic.PositionReached(_targetPosition)) {
                UpdateTargetCell();
            }
            _enemy.MovementLogic.WalkToPosition(_targetPosition);
            return NodeState.Running;
        }
    }
}
