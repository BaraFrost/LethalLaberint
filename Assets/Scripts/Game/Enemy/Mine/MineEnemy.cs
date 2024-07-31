using Scripts.Infrastructure.BehaviorTree;

namespace Game {

    public class MineEnemy : Enemy {

        private MineBehaviorTree _mineBehaviorTree;
        public override BehaviorTree NpcBehaviorTree => _mineBehaviorTree;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer) {
            base.Init(player, cellsContainer);
            _mineBehaviorTree = new MineBehaviorTree(this);
        }
    }
}
