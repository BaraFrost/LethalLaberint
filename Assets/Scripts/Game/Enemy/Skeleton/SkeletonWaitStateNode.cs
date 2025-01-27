using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class SkeletonWaitStateNode : EnemyNode<SkeletonEnemy> {

        public SkeletonWaitStateNode(SkeletonEnemy enemy) : base(enemy) { }

        public override NodeState Evaluate() {
            if (!_enemy.SkeletonHideLogic.IsStanding() && !_enemy.SkeletonHideLogic.IsHided) {
                return NodeState.Failure;
            }
            if (_enemy.SkeletonHideLogic.IsHided && (_enemy.SkeltonHideVisionLogic.CanSeeTarget(_enemy.EntitiesContainer.playerController) ||
                _enemy.EntitiesContainer.additionalEnemyTarget != null && _enemy.MovementLogic.PositionAvailable(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position))) {
                _enemy.SkeletonHideLogic.StandUp();
                return NodeState.Success;
            }
            return NodeState.Success;
        }
    }
}