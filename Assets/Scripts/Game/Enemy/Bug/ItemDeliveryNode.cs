using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ItemDeliveryNode : EnemyNode<BugEnemy> {

        public ItemDeliveryNode(BugEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.EnemyItemsCollectionLogic.HasItem) {
                return NodeState.Failure;
            }
            if(_enemy.MovementLogic.PositionReached(_enemy.EnemyItemsCollectionLogic.StartPosition)) {
                _enemy.EnemyItemsCollectionLogic.DropCollectibleItem();
                return NodeState.Success;
            }
            _enemy.MovementLogic.WalkToPosition(_enemy.EnemyItemsCollectionLogic.StartPosition);
            return NodeState.Success;
        }
    }
}
