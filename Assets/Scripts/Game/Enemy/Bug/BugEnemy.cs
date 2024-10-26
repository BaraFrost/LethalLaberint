using Scripts.Infrastructure.BehaviorTree;
using System.Collections.Generic;
using UnityEngine;

namespace Game {

    public class BugEnemy : Enemy {

        private BugBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        [SerializeField]
        private EnemyItemsCollectionLogic _enemyItemsCollectionLogic;
        public EnemyItemsCollectionLogic EnemyItemsCollectionLogic => _enemyItemsCollectionLogic;

        [SerializeField]
        private RunAwayEnemyLogic _availablePointSearchEnemyLogic;
        public RunAwayEnemyLogic AvailablePointSearchEnemyLogic => _availablePointSearchEnemyLogic;

        [SerializeField]
        private float _distanceToDropItem;
        public float DistanceToDropItem => _distanceToDropItem;

        [SerializeField]
        private float _distanceToRunAway;
        public float DistanceToRunAway => _distanceToRunAway;

        public override void Init(PlayerController player, SpawnedLabyrinthCellsContainer cellsContainer, List<CollectibleItem> collectibleItems) {
            base.Init(player, cellsContainer, collectibleItems);
            _behaviorTree = new BugBehaviorTree(this);
        }
    }
}
