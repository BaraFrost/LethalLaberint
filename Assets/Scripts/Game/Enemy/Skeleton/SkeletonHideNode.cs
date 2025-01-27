using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class SkeletonHideNode : EnemyNode<SkeletonEnemy> {

        public SkeletonHideNode(SkeletonEnemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(!_enemy.SkeletonHideLogic.CanHide() ||
                _enemy.EntitiesContainer.additionalEnemyTarget != null && _enemy.MovementLogic.PositionAvailable(_enemy.EntitiesContainer.additionalEnemyTarget.transform.position)) {
                return NodeState.Failure;
            }
            _enemy.SkeletonHideLogic.Hide();
            return NodeState.Success;
        }
    }
}