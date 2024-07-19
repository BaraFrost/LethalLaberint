using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class HuntingNode : EnemyNode {

        public HuntingNode(Enemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(_enemy.VisionLogic.CanSeeTarget(_enemy.Data.Player)) {
                _enemy.MovementLogic.MoveToPosition(_enemy.Data.Player.transform.position);
                return NodeState.Running;
            }
            return NodeState.Failure;
        }
    }
}
