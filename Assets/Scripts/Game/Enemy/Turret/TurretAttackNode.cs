using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class TurretAttackNode : EnemyNode<TurretEnemy> {

        public TurretAttackNode(TurretEnemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if (!_enemy.VisionLogic.CanSeeTarget(_enemy.Data.Player)) {
                _enemy.AttackLogic.StopAttack();
                return NodeState.Failure;
            }
            if (!_enemy.TurretRotationLogic.RotationReached(_enemy.Data.Player.transform.position)) {
                _enemy.TurretRotationLogic.Rotate(_enemy.Data.Player.transform.position, true);
                return NodeState.Running;
            }
            if (_enemy.AttackLogic.CanAttackTarget(_enemy.Data.Player)) {
                _enemy.AttackLogic.AttackTarget(_enemy.Data.Player);
                return NodeState.Success;
            }
            _enemy.AttackLogic.StopAttack();
            return NodeState.Failure;
        }
    }
}

