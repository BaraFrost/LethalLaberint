using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class WardenEnemy : Enemy {

        private WardenBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        [SerializeField]
        private SpawnAdditionalEnemyLogic _spawnAdditionalEnemyLogic;
        public SpawnAdditionalEnemyLogic SpawnAdditionalEnemyLogic => _spawnAdditionalEnemyLogic;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            base.Init(player, cellsContainer, collectibleItems);
            _behaviorTree = new WardenBehaviorTree(this);
        }
    }
}

