using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class AttackNode : EnemyNode<Enemy> {

        public AttackNode(Enemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(_enemy.AttackLogic.IsAttacking) {
                return NodeState.Running;
            }
            if(!_enemy.VisionLogic.CanSeeTarget(_enemy.EntitiesContainer.playerController)) {
                return NodeState.Failure;
            }
            if(!_enemy.AttackLogic.CanAttackTarget(_enemy.EntitiesContainer.playerController)) {
                return NodeState.Failure;
            }
            _enemy.AttackLogic.AttackTarget(_enemy.EntitiesContainer.playerController);
            return NodeState.Success;
        }
    }
}
