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

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _behaviorTree = new BugBehaviorTree(this);
        }
    }
}
