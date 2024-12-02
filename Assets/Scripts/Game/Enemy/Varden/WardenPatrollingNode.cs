using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class WardenPatrollingNode : EnemyNode<WardenEnemy> {

        private Vector3 _targetPosition;
        private bool _isAttacking;

        public WardenPatrollingNode(WardenEnemy enemy) : base(enemy) {
            _enemy.SpawnAdditionalEnemyLogic.onEnemySpawned += UpdateTargetCell;
            UpdateTargetCell();
        }

        private void UpdateTargetCell() {
            _targetPosition = _enemy.Data.CellContainer.GetRandomCellPosition();
        }

        public override NodeState Evaluate() {
            if (_enemy.SpawnAdditionalEnemyLogic.IsChildEnemyAttack(out var targetEnemy)) {
                _enemy.SpawnAdditionalEnemyLogic.StopSpawnEnemy();
                _targetPosition = _enemy.Data.Player.transform.position;
                _isAttacking = true;
            }

            if (_enemy.SpawnAdditionalEnemyLogic.IsSpawning) {
                return NodeState.Running;
            }

            if(!_enemy.MovementLogic.PositionAvailable(_targetPosition)) {
                UpdateTargetCell();
                _isAttacking = false;
                return NodeState.Running;
            }

            if (_enemy.MovementLogic.PositionReached(_targetPosition)) {
                if(_isAttacking) {
                    _isAttacking = false;
                    UpdateTargetCell();
                    return NodeState.Success;
                }
                _enemy.SpawnAdditionalEnemyLogic.SpawnEnemy(_targetPosition);
                return NodeState.Running;
            }
            if(_isAttacking) {
                _enemy.MovementLogic.RunToPosition(_targetPosition);
                return NodeState.Running;
            }
            _enemy.MovementLogic.WalkToPosition(_targetPosition);
            return NodeState.Running;
        }
    }
}

