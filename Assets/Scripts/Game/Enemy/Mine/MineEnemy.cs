using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class MineEnemy : Enemy {

        private MineBehaviorTree _mineBehaviorTree;
        public override BehaviorTree NpcBehaviorTree => _mineBehaviorTree;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            base.Init(player, cellsContainer, collectibleItems);
            _mineBehaviorTree = new MineBehaviorTree(this);
        }
    }
}
