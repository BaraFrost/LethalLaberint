using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;

namespace Game {

    public class ZoneEnemy : Enemy {

        private ZoneBehaviorTree _zoneBehaviorTree;
        public override BehaviorTree NpcBehaviorTree => _zoneBehaviorTree;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            base.Init(player, cellsContainer, collectibleItems);
            _zoneBehaviorTree = new ZoneBehaviorTree(this);
        }
    }
}
