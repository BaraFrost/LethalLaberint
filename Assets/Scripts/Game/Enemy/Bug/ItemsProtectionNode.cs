using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ItemsProtectionNode : EnemyNode<BugEnemy> {

        public ItemsProtectionNode(BugEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.EnemyItemsCollectionLogic.MaxItemsCollected) {
                return NodeState.Failure;
            }
            if (!_enemy.MovementLogic.PositionReached(_enemy.EnemyItemsCollectionLogic.StartPosition)) {
                _enemy.MovementLogic.MoveToPosition(_enemy.EnemyItemsCollectionLogic.StartPosition);
                return NodeState.Success;
            }
            _enemy.MovementLogic.Rotate(_enemy.Data.Player.transform.position);
            return NodeState.Success;
        }
    }
}

