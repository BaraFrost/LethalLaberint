using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ItemsCollectionNode : EnemyNode<BugEnemy> {

        public ItemsCollectionNode(BugEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (_enemy.EnemyItemsCollectionLogic.HasItem || !_enemy.EnemyItemsCollectionLogic.TryToFindItem(_enemy.EntitiesContainer.collectibleItems, out var item) 
                || !_enemy.MovementLogic.PositionAvailable(item.transform.position)) {
                return NodeState.Failure;
            }
            if (_enemy.MovementLogic.PositionReached(item.transform.position)) {
                _enemy.EnemyItemsCollectionLogic.CollectItem(item);
                return NodeState.Success;
            }
            _enemy.MovementLogic.WalkToPosition(item.transform.position);
            return NodeState.Success;
        }
    }
}