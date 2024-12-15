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
            _targetPosition = _enemy.EntitiesContainer.cellsContainer.GetRandomCellPosition();
        }

        public override NodeState Evaluate() {
            if (_enemy.EntitiesContainer.additionalEnemyTarget != null && 
                _enemy.MovementLogic.PositionAvailable(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position) && 
                !_enemy.MovementLogic.PositionReached(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position)) {
                _enemy.MovementLogic.WalkToPosition(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position);
                return NodeState.Success;
            }
            if (_enemy.SpawnAdditionalEnemyLogic.IsChildEnemyAttack(out var targetEnemy)) {
                _enemy.SpawnAdditionalEnemyLogic.StopSpawnEnemy();
                _targetPosition = _enemy.EntitiesContainer.playerController.transform.position;
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
                    _targetPosition = _enemy.EntitiesContainer.playerController.transform.position;
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

