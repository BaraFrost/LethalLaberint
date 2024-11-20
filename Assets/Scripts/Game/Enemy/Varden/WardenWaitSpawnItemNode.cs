using Scripts.Infrastructure.BehaviorTree;

namespace Game {
    public class WardenWaitSpawnItemNode : EnemyNode<WardenEnemy> {

        public WardenWaitSpawnItemNode(WardenEnemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if (_enemy.SpawnAdditionalEnemyLogic.IsChildEnemyAttack(out var targetEnemy)) {
                _enemy.SpawnAdditionalEnemyLogic.StopSpawnEnemy();
                return NodeState.Failure;
            }
            if (_enemy.SpawnAdditionalEnemyLogic.IsSpawning) {
                return NodeState.Success;
            }
            return NodeState.Failure;
        }
    }
}

