using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class RunningAwayNode : EnemyNode<BugEnemy> {

        public RunningAwayNode(BugEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.EnemyItemsCollectionLogic.HasItem) {
                return NodeState.Failure;
            }
            var directionToPlayer = _enemy.Data.Player.transform.position - _enemy.transform.position;
            directionToPlayer.y = 0;
            var distanceToPlayer = directionToPlayer.magnitude;
            if (distanceToPlayer < _enemy.DistanceToDropItem) {
                _enemy.EnemyItemsCollectionLogic.DropCollectibleItem();
                return NodeState.Failure;
            }
            if (!_enemy.VisionLogic.CanSeeTarget(_enemy.Data.Player) && _enemy.DistanceToRunAway < distanceToPlayer || 
                !_enemy.AvailablePointSearchEnemyLogic.TryToRunAway(_enemy.Data.Player.transform.position)) {
                _enemy.MovementLogic.Rotate(_enemy.Data.Player.transform.position);
            }
            /* var availablePoints = _enemy.AvailablePointSearchEnemyLogic.GetAvailablePoints(directionToPlayer);
             var randomPointIndex = UnityEngine.Random.Range(0, availablePoints.Count);
             while (availablePoints.Count > 0) {
                 if (_enemy.MovementLogic.PositionAvailable(availablePoints[randomPointIndex])) {
                     break;
                 }
                 availablePoints.RemoveAt(randomPointIndex);
                 randomPointIndex = UnityEngine.Random.Range(0, availablePoints.Count);
             }
             if (availablePoints.Count == 0) {
                 _enemy.MovementLogic.Rotate(_enemy.Data.Player.transform.position);
                 return NodeState.Success;
             }
             _enemy.MovementLogic.MoveToPosition(availablePoints[randomPointIndex]);*/
            ;
            return NodeState.Success;
        }
    }
}

