using Scripts.Infrastructure.BehaviorTree;

namespace Game {
    public class ItemsTrackingNode : EnemyNode<BugEnemy> {

        public ItemsTrackingNode(BugEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.EnemyItemsCollectionLogic.TryToFindItem(_enemy.Data.CollectibleItems, out var items)) {
                return NodeState.Failure;
            }
            if(!_enemy.MovementLogic.PositionReached(items.transform.position)) {
                _enemy.MovementLogic.MoveToPosition(items.transform.position);
                return NodeState.Success;
            }
            if (!_enemy.VisionLogic.CanSeeTarget(_enemy.Data.Player)) {
                _enemy.MovementLogic.Rotate(_enemy.Data.Player.transform.position);
                return NodeState.Success;
            }
            _enemy.EnemyItemsCollectionLogic.CollectItem(items);
            return NodeState.Success;
        }
    }
}