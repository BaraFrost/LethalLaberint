using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class TurretEnemy : Enemy {

        private TurretBehaviorTree _turretBehaviorTree;
        public override BehaviorTree NpcBehaviorTree => _turretBehaviorTree;

        [SerializeField]
        private TurretRotationLogic _turretRotationLogic;
        public TurretRotationLogic TurretRotationLogic => _turretRotationLogic;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            base.Init(player, cellsContainer, collectibleItems);
            _turretBehaviorTree = new TurretBehaviorTree(this);
        }
    }
}
