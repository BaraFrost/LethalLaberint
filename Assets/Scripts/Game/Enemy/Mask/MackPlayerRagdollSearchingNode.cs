using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class MackPlayerRagdollSearchingNode : EnemyNode<MaskEnemy> {

        public MackPlayerRagdollSearchingNode(MaskEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if(_enemy.SpawnAdditionalEnemyLogic.IsSpawning) {
                _enemy.MovementLogic.Stop();
                return NodeState.Success;
            }
            var playerRagdolls = _enemy.Data.Player.RagdollVisualLogic.SpawnedRagdolls;
            if (playerRagdolls.Count == 0) {
                return NodeState.Failure;
            }
            foreach(var ragdoll in playerRagdolls) {
                if(!ragdoll.collectedByEnemy && !ragdoll.Collected) {
                    if(_enemy.MovementLogic.PositionReached(ragdoll.transform.position)) {
                        _enemy.EnemyItemsCollectionLogic.CollectItem(ragdoll);
                        _enemy.SpawnAdditionalEnemyLogic.SpawnEnemy(ragdoll.transform.position);
                        return NodeState.Success;
                    }
                    if(_enemy.MovementLogic.PositionAvailable(ragdoll.transform.position)) {
                        _enemy.MovementLogic.WalkToPosition(ragdoll.transform.position);
                        return NodeState.Success;
                    }
                }
            }
            return NodeState.Failure;
        }
    }
}
