using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class WardenEnemy : Enemy {

        private WardenBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        [SerializeField]
        private SpawnAdditionalEnemyLogic _spawnAdditionalEnemyLogic;
        public SpawnAdditionalEnemyLogic SpawnAdditionalEnemyLogic => _spawnAdditionalEnemyLogic;

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _behaviorTree = new WardenBehaviorTree(this);
        }
    }
}

