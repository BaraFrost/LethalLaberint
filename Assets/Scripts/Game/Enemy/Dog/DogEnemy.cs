using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class DogEnemy : Enemy {

        private DogBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            base.Init(player, cellsContainer, collectibleItems);
            _behaviorTree = new DogBehaviorTree(this);
        }
    }
}
