using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class AttackNode : EnemyNode {

        public AttackNode(Enemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(!_enemy.AttackLogic.CanAttackTarget(_enemy.Data.Player)) {
                return NodeState.Failure;
            }
            _enemy.AttackLogic.AttackTarget(_enemy.Data.Player);
            return NodeState.Success;
        }
    }
}
