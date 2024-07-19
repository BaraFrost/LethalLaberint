using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class DogEnemy : Enemy {

        private DogBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer) {
            base.Init(player, cellsContainer);
            _behaviorTree = new DogBehaviorTree(this);
        }
    }
}
