using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class ZoneEnemy : Enemy {

        private ZoneBehaviorTree _zoneBehaviorTree;
        public override BehaviorTree NpcBehaviorTree => _zoneBehaviorTree;

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _zoneBehaviorTree = new ZoneBehaviorTree(this);
        }
    }
}
