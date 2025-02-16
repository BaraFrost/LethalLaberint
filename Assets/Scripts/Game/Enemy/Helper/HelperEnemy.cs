using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class HelperEnemy : Enemy {

        private HelperBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        [SerializeField]
        private EnemyItemsCollectionLogic _enemyItemsCollectionLogic;
        public EnemyItemsCollectionLogic EnemyItemsCollectionLogic => _enemyItemsCollectionLogic;

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _behaviorTree = new HelperBehaviorTree(this);
        }
    }
}
