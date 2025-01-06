using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class DogEnemy : Enemy {

        private DogBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _behaviorTree = new DogBehaviorTree(this);
        }
    }
}
