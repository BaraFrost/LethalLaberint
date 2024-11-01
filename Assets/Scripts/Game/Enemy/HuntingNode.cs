using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class HuntingNode : EnemyNode<Enemy> {

        private Vector3? _lastTargetPosition;

        public HuntingNode(Enemy enemy) : base(enemy) {
        }

        public override NodeState Evaluate() {
            if(_enemy.VisionLogic.CanSeeTarget(_enemy.Data.Player)) {
                _lastTargetPosition = _enemy.Data.Player.transform.position;
            }
            if(_lastTargetPosition == null) {
                return NodeState.Failure;
            }
            Debug.DrawLine(_enemy.transform.position, _lastTargetPosition.Value, Color.blue);
            _enemy.MovementLogic.WalkToPosition(_lastTargetPosition.Value);
            if (!_enemy.MovementLogic.PositionReached(_lastTargetPosition.Value)) {
                return NodeState.Running;
            }
            _lastTargetPosition = null;
            return NodeState.Failure;
        }
    }
}
