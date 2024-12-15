using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class MaskEnemy : Enemy {

        private MaskEnemyBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        [SerializeField]
        private SpawnAdditionalEnemyLogic _spawnAdditionalEnemyLogic;
        public SpawnAdditionalEnemyLogic SpawnAdditionalEnemyLogic => _spawnAdditionalEnemyLogic;

        [SerializeField]
        private EnemyItemsCollectionLogic _enemyItemsCollectionLogic;
        public EnemyItemsCollectionLogic EnemyItemsCollectionLogic => _enemyItemsCollectionLogic;

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _behaviorTree = new MaskEnemyBehaviorTree(this);
        }
    }
}

