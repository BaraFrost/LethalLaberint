using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ItemsProtectionNode : EnemyNode<HelperEnemy> {

        public ItemsProtectionNode(HelperEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.EnemyItemsCollectionLogic.MaxItemsCollected) {
                return NodeState.Failure;
            }
            _enemy.MovementLogic.Stop();
            return NodeState.Success;
            /*
            if (_enemy.MovementLogic.PositionAvailable(_enemy.EnemyItemsCollectionLogic.StartPosition) && !_enemy.MovementLogic.PositionReached(_enemy.EnemyItemsCollectionLogic.StartPosition)) {
                _enemy.MovementLogic.WalkToPosition(_enemy.EnemyItemsCollectionLogic.StartPosition);
                return NodeState.Success;
            }
            _enemy.MovementLogic.Rotate(_enemy.EntitiesContainer.playerController.transform.position);
            return NodeState.Success;*/
        }
    }
}

