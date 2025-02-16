using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ItemsCollectionNode : EnemyNode<HelperEnemy> {

        private CollectibleItem _collectibleItem;

        public ItemsCollectionNode(HelperEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (_collectibleItem == null) {
                _enemy.EnemyItemsCollectionLogic.TryToFindItem(_enemy.EntitiesContainer.collectibleItems, out var collectibleItem);
                _collectibleItem = collectibleItem;
            }
            if (_collectibleItem == null) {
                return NodeState.Failure;
            }
            if (!_enemy.MovementLogic.PositionAvailable(_collectibleItem.transform.position)) {
                _collectibleItem.collectedByEnemy = false;
                _collectibleItem = null;
                return NodeState.Failure;
            }
            _collectibleItem.collectedByEnemy = true;
            if (!_enemy.MovementLogic.PositionReached(_collectibleItem.transform.position)) {
                _enemy.MovementLogic.WalkToPosition(_collectibleItem.transform.position);
                return NodeState.Success;
            }
            /*if (!_enemy.VisionLogic.CanSeeTarget(_enemy.EntitiesContainer.playerController)) {
                _enemy.MovementLogic.Rotate(_enemy.EntitiesContainer.playerController.transform.position);
                return NodeState.Success;
            }*/
            _enemy.EnemyItemsCollectionLogic.CollectItem(_collectibleItem);
            _collectibleItem = null;
            return NodeState.Success;
            /*if (_enemy.EnemyItemsCollectionLogic.HasItem || !_enemy.EnemyItemsCollectionLogic.TryToFindItem(_enemy.EntitiesContainer.collectibleItems, out var item) 
                || !_enemy.MovementLogic.PositionAvailable(item.transform.position)) {
                return NodeState.Failure;
            }
            if (_enemy.MovementLogic.PositionReached(item.transform.position)) {
                _enemy.EnemyItemsCollectionLogic.CollectItem(item);
                return NodeState.Success;
            }
            _enemy.MovementLogic.WalkToPosition(item.transform.position);
            return NodeState.Success;*/
        }
    }
}