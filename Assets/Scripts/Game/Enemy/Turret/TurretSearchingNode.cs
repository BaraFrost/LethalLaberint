using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class TurretSearchingNode : EnemyNode<TurretEnemy> {

        private Vector3? target;

        public TurretSearchingNode(TurretEnemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(target == null || _enemy.TurretRotationLogic.RotationReached(target.Value)) {
                target = _enemy.TurretRotationLogic.GetTargetRotation();
            }
            _enemy.TurretRotationLogic.Rotate(target.Value);
            return NodeState.Running;
        }
    }
}
