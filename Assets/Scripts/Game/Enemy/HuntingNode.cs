using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class HuntingNode : EnemyNode {

        private Vector3? _lastTargetPosition;

        public HuntingNode(Enemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(_enemy.VisionLogic.CanSeeTarget(_enemy.Data.Player)) {
                _lastTargetPosition = _enemy.Data.Player.transform.position;
            }
            if(_lastTargetPosition != null && !_enemy.MovementLogic.PositionReached(_lastTargetPosition.Value)) {
                _enemy.MovementLogic.MoveToPosition(_enemy.Data.Player.transform.position);
                _lastTargetPosition = null;
                return NodeState.Running;
            }
            return NodeState.Failure;
        }
    }
}
