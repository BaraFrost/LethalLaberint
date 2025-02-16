using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ItemDeliveryNode : EnemyNode<HelperEnemy> {

        public ItemDeliveryNode(HelperEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.EnemyItemsCollectionLogic.HasItem) {
                return NodeState.Failure;
            }
            if(_enemy.MovementLogic.PositionReached(_enemy.EnemyItemsCollectionLogic.StartPosition)) {
                if (!_enemy.VisionLogic.CanSeeTarget(_enemy.EntitiesContainer.playerController)) {
                    _enemy.MovementLogic.Rotate(_enemy.EntitiesContainer.playerController.transform.position);
                    _enemy.MovementLogic.Stop();
                    return NodeState.Success;
                }
                _enemy.EnemyItemsCollectionLogic.DropCollectibleItem();
                return NodeState.Success;
            }
            _enemy.MovementLogic.WalkToPosition(_enemy.EnemyItemsCollectionLogic.StartPosition);
            return NodeState.Success;
        }
    }
}
