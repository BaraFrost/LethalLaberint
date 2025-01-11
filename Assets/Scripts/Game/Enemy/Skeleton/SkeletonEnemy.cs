using Scripts.Infrastructure.BehaviorTree;
using UnityEngine;

namespace Game {

    public class SkeletonEnemy : Enemy {

        private SkeletonBehaviorTree _behaviorTree;
        public override BehaviorTree NpcBehaviorTree => _behaviorTree;

        [SerializeField]
        private AbstractEnemyVisionLogic _skeletonHideVisonLogic;
        public AbstractEnemyVisionLogic SkeltonHideVisionLogic => _skeletonHideVisonLogic;

        [SerializeField]
        private SkeletonHideLogic _skeletonHideLogic;
        public SkeletonHideLogic SkeletonHideLogic => _skeletonHideLogic;

        public override void Init(GameEntitiesContainer entitiesContainer) {
            base.Init(entitiesContainer);
            _behaviorTree = new SkeletonBehaviorTree(this);
        }
    }
}
